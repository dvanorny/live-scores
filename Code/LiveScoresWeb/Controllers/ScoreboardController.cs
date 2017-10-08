using System.Web.Configuration;
using System.Web.Mvc;
using LiveScoresWeb.ViewModels;

namespace LiveScoresWeb.Controllers
{
	[Authorize]
	[RequireHttps]
	public class ScoreboardController : BaseController
	{
		private string dbConn = WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
		
		public ActionResult Index()
		{
			SharedVM.LogPageHit("Scoreboard/Index", User.Identity.Name);

			var vm = new ScoreboardVM(dbConn);

			var gamesList = vm.GetNflScores();
			var nflBets = vm.GetNflBets();
			var liveList = vm.CreateLiveUpdate(nflBets, gamesList);

			return View(liveList);
		}
	}
}
