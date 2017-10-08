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
			var query = @"select BetId, Sport, BetDate, Details, Risking, ToCollect, GroupBet, PersonVanorny, 
							PersonBorst, PersonLesinski, PersonTschida, PersonKerber, Outcome from Bets";
			var results = db.Query<BetObj>(query).OrderByDescending(x => x.BetDate).ToList();
			return results;
		}

		public BetObj GetSingleBet(int betId)
		{
			IDbConnection db = new SqlConnection(sqlPath);
			var query = "select BetId, Sport, BetDate, Details, Risking, ToCollect, PersonVanorny, PersonBorst, PersonLesinski, PersonTschida, PersonKerber, Outcome from Bets where BetId=" + betId;
			var result = db.Query<BetObj>(query).First();
			return result;
		}

		public void AddNewBet(BetObj bet)
		{
			var sql = @"insert into Bets (BetDate, Sport, Details, Risking, ToCollect, 
						PersonVanorny, PersonLesinski, PersonBorst, PersonKerber, PersonTschida, GroupBet, Outcome)
						values (@a, @b, @c, @d, @e, @f, @g, @h, @i, @j, @k, @l)";

			var queryParams = new
			{
				a = bet.BetDate,
				b = bet.Sport,
				c = bet.Details,
				d = bet.Risking,
				e = bet.ToCollect,
				f = bet.PersonVanorny,
				g = bet.PersonLesinski,
				h = bet.PersonBorst,
				i = bet.PersonKerber,
				j = bet.PersonTschida,
				k = FindIfGroupBet(bet),
				l = bet.Outcome
			};

			IDbConnection db = new SqlConnection(sqlPath);
			db.Execute(sql, queryParams);
		}

		private string FindIfGroupBet(BetObj bet)
		{
			if (bet.PersonBorst && bet.PersonKerber && bet.PersonLesinski && bet.PersonTschida && bet.PersonVanorny)
				return "Y";
			else
				return "N";
		}

		public void UpdateBetItem(BetObj bet)
		{
			var sql = @"update Bets set BetDate=@a, Sport=@b, Details=@c, Risking=@d, ToCollect=@e, 
						PersonVanorny=@f, PersonLesinski=@g, PersonBorst=@h, PersonKerber=@i, PersonTschida=@j, GroupBet=@k, Outcome=@l
						where BetId=@id";

			var queryParams = new
			{
				a = bet.BetDate,
				b = bet.Sport,
				c = bet.Details,
				d = bet.Risking,
				e = bet.ToCollect,
				f = bet.PersonVanorny,
				g = bet.PersonLesinski,
				h = bet.PersonBorst,
				i = bet.PersonKerber,
				j = bet.PersonTschida,
				k = FindIfGroupBet(bet),
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