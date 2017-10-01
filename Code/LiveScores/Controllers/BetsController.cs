using System.Web.Configuration;
using System.Web.Mvc;
using LiveScores.ViewModels;

namespace LiveScores.Controllers
{
	public class BetsController : BaseController
	{
		public ActionResult Index()
		{
			SharedVM.LogPageHit("Bets/Index", User.Identity.Name);

			var vm = new BetsVM(WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString);
			var betsList = vm.GetAllBets();
			ViewBag.BetCount = betsList.Count;
			return View(betsList);
		}
	}
}
