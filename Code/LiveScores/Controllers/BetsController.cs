using System.Web.Configuration;
using System.Web.Mvc;
using LiveScores.Entities;
using LiveScores.ViewModels;

namespace LiveScores.Controllers
{
	[RequireHttps]
	public class BetsController : BaseController
	{
		private string dbConn = WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;

		public ActionResult Index()
		{
			SharedVM.LogPageHit("Bets/Index", User.Identity.Name);

			var vm = new BetsVM(dbConn);
			var betsList = vm.GetAllBets();
			ViewBag.BetCount = betsList.Count;
			return View(betsList);
		}

		public ActionResult Edit(int id)
		{
			SharedVM.LogPageHit("Bets/Edit/" + id, User.Identity.Name);

			var vm = new BetsVM(dbConn);
			var thisSite = vm.GetSingleBet(id);
			return View(thisSite);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(BetObj myBet)
		{
			SharedVM.LogPageHit("Bets/Edit/Save(" + myBet.BetId + ")", User.Identity.Name);

			var vm = new BetsVM(dbConn);
			if (myBet.BetId > 0)
			{
				vm.UpdateBetItem(myBet);
			}

			var thisSite = vm.GetSingleBet(myBet.BetId);
			return View(thisSite);
		}
	}
}
