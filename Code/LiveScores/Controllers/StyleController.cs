using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LiveScores.Controllers;
using LiveScores.Entities;
using LiveScores.ViewModels;

namespace LiveScores.Controllers
{
	public class StyleController : BaseController
	{
		public string Index()
		{
			var user = User.Identity.Name;

			Response.ContentType = "text/css";
			var vm = new StyleVM();
			var styles = vm.GetStylesForUser(user, true);

			return BuildCustomStyleSheet(styles);

			//if (styles.ContainsCustomStyles)
			//	return BuildCustomStyleSheet(styles);
			//else
			//	return System.IO.File.ReadAllText(Server.MapPath("~/Content/Custom.css"));
		}

		public ActionResult Edit()
		{
			var user = User.Identity.Name;
			SharedVM.LogPageHit("Style/Edit", user);

			var styleType = user;
			if (TempData["TmpStyle"] != null)
			{
				styleType = TempData["TmpStyle"].ToString();
			}
			var vm = new StyleVM();
			var pageObj = new StyleEditPageObj()
			{
				Styles = vm.GetStylesForUser(styleType, true),
				DefinedThemes = LoadSavedStyles()
			};
			return View(pageObj);
		}

		private SelectList LoadSavedStyles()
		{
			var vm = new StyleVM();
			var items = new List<SelectListItem>();
			foreach (var style in vm.GetSavedStyles())
			{
				items.Add(new SelectListItem { Text = style, Value = style });
			}

			return new SelectList(items, "Value", "Text");
		}

		[HttpPost]
		public ActionResult Edit(StyleEditPageObj myStyles)
		{
			var user = User.Identity.Name;
			SharedVM.LogPageHit("Elite/Edit/SaveStyles", user);

			var vm = new StyleVM();
			vm.SaveStylesForUser(myStyles.Styles, user);
			var pageObj = new StyleEditPageObj()
			{
				Styles = vm.GetStylesForUser(user, true),
				DefinedThemes = LoadSavedStyles()
			};
			return View(pageObj);
		}

		[HttpPost]
		public ActionResult LoadStyle(StyleEditPageObj myStyles)
		{
			TempData["TmpStyle"] = myStyles.ChosenStyle;

			//var result = new StyleController { tmpStyleChosen = myStyles.ChosenStyle };
			//result.Edit();

			return RedirectToAction("Edit");
		}

		private string BuildCustomStyleSheet(StyleObj styles)
		{
			var newStyle = @"
			body {
				background:inherit;
				background-color: @bgcolor;
				font-family: @fontfamily;
				font-size: @fontsizepx;
			}

			body {
				padding-bottom: 20px;
			}

			/*Background Color of table header row*/
			.hoverTable th {
				background: @tableheaderbkgd;
			}

			/*Background Color of table row*/
			.hoverTable tr {
				background: @tablerowbkgd;
			}

			/*Background Color of alternate table row*/
			.hoverTable tr:nth-child(even) {
				background: @tablealternaterowbkgd;
			}

			/*Border color of each table cell*/
			.hoverTable td {
				border: @tablecellborder 1px solid;
			}

			/*Color of thead main LiveScores logo in top-left*/
			.navbar-inverse .navbar-brand {
				color: @titlecolor;
			}

			/*Color of main links across the top frame*/
			.navbar-inverse .navbar-nav > li > a {
				color: @mainlinkscolor;
			}

			/*Background color of the top frame*/
			.navbar-inverse {
				background-color: @topframecolor;
				border-color: @topframebordercolor;
			}

			.body-content {
				padding-top: 15px;
				padding-left: 15px;
				padding-right: 15px;
			}

			.table > tbody > tr > td {
			  padding: @tablepaddingpx;
			}

			";

			newStyle = ReplaceText(newStyle, "@bgcolor", styles.BackgroundColor, "#334750");
			newStyle = ReplaceText(newStyle, "@tableheaderbkgd", styles.TableHeaderRowBackground, "#6FA8D9");
			newStyle = ReplaceText(newStyle, "@tablerowbkgd", styles.TableRowBackground, "#EDEEEF");
			newStyle = ReplaceText(newStyle, "@tablealternaterowbkgd", styles.TableRowAlternateBackground, "#FFFFFF");
			newStyle = ReplaceText(newStyle, "@tablecellborder", styles.TableCellBorder, "#4E95F4");
			newStyle = ReplaceText(newStyle, "@fontfamily", styles.FontFamily, "'Open Sans', sans-serif");
			newStyle = ReplaceText(newStyle, "@topframebordercolor", styles.TopFrameBorderColor, "#101010");
			newStyle = ReplaceText(newStyle, "@topframecolor", styles.TopFrameColor, "#333333");
			newStyle = ReplaceText(newStyle, "@mainlinkscolor", styles.MainLinksColor, "#9d9d9d");
			newStyle = ReplaceText(newStyle, "@titlecolor", styles.TitleColor, "Orange");
			newStyle = ReplaceText(newStyle, "@tablepadding", styles.TablePadding, "8");
			newStyle = ReplaceText(newStyle, "@fontsize", styles.FontSize, "14");

			return newStyle;
		}

		private string ReplaceText(string mainStr, string strToSearchFor, string replacementText, string defaultValue)
		{
			if (String.IsNullOrEmpty(replacementText))
			{
				return mainStr.Replace(strToSearchFor, defaultValue);
			}
			return mainStr.Replace(strToSearchFor, replacementText);
		}
	}
}