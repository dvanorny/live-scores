using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using LiveScores.Models;
using Newtonsoft.Json;

namespace LiveScores.Controllers
{
	public class HomeController : Controller
	{
		public async Task<ActionResult> Index()
		{
			var result = GetNflScores();
			return View();
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
				var nflGame = new NflGame();
				nflGame.HomeTeamAbbrev = game.Attributes["h"].Value;
				nflGame.HomeTeamName = game.Attributes["hnn"].Value;
				nflGame.VisitorTeamAbbrev = game.Attributes["v"].Value;
				nflGame.VisitorTeamName = game.Attributes["vnn"].Value;
				nflGame.CurrentQuarter = game.Attributes["q"].Value;
				nflGame.DayOfWeek = game.Attributes["d"].Value;
				nflGame.GameId = game.Attributes["gsis"].Value;
				nflGame.HomeScore = Convert.ToInt16(game.Attributes["hs"].Value);
				nflGame.VisitorScore = Convert.ToInt16(game.Attributes["vs"].Value);
				nflGame.GameTime = game.Attributes["t"].Value;
				nflGame.InRedzone = BoolConvert(game.Attributes["rz"].Value);
				gamesList.Add(nflGame);
			}
			return gamesList;
		}

		private bool BoolConvert(string value)
		{
			if (value == "0")
				return false;
			else
				return true;
		}

		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}


		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}