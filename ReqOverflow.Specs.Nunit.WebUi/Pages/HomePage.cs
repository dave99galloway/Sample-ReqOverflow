using OpenQA.Selenium;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Pages;

public class HomePage(IWebDriver driver)
{
    // for increased reliability make this a func that returns an IWebElement so it can "never" be stale.
    // as we get more complex pass a root element in from the parent, define sa  root for the page and make sure these methods and eleemtns are found from those
    private IWebElement Login => driver.FindElement(By.CssSelector("li > a[href='/Login']"));

    public LoginPage OpenLoginPage()
    {
        driver.Wait().Until(_ => Login).Click();
        return new LoginPage(driver);
    }
}

public static class HomePageFactory
{
    public static HomePage OnHomePage(this IWebDriver driver)
    {   
        var homePage = new HomePage(driver);
        driver.Wait().UntilAssertionPasses(_ =>
        {
            Assert.That(driver.Title, Does.Contain("home").IgnoreCase);
            return driver.Title;
        });
        return homePage;
    }
    
    public static HomePage OpenHomePage(this IWebDriver driver)
    {
        
        if (!driver.Title.Contains("home", StringComparison.InvariantCultureIgnoreCase))
            driver.Navigate().GoToUrl("http://localhost:5000");
        return driver.OnHomePage();
    }
}
