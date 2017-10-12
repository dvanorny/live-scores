using System.Web.Mvc;
using LiveScoresWeb.ViewModels;

namespace LiveScoresWeb.Controllers
{
	public class BaseController : System.Web.Mvc.Controller
	{
		public BaseController()
		{
		}

		protected override void OnActionExecuting(ActionExecutingContext ctx)
		{
			var user = User.Identity.Name;
			//var view = SharedVM.DisplayBannerBit(user) ? "contents" : "none";
			//ViewBag.DisplayBanner = view;
		}

		public ActionResult CloseBanner()
		{
			var vm = new SharedVM();
			vm.CloseBanner(User.Identity.Name);
			return Redirect(Request.UrlReferrer.PathAndQuery);
		}
	}
}