using OpenQA.Selenium;
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Transforms;

[Binding]
public class DriverTransform(WebDriverService driverService)
{
    // [StepArgumentTransformation("the (.*) browser")]
    // [StepArgumentTransformation("the driver for user (.*)")]
    [StepArgumentTransformation]
    public IWebDriver TransformUserStringToDriver(string userName) => driverService.GetDriverForUser(userName);
}