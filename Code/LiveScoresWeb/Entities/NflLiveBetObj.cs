using System;
using System.ComponentModel.DataAnnotations;

namespace LiveScoresWeb.Entities
{
	public class NflLiveBetObj
	{
		public int BetId { get; set; }
		public string Sport { get; set; }
		public DateTime BetDate { get; set; }
		public string Details { get; set; }
		public float Risking { get; set; }
		public float ToCollect { get; set; }
		public string Outcome { get; set; }
		public string GroupBet { get; set; }

		public string ExternalId { get; set; }
		public int TypeOfBet { get; set; }
		public string BetTeam { get; set; }
		public string BetNumber { get; set; }
		
		public string GameId { get; set; }
		public string DayOfWeek { get; set; }
		public string GameTime { get; set; }

		public string StatusStyle
		{
			get
			{

				switch (CurrentStatus.ToUpper())
				{
					case "WINNING":
						return "background:green;color:white;";
					case "LOSING":
						return "background:red;color:black;";
					case "PUSH":
						return "background:lightgrey;color:black;";
					default:
						return "";
				}
			}
		}
		public string CurrentQuarter { get; set; }
		public string CurrentQuarterDisplay
		{
			get
			{
				switch (CurrentQuarter.ToLower())
				{
					case "p":
						return "Not Started";
					case "f":
						return "FINAL";
					case "1":
						return "Q1";
					case "2":
						return "Q2";
					case "3":
						return "Q3";
					case "4":
						return "Q4";
					default:
						return "";
				}

			}
		}

		public string HomeTeamAbbrev { get; set; }
		public string HomeTeamName { get; set; }
		public string VisitorTeamAbbrev { get; set; }
		public string VisitorTeamName { get; set; }
		public int HomeScore { get; set; }
		public int VisitorScore { get; set; }
		public bool InRedzone { get; set; }

		public string CurrentStatus { get; set; }
		public string BetDetailsDisplay
		{
			get
			{
				switch (TypeOfBet)
				{
					case 1:
						return String.Concat("O ", BetNumber);
					case 2:
						return String.Concat("U ", BetNumber);
					case 3:
						return String.Concat(BetTeam, " ML");
					case 4:
						if (BetNumber.StartsWith("-"))
							return String.Concat(HomeTeamAbbrev, " ", BetNumber);
						else if (BetNumber.StartsWith("+"))
							return String.Concat(VisitorTeamAbbrev, " ", BetNumber);
						else
							return BetNumber;
				}
				return "";
			}
		}
	}
}