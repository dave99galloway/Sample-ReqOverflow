using OpenQA.Selenium;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Contexts;

public static class ScenarioContextExtensions
{
    public static string GetDriverUser(this ScenarioContext scenarioContext, IWebDriver driver)
    {
        var service = scenarioContext.ScenarioContainer.Resolve<WebDriverService>();
        return service.GetDriverUser(driver);
    }
}