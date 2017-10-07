using System;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;

namespace LiveScores.Entities
{
	public class BetObj
	{
		public int BetId { get; set; }
		public string Sport { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}", ConvertEmptyStringToNull = true)]
		public DateTime BetDate { get; set; }

		public string Details { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", ConvertEmptyStringToNull = true)]
		[DataType(DataType.Currency)]
		public decimal Risking { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", ConvertEmptyStringToNull = true)]
		[DataType(DataType.Currency)]
		public decimal ToCollect { get; set; }

		public string Outcome { get; set; }
		public string GroupBet { get; set; }
		public bool PersonVanorny { get; set; }
		public bool PersonBorst { get; set; }
		public bool PersonTschida { get; set; }
		public bool PersonLesinski { get; set; }
		public bool PersonKerber { get; set; }
		public DateTime CreatedOn { get; set; }

		public bool AllBet
		{
			get
			{
				if (PersonBorst && PersonKerber && PersonLesinski && PersonTschida && PersonVanorny)
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