
using Reqnroll;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;
using ReqOverflow.Specs.Nunit.WebUi.Pages;

namespace ReqOverflow.Specs.Nunit.WebUi.Hooks;

[Binding]
public class DriverHooks(WebDriverService driverService)
{
    [AfterScenario]
    public void AfterScenario() => driverService.QuitAllDrivers(driver=>
    {
       var  homePage = new HomePage(driver);
           if (homePage.IsLoggedIn)
            homePage.LogOutUser();
    });
}