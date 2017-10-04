using System.Web.Mvc;

namespace LiveScoresWeb.Entities
{
	public class StyleEditPageObj
	{
		public StyleObj Styles { get; set; }
		public SelectList DefinedThemes { get; set; }
		public string ChosenStyle { get; set; }
	}
}