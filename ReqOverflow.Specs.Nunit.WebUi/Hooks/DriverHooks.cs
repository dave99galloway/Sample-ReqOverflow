
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Hooks;

[Binding]
public class DriverHooks(WebDriverService driverService)
{
    [AfterScenario]
    public void AfterScenario() => driverService.QuitAllDrivers();
}