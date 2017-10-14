using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiveScoresWeb.Entities
{
	public class LiveBetObj
	{
		public int BetId { get; set; }
		public string Sport { get; set; }
		public DateTime BetDate { get; set; }
		public string Details { get; set; }
		public float Risking { get; set; }
		public float ToCollect { get; set; }
		public string Outcome { get; set; }
		public string GroupBet { get; set; }

		public IList<LiveBetNflObj> NflItems { get; set; }
	}
}