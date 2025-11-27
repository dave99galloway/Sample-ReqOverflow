using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Steps;

[Binding]
public static class LoginSteps
{
    //this step def is too fat and attempts to be multipurpose but actually isn't -> make ita specific stp to check we are on the login page instead
    [Given("{string} is on the {string} page")]
    public static void GivenIsOnThePage(IWebDriver actor, string page)
    {
        if (!actor.Title.Contains(page, StringComparison.InvariantCultureIgnoreCase))
            actor.Navigate().GoToUrl("http://localhost:5000");

        actor.Wait().UntilAssertionPasses(driver =>
        {
            Assert.That(driver.Title, Does.Contain(page).IgnoreCase);
            return driver.Title;
        });
    }

    [When("{string} hits Login")]
    public static void WhenHitsLogin(IWebDriver actor)
    {
        actor.Wait().Until(driver => driver.FindElement(By.CssSelector("li > a[href='/Login']"))).Click();
    }

    [Then("{string} is on the {string} page")]
    public static void ThenIsOnThePage(IWebDriver actor, string page)
    {
        actor.Wait().UntilAssertionPasses(driver =>
        {
            Assert.That(driver.Title, Does.Contain(page).IgnoreCase);
            return driver.Title;
        });
    }
}