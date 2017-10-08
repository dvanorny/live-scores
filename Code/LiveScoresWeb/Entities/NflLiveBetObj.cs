﻿using System;
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
						return "1";
					case "2":
						return "2";
					case "3":
						return "3";
					case "4":
						return "4";
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
						return String.Concat(BetTeam, " ", BetNumber);
				}
				return "";
			}
		}
	}
}