using System.Diagnostics.CodeAnalysis;

using Bunit;

using RH.Apps.Web.SPA.Client.Pages;

using Xunit;

namespace RH.Apps.Web.SPA.Lite.Tests
{
	[ExcludeFromCodeCoverage]
	/// <summary>
	/// These tests are written entirely in C#.
	/// Learn more at https://bunit.egilhansen.com/docs/
	/// </summary>
	public class CounterShould
		: TestContext
	{
		[Fact(DisplayName = "Starts at 0")]
		public void StartAtZero()
		{
			// Arrange
			var cut = RenderComponent<Counter>();

			// Assert that content of the paragraph shows counter at zero
			cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
		}

		[Fact(DisplayName = "Clicking button increments counter")]
		public void ClickingButtonIncrementsCounter()
		{
			// Arrange
			var cut = RenderComponent<Counter>();

			// Act - click button to increment counter
			cut.Find("button").Click();

			// Assert that the counter was incremented
			cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
		}
	}
}
