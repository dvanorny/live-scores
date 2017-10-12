using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using LiveScoresWeb.Entities;
using LiveScoresWeb.ViewModels;

namespace LiveScoresWeb.Controllers
{
	[Authorize]
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

		public ActionResult Cancel()
		{
			return RedirectToAction("Index");
		}

		public ActionResult Add()
		{
			SharedVM.LogPageHit("Bets/Add", User.Identity.Name);
			var newBet = new BetObj();
			newBet.AB = true;
			newBet.JK = true;
			newBet.CL = true;
			newBet.AT = true;
			newBet.DV = true;
			newBet.BetDate = DateTime.Today;
			newBet.GroupBet = "Y";
			newBet.Sport = "NFL";

			GetDataForDropdowns(ref newBet);
			return View(newBet);
		}

		[HttpPost]
		public ActionResult Add(BetObj myBet)
		{
			if (ModelState.IsValid)
			{
				SharedVM.LogPageHit("Bets/Add/Save (" + myBet.BetId + ")", User.Identity.Name);

				var vm = new BetsVM(dbConn);
				vm.AddNewBet(myBet);

				return RedirectToAction("Index");
			}
			else
				return View();
		}

		public ActionResult Edit(int id)
		{
			SharedVM.LogPageHit("Bets/Edit/" + id, User.Identity.Name);

			var vm = new BetsVM(dbConn);
			var thisBet = vm.GetSingleBet(id);
			GetDataForDropdowns(ref thisBet);
			return View(thisBet);
		}

		public void GetDataForDropdowns(ref BetObj thisBet)
		{
			var myList = new List<OutcomeObj>
			{
				new OutcomeObj() {OutcomeId = "", Description = ""},
				new OutcomeObj() {OutcomeId = "W", Description = "Win"},
				new OutcomeObj() {OutcomeId = "L", Description = "Loss"},
				new OutcomeObj() {OutcomeId = "P", Description = "Push"}
			};

			thisBet.OutcomesList = myList;
		}

		[HttpPost]
		public ActionResult Edit(BetObj myBet)
		{
			SharedVM.LogPageHit("Bets/Edit/Save(" + myBet.BetId + ")", User.Identity.Name);

			var vm = new BetsVM(dbConn);
			if (myBet.BetId > 0)
			{
				vm.UpdateBetItem(myBet);
			}
			
			return RedirectToAction("Edit", myBet.BetId);
		}

		[HttpPost]
		public ActionResult SaveAndNew(BetObj myBet)
		{
			SharedVM.LogPageHit("Bets/Edit/Save(" + myBet.BetId + ")", User.Identity.Name);

			var vm = new BetsVM(dbConn);
			if (myBet.BetId > 0)
			{
				vm.UpdateBetItem(myBet);
			}
			
			return RedirectToAction("Add");
		}
		
		public ActionResult Delete(int id)
		{
			SharedVM.LogPageHit("Bets/Delete(" + id + ")", User.Identity.Name);

			var vm = new BetsVM(dbConn);
			if (id > 0)
			{
				vm.DeleteBetItem(id);
			}

			return RedirectToAction("Index");
		}
	}
}
