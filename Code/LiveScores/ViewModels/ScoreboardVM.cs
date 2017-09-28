using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Xml;
using LiveScores.Entities;

namespace LiveScores.ViewModels
{
	public class ScoreboardVM
	{

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