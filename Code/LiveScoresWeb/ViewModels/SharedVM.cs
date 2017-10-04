using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LiveScoresWeb.ViewModels
{
	public class SharedVM
	{
		private static string dbPath = "";

		public static List<string> GetActiveColumns(string userId, string gridName)
		{
			LoadDatabasePath();
			var list = new List<string>();
			var sql = @"select ActiveColumn from UserProfileGridColumns where Username=@user and GridName=@grid order by SortOrder";
			using (var conn = new SqlConnection(dbPath))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				cmd.Parameters.AddWithValue("@user", userId);
				cmd.Parameters.AddWithValue("@grid", gridName);
				using (var rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						list.Add(rdr[0].ToString().ToLower());
					}
				}
			}
			//If no columns were returned from the LiveScores database, then return the default list
			if (list.Count == 0)
			{
				list.Add("siteid");
				list.Add("sitename");
				list.Add("loginbtn");
				list.Add("dbserver");
				list.Add("dbname");
				list.Add("version");
				list.Add("createdon");
				list.Add("selfhostedsite");
				list.Add("roundnumber");
				list.Add("infobtn");
                               
			}
			return list;
		}

		public static bool DisplayBannerBit(string userId)
		{
			var result = true;
			var missingUser = false;
			LoadDatabasePath();
			var sql = @"select ShowBanner from UserBannerProperties where Username=@user";
			using (var conn = new SqlConnection(dbPath))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@user", userId);
					var obj = cmd.ExecuteScalar();
					if (obj == null)
						missingUser = true;
					else
						result = Convert.ToBoolean(obj);
				}
				if (missingUser)
				{
					//Insert a new record into the database
					var sql2 = "insert into UserBannerProperties (Username) values (@user)";
					using (var cmd = new SqlCommand(sql2, conn))
					{
						cmd.Parameters.AddWithValue("@user", userId);
						cmd.ExecuteNonQuery();
					}
				}
			}
			return result;
		}

		public void CloseBanner(string userId)
		{
			LoadDatabasePath();
			var sql = @"update UserBannerProperties set ShowBanner=0 where Username=@user";
			using (var conn = new SqlConnection(dbPath))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				cmd.Parameters.AddWithValue("@user", userId);
				cmd.ExecuteNonQuery();
			}
		}

		public void UpdateActiveColumns(string userId, string gridName, List<string> newColumns)
		{
			LoadDatabasePath();
			var sql1 = @"delete from UserProfileGridColumns where Username=@user and GridName=@grid";

			var sb = new StringBuilder();
			sb.Append("insert into UserProfileGridColumns (Username, GridName, ActiveColumn) VALUES ");
			for (int i = 0; i < newColumns.Count; i++)
			{
				if (i > 0)
					sb.Append(", ");
				sb.Append("(@user, @grid, '");
				sb.Append(newColumns[i]);
				sb.Append("')");
			}
			var sql2 = sb.ToString();

			using (var conn = new SqlConnection(dbPath))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql1, conn))
				{
					cmd.Parameters.AddWithValue("@user", userId);
					cmd.Parameters.AddWithValue("@grid", gridName);
					cmd.ExecuteNonQuery();
				}
				using (var cmd = new SqlCommand(sql2, conn))
				{
					cmd.Parameters.AddWithValue("@user", userId);
					cmd.Parameters.AddWithValue("@grid", gridName);
					cmd.ExecuteNonQuery();
				}
			}
		}

		private static void LoadDatabasePath()
		{
			if (dbPath == "")
				dbPath = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
		}
		
		public static void LogPageHit(string pageDesc, string username)
		{
			LoadDatabasePath();
			var sql = @"insert into PageViewLog (Username, PageView) values (@user, @page)";

			using (var conn = new SqlConnection(dbPath))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				cmd.Parameters.AddWithValue("@user", username);
				cmd.Parameters.AddWithValue("@page", pageDesc);
				cmd.ExecuteNonQuery();
			}
		}
	}
}