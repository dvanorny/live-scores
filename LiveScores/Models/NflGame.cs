using System;

namespace LiveScores.Models
{
	public class NflGame
	{
		public string GameId { get; set; }
		public string DayOfWeek { get; set; }
		public string GameTime { get; set; }
		public string CurrentQuarter { get; set; }
		public string HomeTeamAbbrev { get; set; }
		public string HomeTeamName { get; set; }
		public string VisitorTeamAbbrev { get; set; }
		public string VisitorTeamName { get; set; }
		public int HomeScore { get; set; }
		public int VisitorScore { get; set; }
		public bool InRedzone { get; set; }
	}
}