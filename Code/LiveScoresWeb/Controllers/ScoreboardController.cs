using System.Linq;
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
			var myBets = vm.GetScoreboardBets();
			var includesNflGame = myBets.Any(bet => bet.Sport.ToUpper() == "NFL");
			if (includesNflGame)
			{
				var nflScores = vm.GetNflScores();
				myBets = vm.CreateLiveUpdate(myBets, nflScores);
			}

			return View(myBets);
		}
	}
}
