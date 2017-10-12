using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LiveScoresWeb.Entities;

namespace LiveScoresWeb.ViewModels
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
			var query = @"select BetId, Sport, BetDate, Details, Risking, ToCollect, GroupBet, DV, 
							AB, CL, AT, JK, Outcome from Bets";
			var results = db.Query<BetObj>(query).OrderByDescending(x => x.BetDate).ToList();
			return results;
		}

		public BetObj GetSingleBet(int betId)
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = "select BetId, Sport, BetDate, Details, Risking, ToCollect, DV, AB, CL, AT, JK, Outcome from Bets where BetId=" + betId;
			var result = db.Query<BetObj>(query).First();
			return result;
		}

		public void AddNewBet(BetObj bet)
		{
			var sql = @"insert into Bets (BetDate, Sport, Details, Risking, ToCollect, 
						DV, CL, AB, JK, AT, Outcome)
						values (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @l)";

			var queryParams = new
			{
				a = bet.BetDate,
				b = bet.Sport,
				c = bet.Details,
				d = bet.Risking,
				e = bet.ToCollect,
				f = bet.DV,
				g = bet.CL,
				h = bet.AB,
				i = bet.JK,
				j = bet.AT,
				l = bet.Outcome
			};

			IDbConnection db = new SqlConnection(sqlPath);
			db.Execute(sql, queryParams);
		}

		private string FindIfGroupBet(BetObj bet)
		{
			if (bet.AB && bet.JK && bet.CL && bet.AT && bet.DV)
				return "Y";
			else
				return "N";
		}

		public void UpdateBetItem(BetObj bet)
		{
			var sql = @"update Bets set BetDate=@a, Sport=@b, Details=@c, Risking=@d, ToCollect=@e, 
						DV=@f, CL=@g, AB=@h, JK=@i, AT=@j, Outcome=@l
						where BetId=@id";

			var queryParams = new
			{
				a = bet.BetDate,
				b = bet.Sport,
				c = bet.Details,
				d = bet.Risking,
				e = bet.ToCollect,
				f = bet.DV,
				g = bet.CL,
				h = bet.AB,
				i = bet.JK,
				j = bet.AT,
				l = bet.Outcome,
				id = bet.BetId
			};

			IDbConnection db = new SqlConnection(sqlPath);
			db.Execute(sql, queryParams);
		}

		public void DeleteBetItem(int betId)
		{
			var sql = @"delete from Bets where BetId=@id";

			var queryParams = new
			{
				id = betId
			};

			IDbConnection db = new SqlConnection(sqlPath);
			db.Execute(sql, queryParams);
		}


	}
}