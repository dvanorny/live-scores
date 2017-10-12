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

		public IList<NflLiveBetObj> CreateLiveUpdate(IList<NflLiveBetObj> bets, IList<NflGame> games)
		{
			var combinedList = new List<NflLiveBetObj>();
			foreach (var bet in bets)
			{
				//Find the score
				var game = games.First(x => x.GameId.Equals(bet.ExternalId));
				bet.VisitorScore = game.VisitorScore;
				bet.HomeScore = game.HomeScore;
				bet.CurrentQuarter = game.CurrentQuarter;
				bet.GameTime = game.GameTime;
				bet.VisitorTeamAbbrev = game.VisitorTeamAbbrev;
				bet.VisitorTeamName = game.VisitorTeamName;
				bet.HomeTeamName = game.HomeTeamName;
				bet.HomeTeamAbbrev = game.HomeTeamAbbrev;
				bet.GameId = game.GameId;
				//Figure out the CurrentStatus property
				if (bet.CurrentQuarter == "P")
					bet.CurrentStatus = "";
				else
				{
					switch (bet.TypeOfBet)
					{
						case 1:
							var numberToHit = Convert.ToDecimal(bet.BetNumber);
							if (bet.HomeScore + bet.VisitorScore > numberToHit)
								bet.CurrentStatus = "WINNING";
							else if (bet.HomeScore + bet.VisitorScore == numberToHit)
								bet.CurrentStatus = "PUSH";
							else
								bet.CurrentStatus = "LOSING";
							break;
						case 2:
							var numberToHit2 = Convert.ToDecimal(bet.BetNumber);
							if (bet.HomeScore + bet.VisitorScore < numberToHit2)
								bet.CurrentStatus = "WINNING";
							else if (bet.HomeScore + bet.VisitorScore == numberToHit2)
								bet.CurrentStatus = "PUSH";
							else
								bet.CurrentStatus = "LOSING";
							break;
						case 4:
							var betNum = bet.BetNumber;
							if (betNum.StartsWith("-"))
							{
								var spread = Convert.ToDecimal(betNum.Substring(1));
								if (bet.HomeScore - spread > bet.VisitorScore)
									bet.CurrentStatus = "WINNING";
								else if (bet.HomeScore - spread == bet.VisitorScore)
									bet.CurrentStatus = "PUSH";
								else
									bet.CurrentStatus = "LOSING";
							} else if (betNum.StartsWith("+")) {
								var spread = Convert.ToDecimal(betNum.Substring(1));
								if (bet.VisitorScore - spread < bet.HomeScore)
									bet.CurrentStatus = "WINNING";
								else if (bet.VisitorScore - spread == bet.HomeScore)
									bet.CurrentStatus = "PUSH";
								else
									bet.CurrentStatus = "LOSING";
							}
							break;
					}
				}


				combinedList.Add(bet);
			}
			return combinedList;
		}

		public IList<NflLiveBetObj> GetNflBets()
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = @"select a.BetId, a.BetDate, a.Details, a.Risking, a.ToCollect, a.GroupBet, b.ExternalId, b.TypeOfBet, b.BetTeam, b.BetNumber, a.Notes
							from NflBets b
							inner join Bets a on a.BetId=b.BetId
							where a.Sport='NFL' and (a.Outcome='' or a.Outcome is null)";
			var results = db.Query<NflLiveBetObj>(query).OrderBy(x => x.BetDate).ToList();
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