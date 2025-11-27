using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;
using ReqOverflow.Specs.Nunit.WebUi.Pages;

namespace ReqOverflow.Specs.Nunit.WebUi.Steps;

[Binding]
public static class LoginSteps
{
    [Given("{string} is on the Home page")]
    public static void GivenIsOnTheHomePage(IWebDriver actor) => actor.OpenHomePage();

    [When("{string} hits Login")]
    public static void WhenHitsLogin(IWebDriver actor) => actor.OnHomePage().OpenLoginPage();

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