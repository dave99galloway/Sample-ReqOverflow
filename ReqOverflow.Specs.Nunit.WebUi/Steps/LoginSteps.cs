using OpenQA.Selenium;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;
using ReqOverflow.Specs.Nunit.WebUi.Pages;

namespace ReqOverflow.Specs.Nunit.WebUi.Steps;

[Binding]
public class LoginSteps(ScenarioContext scenarioContext)
{
    [Then("{string} is on the Home page")]
    [Given("{string} is on the Home page")]
    public static void GivenIsOnTheHomePage(IWebDriver actor) => actor.OnHomePage();

    [When("{string} hits Login")]
    public static void WhenHitsLogin(IWebDriver actor) => actor.OnHomePage().OpenLoginPage();

    [Then("{string} can see the Login page has opened")]
    public static void ThenCanSeeTheLoginPageHasOpened(IWebDriver actor) => actor.OnLoginPage();

    [Given("{string} has opened the Home page")]
    public static void GivenHasOpenedTheHomePage(IWebDriver actor) => actor.OpenHomePage();

    [Given("{string} has opened the Login page")]
    public static void GivenHasOpenedTheLoginPage(IWebDriver actor) => actor.OpenLoginPage();

    [When("{string} logs in with password {string}")]
    public void WhenLogsInWithPassword(IWebDriver actor, string password)
    {
        var service = scenarioContext.ScenarioContainer.Resolve<WebDriverService>();
        actor.OnLoginPage().Login(username: service.GetDriverUser(actor), password: password);
    }
}