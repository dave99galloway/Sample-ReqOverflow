using OpenQA.Selenium;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Contexts;
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
    public void WhenLogsInWithPassword(IWebDriver actor, string password) => actor.OnLoginPage()
        .Login(username: scenarioContext.GetDriverUser(actor), password: password);

    [Then("{string} can see they are the logged in user")]
    public void ThenCanSeeTheyAreTheLoggedInUser(IWebDriver actor) =>
        actor.OnHomePage().AssertLoggedInUserIs(scenarioContext.GetDriverUser(actor));

    [Given("{string} has logged in")]
    public void GivenHasLoggedIn(IWebDriver actor) => actor.OpenHomePage().OpenLoginPage()
        .Login(username: scenarioContext.GetDriverUser(actor),
            //just hard code for now - can get data for users from somewhere else later
            password: "r)pF0*50oBs4");

    [When("{string} logs out")]
    public static void WhenLogsOut(IWebDriver actor) => actor.OnHomePage().LogOutUser();

    [Then("{string} can see the logged in user is anonymous")]
    public void ThenCanSeeTheLoggedInUserIsAnonymous(IWebDriver actor) =>
        actor.OnHomePage().AssertLoggedInUserIs("anonymous");
}