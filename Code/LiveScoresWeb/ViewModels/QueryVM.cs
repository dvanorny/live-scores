using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Dapper;
using LiveScoresWeb.Entities;

namespace LiveScoresWeb.ViewModels
{
	public class QueryVM
	{
		private string dbConn = WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
		
		public string GetSavedQuerySql(int queryId)
		{
			var results = "";
			if (queryId > 0)
			{
				var sql = @"select QueryText from QueryScripts where QueryId=@id order by QueryText";

				using (var conn = new SqlConnection(dbConn))
				{
					conn.Open();
					using (var cmd = new SqlCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@id", queryId);
						results = cmd.ExecuteScalar().ToString();
					}
				}
			}
			return results;
		}

		public string GetSavedQueryDesc(int queryId)
		{
			var results = "";
			if (queryId > 0)
			{
				var sql = @"select QueryDescription from QueryScripts where QueryId=@id";

				using (var conn = new SqlConnection(dbConn))
				{
					conn.Open();
					using (var cmd = new SqlCommand(sql, conn))
					{
						cmd.Parameters.AddWithValue("@id", queryId);
						results = cmd.ExecuteScalar().ToString();
					}
				}
			}
			return results;
		}

		public IEnumerable<dynamic> ExecuteSql(QueryObj queryObj)
		{
			IEnumerable<dynamic> results = null;
			using (IDbConnection myConn = new SqlConnection(dbConn))
			{
				myConn.Open();

				using (var tran = myConn.BeginTransaction(IsolationLevel.ReadUncommitted))
				{
					try
					{
						results = myConn.Query(queryObj.QuerySql, null, tran);
						tran.Commit();
					}
					catch { }
				}

			}
			return results;
		}

		public Dictionary<int, string> GetSavedQueries()
		{
			var results = new Dictionary<int, string>();
			var sql = @"select QueryId, QueryName from QueryScripts where ActiveStatus=1 order by QueryName";

			using (var conn = new SqlConnection(dbConn))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql, conn))
				{
					using (var rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							results.Add(Convert.ToInt32(rdr["QueryId"]), rdr["QueryName"].ToString());
						}
					}
				}
			}

			return results;
		}
	}
}