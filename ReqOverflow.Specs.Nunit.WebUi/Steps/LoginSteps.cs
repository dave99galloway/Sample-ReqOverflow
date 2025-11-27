using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;

namespace ReqOverflow.Specs.Nunit.WebUi.Steps;

[Binding]
public class LoginSteps
{
    [Given("{string} is on the {string} page")]
    public void GivenIsOnThePage(IWebDriver actor, string page)
    {
        if (!actor.Title.Contains(page, StringComparison.InvariantCultureIgnoreCase))
            actor.Navigate().GoToUrl("http://localhost:5000");
        var wait = new WebDriverWait(actor, TimeSpan.FromSeconds(5));
        wait.IgnoreExceptionTypes(typeof(AssertionException));
        wait.Until(driver =>
        {
            Assert.That(driver.Title, Does.Contain("page").IgnoreCase);
            return true;
        });
    }

    [When("{string} hits Login")]
    public void WhenHitsLogin(string marvin)
    {
        ScenarioContext.StepIsPending();
    }

    [Then("{string} is on the {string} page")]
    public void ThenIsOnThePage(string actor, string page)
    {
        ScenarioContext.StepIsPending();
    }
}