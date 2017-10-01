using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
			var query = "select * from Bets";
			var results = (List<BetObj>)db.Query<BetObj>(query);
			return results;
		}
		
	}
}