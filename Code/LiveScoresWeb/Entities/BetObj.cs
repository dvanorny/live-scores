using System;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;

namespace LiveScoresWeb.Entities
{
	public class BetObj
	{
		public int BetId { get; set; }
		public string Sport { get; set; }

		[DataType(DataType.Date)]
		public DateTime BetDate { get; set; }
		
		[Required]
		public string Details { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", ConvertEmptyStringToNull = true)]
		public float Risking { get; set; }

		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}", ConvertEmptyStringToNull = true)]
		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public float ToCollect { get; set; }

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
				if (Outcome is null)
					return "";
				if (Outcome.ToUpper() == "W")
					return "cell-win";
				if (Outcome.ToUpper() == "L")
					return "cell-loss";
				if (Outcome.ToUpper() == "P")
					return "cell-push";
				return "";
			}
		}
	}
}