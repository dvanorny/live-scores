using System.Web.Mvc;
using LiveScoresWeb.ViewModels;

namespace LiveScoresWeb.Controllers
{
	[RequireHttps]
	public class ScoreboardController : BaseController
	{
		public ActionResult Index()
		{
			SharedVM.LogPageHit("Scoreboard/Index", User.Identity.Name);

			var vm = new ScoreboardVM();
			var gamesList = vm.GetNflScores();
			//ViewBag.SiteCount = results.Count();
			return View(gamesList);
		}
	}
}
