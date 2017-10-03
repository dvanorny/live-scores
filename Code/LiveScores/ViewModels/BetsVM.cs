using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LiveScores.Entities;

namespace LiveScores.ViewModels
{
	public class BetsVM
	{
		private string sqlPath;

		public BetsVM(string connPath)
		{
			sqlPath = connPath;
		}

		public IList<BetObj> GetAllBets()
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = "select * from Bets order by BetDate desc";
			var results = (List<BetObj>)db.Query<BetObj>(query);
			return results;
		}

		public BetObj GetSingleBet(int betId)
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = "select * from Bets where BetId=" + betId;
			var result = db.Query<BetObj>(query).First();
			return result;
		}

		public void UpdateBetItem(BetObj betObj)
		{
			
		}
		
	}
}