using System;
using System.EnterpriseServices.Internal;

namespace LiveScores.Entities
{
	public class BetObj
	{
		public int BetId { get; set; }
		public string Sport { get; set; }
		public DateTime BetDate { get; set; }
		public string Details { get; set; }
		public double Risking { get; set; }
		public double ToCollect { get; set; }
		public string Outcome { get; set; }
		public string GroupBet { get; set; }
		public bool PersonVanorny { get; set; }
		public bool PersonBorst { get; set; }
		public bool PersonTschida { get; set; }
		public bool PersonLesinksi { get; set; }
		public bool PersonKerber { get; set; }
		public DateTime CreatedOn { get; set; }

		public bool AllBet
		{
			get
			{
				if (PersonBorst && PersonKerber && PersonLesinksi && PersonTschida && PersonVanorny)
					return true;
				return false;
			}
		}

		public string OutcomeStyle
		{
			get
			{
				if (Outcome.ToUpper() == "W")
				{
					return "cell-win";
				}
				else if (Outcome.ToUpper() == "L")
				{
					return "cell-loss";
				}
				else if (Outcome.ToUpper() == "P")
				{
					return "cell-push";
				}
				else
				{
					return "";
				}
			}
		}
	}
}