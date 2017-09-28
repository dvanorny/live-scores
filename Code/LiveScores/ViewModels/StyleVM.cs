using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LiveScores.Entities;

namespace LiveScores.ViewModels
{
	public class StyleVM
	{
		private string mainDb;

		public StyleVM()
		{
			mainDb = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
		}

		public void SaveStylesForUser(StyleObj styles, string username)
		{
			var sql = "delete from UserStyles where Username=@user";
			var sql2 = @"insert into UserStyles (Username, BackgroundColor, FontFamily, TitleColor, MainLinksColor, TopFrameColor, TopFrameBorderColor, TableHeaderRowBackground, TableRowBackground, TableRowAlternateBackground, TableCellBorder, TablePadding, FontSize)
						values (@user, @bgcolor, @font, @title, @mainlinks, @topframebkgd, @topframeborder, @tableheader, @tablerow, @tablealternaterow, @cellborder, @tablepadding, @fontsize)
						";

			using (var conn = new SqlConnection(mainDb))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@user", username);
					cmd.ExecuteNonQuery();
				}
				using (var cmd = new SqlCommand(sql2, conn))
				{
					cmd.Parameters.AddWithValue("@user", username);
					if (String.IsNullOrEmpty(styles.BackgroundColor))
						cmd.Parameters.AddWithValue("@bgcolor", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@bgcolor", styles.BackgroundColor);

					if (String.IsNullOrEmpty(styles.FontFamily))
						cmd.Parameters.AddWithValue("@font", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@font", styles.FontFamily);

					if (String.IsNullOrEmpty(styles.TitleColor))
						cmd.Parameters.AddWithValue("@title", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@title", styles.TitleColor);
					
					if (String.IsNullOrEmpty(styles.MainLinksColor))
						cmd.Parameters.AddWithValue("@mainlinks", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@mainlinks", styles.MainLinksColor);

					if (String.IsNullOrEmpty(styles.TopFrameColor))
						cmd.Parameters.AddWithValue("@topframebkgd", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@topframebkgd", styles.TopFrameColor);

					if (String.IsNullOrEmpty(styles.TopFrameBorderColor))
						cmd.Parameters.AddWithValue("@topframeborder", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@topframeborder", styles.TopFrameBorderColor);

					if (String.IsNullOrEmpty(styles.TableHeaderRowBackground))
						cmd.Parameters.AddWithValue("@tableheader", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@tableheader", styles.TableHeaderRowBackground);

					if (String.IsNullOrEmpty(styles.TableRowBackground))
						cmd.Parameters.AddWithValue("@tablerow", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@tablerow", styles.TableRowBackground);

					if (String.IsNullOrEmpty(styles.TableRowAlternateBackground))
						cmd.Parameters.AddWithValue("@tablealternaterow", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@tablealternaterow", styles.TableRowAlternateBackground);

					if (String.IsNullOrEmpty(styles.TableRowBackground))
						cmd.Parameters.AddWithValue("@cellborder", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@cellborder", styles.TableCellBorder);

					if (String.IsNullOrEmpty(styles.TablePadding))
						cmd.Parameters.AddWithValue("@tablepadding", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@tablepadding", styles.TablePadding);

					if (String.IsNullOrEmpty(styles.FontSize))
						cmd.Parameters.AddWithValue("@fontsize", DBNull.Value);
					else
						cmd.Parameters.AddWithValue("@fontsize", styles.FontSize);

					cmd.ExecuteNonQuery();
				}

			}
		}

		public List<string> GetSavedStyles()
		{
			var results = new List<string>();
			var sql = @"select Username from UserStyles where IsSavedStyle=1";

			using (var conn = new SqlConnection(mainDb))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql, conn))
				{
					using (var rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							results.Add(GetVal(rdr["Username"]));
						}
					}
				}
			}

			return results;
		}
		
		public StyleObj GetStylesForUser(string username, bool loadDefaultIfNoUser = false)
		{
			var styles = new StyleObj() { ContainsCustomStyles = false };
			var sql = @"select * from UserStyles where Username=@user";
			var sql2 = @"select * from UserStyles where Username='default'";

			using (var conn = new SqlConnection(mainDb))
			{
				conn.Open();
				using (var cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@user", username);
					using (var rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							styles.ContainsCustomStyles = true;
							styles.BackgroundColor = GetVal(rdr["BackgroundColor"]);
							styles.TableRowBackground = GetVal(rdr["TableRowBackground"]);
							styles.TableRowAlternateBackground = GetVal(rdr["TableRowAlternateBackground"]);
							styles.TableCellBorder = GetVal(rdr["TableCellBorder"]);
							styles.TableHeaderRowBackground = GetVal(rdr["TableHeaderRowBackground"]);
							styles.FontFamily = GetVal(rdr["FontFamily"]);
							styles.MainLinksColor = GetVal(rdr["MainLinksColor"]);
							styles.TitleColor = GetVal(rdr["TitleColor"]);
							styles.TopFrameBorderColor = GetVal(rdr["TopFrameBorderColor"]);
							styles.TopFrameColor = GetVal(rdr["TopFrameColor"]);
							styles.TablePadding = GetVal(rdr["TablePadding"]);
							styles.FontSize = GetVal(rdr["FontSize"]);
						}
					}
				}
				if (!styles.ContainsCustomStyles && loadDefaultIfNoUser)
				{
					using (var cmd = new SqlCommand(sql2, conn))
					{
						using (var rdr = cmd.ExecuteReader())
						{
							while (rdr.Read())
							{
								styles.ContainsCustomStyles = true;
								styles.BackgroundColor = GetVal(rdr["BackgroundColor"]);
								styles.TableRowBackground = GetVal(rdr["TableRowBackground"]);
								styles.TableCellBorder = GetVal(rdr["TableCellBorder"]);
								styles.TableHeaderRowBackground = GetVal(rdr["TableHeaderRowBackground"]);
								styles.FontFamily = GetVal(rdr["FontFamily"]);
								styles.MainLinksColor = GetVal(rdr["MainLinksColor"]);
								styles.TitleColor = GetVal(rdr["TitleColor"]);
								styles.TopFrameBorderColor = GetVal(rdr["TopFrameBorderColor"]);
								styles.TopFrameColor = GetVal(rdr["TopFrameColor"]);
								styles.TablePadding = GetVal(rdr["TablePadding"]);
								styles.FontSize = GetVal(rdr["FontSize"]);
							}
						}
					}
				}
			}
			return styles;
		}

		private string GetVal(object obj)
		{
			if (obj == null)
				return "";
			else
				return obj.ToString();
		}
	}
}