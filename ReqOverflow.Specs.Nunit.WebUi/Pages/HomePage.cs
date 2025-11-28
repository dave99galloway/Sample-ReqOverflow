using OpenQA.Selenium;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ReqOverflow.Specs.Nunit.WebUi.Pages;

public class HomePage(IWebDriver driver)
{
    // for increased reliability make this a func that returns an IWebElement so it can "never" be stale.
    // as we get more complex pass a root element in from the parent, define sa  root for the page and make sure these methods and eleemtns are found from those
    // some frameworks wrap the IWebElement and a string description to make it easier to identify which elements are being used/have failed. may also store the By separately for more options around using supported methods for By insted of element
    
    private IWebElement Login => driver.FindElement(By.CssSelector("li > a[href='/Login']"));

    private IWebElement LoggedInUser => driver.FindElement(By.CssSelector("div#UserInfo span"));
    
    private IWebElement LogOut => driver.FindElement(By.LinkText("Logout"));

    public LoginPage OpenLoginPage()
    {
        driver.Wait().Until(_ => driver.GetElementIf(ElementToBeClickable(Login))).Click();
        return new LoginPage(driver);
    }

    public HomePage AssertLoggedInUserIs(string expectedUser)
    {
        Assert.That(
            driver.Wait().Until(_ => LoggedInUser.Displayed && LoggedInUser.Text == expectedUser ? LoggedInUser : null)
                .Text, Is.EqualTo(expectedUser));
        return this;
    }

    public HomePage LogOutUser()
    {
        driver.Wait().Until(_ => driver.GetElementIf(ElementToBeClickable(LogOut))).Click();

        return this;
    }
}

public static class HomePageFactory
{
    private static readonly Predicate<IWebDriver> TitlePredicate =
        driver => driver.Title.Contains("home", StringComparison.InvariantCultureIgnoreCase);

    public static HomePage OnHomePage(this IWebDriver driver)
    {
        var homePage = new HomePage(driver);
        driver.Wait().Until(_ => TitlePredicate);
        return homePage;
    }

    public static HomePage OpenHomePage(this IWebDriver driver)
    {
        if (!TitlePredicate(driver))
            driver.Navigate().GoToUrl("http://127.0.0.1:5000");
        return driver.OnHomePage();
    }
}