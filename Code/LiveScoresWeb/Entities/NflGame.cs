namespace LiveScoresWeb.Entities
{
	public class NflGame
	{
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
	}
}