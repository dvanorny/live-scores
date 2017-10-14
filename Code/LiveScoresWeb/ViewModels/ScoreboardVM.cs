using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;
using Dapper;
using LiveScoresWeb.Entities;

namespace LiveScoresWeb.ViewModels
{
	public class ScoreboardVM
	{
		private string sqlPath;

		public ScoreboardVM(string connPath)
		{
			sqlPath = connPath;
		}

		public IList<LiveBetObj> CreateLiveUpdate(IList<LiveBetObj> bets, IList<NflGame> games)
		{
			var combinedList = new List<LiveBetObj>();
			foreach (var bet in bets)
			{
				foreach (var myGame in bet.NflItems)
				{
					if (bet.Sport.ToUpper() == "NFL" && !String.IsNullOrEmpty(myGame.ExternalId))
					{
						//Find the score
						var game = games.First(x => x.GameId.Equals(myGame.ExternalId));
						myGame.VisitorScore = game.VisitorScore;
						myGame.HomeScore = game.HomeScore;
						myGame.CurrentQuarter = game.CurrentQuarter;
						myGame.GameTime = game.GameTime;
						myGame.VisitorTeamAbbrev = game.VisitorTeamAbbrev;
						myGame.VisitorTeamName = game.VisitorTeamName;
						myGame.HomeTeamName = game.HomeTeamName;
						myGame.HomeTeamAbbrev = game.HomeTeamAbbrev;
						myGame.GameId = game.GameId;
						//Figure out the CurrentStatus property
						if (myGame.CurrentQuarter == "P")
							myGame.CurrentStatus = "";
						else
						{
							switch (myGame.TypeOfBet)
							{
								case 1:
									var numberToHit = Convert.ToDecimal(myGame.BetNumber);
									if (myGame.HomeScore + myGame.VisitorScore > numberToHit)
										myGame.CurrentStatus = "WINNING";
									else if (myGame.HomeScore + myGame.VisitorScore == numberToHit)
										myGame.CurrentStatus = "PUSH";
									else
										myGame.CurrentStatus = "LOSING";
									break;
								case 2:
									var numberToHit2 = Convert.ToDecimal(myGame.BetNumber);
									if (myGame.HomeScore + myGame.VisitorScore < numberToHit2)
										myGame.CurrentStatus = "WINNING";
									else if (myGame.HomeScore + myGame.VisitorScore == numberToHit2)
										myGame.CurrentStatus = "PUSH";
									else
										myGame.CurrentStatus = "LOSING";
									break;
								case 4:
									var betNum = myGame.BetNumber;
									if (betNum.StartsWith("-"))
									{
										var spread = Convert.ToDecimal(betNum.Substring(1));
										if (myGame.HomeScore - spread > myGame.VisitorScore)
											myGame.CurrentStatus = "WINNING";
										else if (myGame.HomeScore - spread == myGame.VisitorScore)
											myGame.CurrentStatus = "PUSH";
										else
											myGame.CurrentStatus = "LOSING";
									}
									else if (betNum.StartsWith("+"))
									{
										var spread = Convert.ToDecimal(betNum.Substring(1));
										if (myGame.VisitorScore - spread < myGame.HomeScore)
											myGame.CurrentStatus = "WINNING";
										else if (myGame.VisitorScore - spread == myGame.HomeScore)
											myGame.CurrentStatus = "PUSH";
										else
											myGame.CurrentStatus = "LOSING";
									}
									break;
							}
						}
					}
				}

				combinedList.Add(bet);
			}
			return combinedList;
		}

		public IList<LiveBetObj> GetScoreboardBets()
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = @"select BetId, BetDate, Sport, Details, Risking, ToCollect, GroupBet, Outcome, Notes
							from Bets where cast(dateadd(hour, -5, getdate()) as date) = cast(a.BetDate as date)";
			var results = db.Query<LiveBetObj>(query).OrderBy(x => x.BetDate).ToList();

			var query2 = @"select ExternalId, TypeOfBet, BetTeam, BetNumber
							from NflGames where BetId = @id";
			foreach (var game in results)
			{
				
			}
			return results;
		}

		public IList<NflGame> GetNflScores()
		{
			//string url = "http://www.nfl.com/liveupdate/scores/scores.json";
			string url = "http://www.nfl.com/liveupdate/scorestrip/ss.xml";
			//http://www.nfl.com/ajax/scorestrip?season=2017&seasonType=REG&week=4

			var webpage = new WebClient();
			var html = webpage.DownloadString(url);
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(html);
			XmlNode result = xmlDoc.SelectNodes("//ss/gms")[0];

			var gamesList = new List<NflGame>();
			foreach (XmlNode game in result.ChildNodes)
			{
				var nflGame = new NflGame
				{
					HomeTeamAbbrev = game.Attributes["h"].Value,
					HomeTeamName = CapWord(game.Attributes["hnn"].Value),
					VisitorTeamAbbrev = game.Attributes["v"].Value,
					VisitorTeamName = CapWord(game.Attributes["vnn"].Value),
					CurrentQuarter = game.Attributes["q"].Value,
					DayOfWeek = game.Attributes["d"].Value,
					GameId = game.Attributes["gsis"].Value,
					HomeScore = Convert.ToInt16(game.Attributes["hs"].Value),
					VisitorScore = Convert.ToInt16(game.Attributes["vs"].Value),
					GameTime = game.Attributes["t"].Value,
					InRedzone = BoolConvert(game.Attributes["rz"].Value)
				};
				gamesList.Add(nflGame);
			}
			return gamesList;
		}

		private string CapWord(string word)
		{
			if (String.IsNullOrEmpty(word))
				return "";

			CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			return textInfo.ToTitleCase(word);
		}

		private bool BoolConvert(string value)
		{
			if (value == "0")
				return false;

			return true;
		}
	}
}