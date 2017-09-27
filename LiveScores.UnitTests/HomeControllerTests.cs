using System;
using LiveScores.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveScores.UnitTests
{
	[TestClass]
	public class HomeControllerTests
	{
		[TestMethod]
		public void Test_GetNflScores()
		{
			var obj = new HomeController();
			var result = obj.GetNflScores();
			Assert.IsTrue(result.Count > 0);
		}
	}
}
