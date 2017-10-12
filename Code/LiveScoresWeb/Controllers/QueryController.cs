using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using LiveScoresWeb.Entities;
using LiveScoresWeb.ViewModels;

namespace LiveScoresWeb.Controllers
{
	[Authorize]
	[RequireHttps]
	public class QueryController : BaseController
	{
		public ActionResult Index()
		{
			SharedVM.LogPageHit("Query/Index", User.Identity.Name);
			var queryObj = new QueryObj() { QuerySql = "" };
			return View(queryObj);
		}

		public ActionResult Scripts()
		{
			SharedVM.LogPageHit("Query/Scripts/", User.Identity.Name);
			var queryObj = new QueryObj()
			{
				SavedQueries = LoadSavedQueries()
			};
			var vm = new QueryVM();
			return View(queryObj);
		}

		[HttpPost]
		public ActionResult Scripts(QueryObj queryObj)
		{
			int queryId = queryObj.QueryId;
			SharedVM.LogPageHit("Query/RunSavedQuery/" + queryId, User.Identity.Name);

			var dt = new DataTable();
			var vm = new QueryVM();
			queryObj.QuerySql = vm.GetSavedQuerySql(queryId);
			queryObj.QueryDescription = vm.GetSavedQueryDesc(queryId);

			try
			{
				var sqlResults = vm.ExecuteSql(queryObj);

				if (sqlResults != null)
				{
					BuildTableHeader(sqlResults, false, ref dt);

					if (sqlResults != null && sqlResults.Any())
					{
						foreach (var dataRow in sqlResults.ToList())
						{
							DataRow row = dt.NewRow();
							int rowNum = 0;
							foreach (var item in dataRow)
							{
								row[rowNum] = item.Value;
								rowNum++;
							}
							dt.Rows.Add(row);
						}

					}
					queryObj.ResultsDataTable = dt;
				}
			}
			catch (Exception)
			{
				//Should probably do something with this, but for now just swallowing it
			}

			queryObj.SavedQueries = LoadSavedQueries();
			return View(queryObj);
		}

		private SelectList LoadSavedQueries()
		{
			var vm = new QueryVM();
			var items = new List<SelectListItem>();
			foreach (var obj in vm.GetSavedQueries())
			{
				items.Add(new SelectListItem { Text = obj.Value, Value = obj.Key.ToString() });
			}

			return new SelectList(items, "Value", "Text");
		}
		
		private void BuildTableHeader(IEnumerable<dynamic> sqlResults, bool includeServerCols, ref DataTable dt)
		{
			var dataRow = sqlResults.First();
			if (includeServerCols)
			{
				dt.Columns.Add(new DataColumn("Server", typeof(string)));
				dt.Columns.Add(new DataColumn("Database", typeof(string)));
			}
			foreach (var item in dataRow)
			{
				if (item.Key.ToString().ToLower() == "cnt" || item.Key.ToString().ToLower() == "count")
				{
					dt.Columns.Add(new DataColumn(item.Key, typeof(int)));
				}
				else
				{
					dt.Columns.Add(new DataColumn(item.Key, typeof(string)));
				}
			}
		}
	}
}
