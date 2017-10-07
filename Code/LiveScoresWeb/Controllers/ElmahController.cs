using System.Web.Mvc;
using Elmah;

namespace LiveScoresWeb.Controllers
{
	public class ElmahController : Controller
	{
		public ActionResult Index(string type)
		{
			return new ElmahResult(type);
		}
	}
}