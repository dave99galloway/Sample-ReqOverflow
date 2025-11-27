using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ReqOverflow.Specs.Nunit.WebUi.Drivers;

public static class WebDriverExtensions
{
    
    public static WebDriverWait Wait(this IWebDriver driver, TimeSpan? @for = null)
    {
        //get timeout from config later
        var wait = new WebDriverWait(driver, @for ?? TimeSpan.FromSeconds(5));
        return wait;
    }
}