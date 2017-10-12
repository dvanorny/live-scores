using System.Data;
using System.Web.Mvc;

namespace LiveScoresWeb.Entities
{
	public class QueryObj
	{
		public string QuerySql { get; set; }
		public string ResultsTable { get; set; }
		public DataTable ResultsDataTable { get; set; }
		public int QueryId { get; set; }
		public string QueryName { get; set; }
		public string QueryDescription { get; set; }
		public SelectList SavedQueries { get; set; }
	}
}