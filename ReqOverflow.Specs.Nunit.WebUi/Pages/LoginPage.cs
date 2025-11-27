using OpenQA.Selenium;
using ReqOverflow.Specs.Nunit.WebUi.Drivers;

namespace ReqOverflow.Specs.Nunit.WebUi.Pages;

public class LoginPage(IWebDriver driver)
{
    private IWebElement UserName => driver.FindElement(By.CssSelector("form#LoginForm > p > input#Name"));

    private IWebElement Password => driver.FindElement(By.CssSelector("form#LoginForm > p > input#Password"));

    private IWebElement Submit => driver.FindElement(By.CssSelector("form#LoginForm > p > input#LoginButton"));
    
    
    public HomePage Login(string username, string password)
    {
        UserName.SendKeys(username);
        Password.SendKeys(password);
        Submit.Click();
        return driver.OnHomePage();
    }
}

public static class LoginPageFactory
{
    private static readonly Predicate<IWebDriver> TitlePredicate = driver =>   driver.Title.Contains("login", StringComparison.InvariantCultureIgnoreCase);

    public static LoginPage OnLoginPage(this IWebDriver driver)
    {
        var loginPage = new LoginPage(driver);
        driver.Wait().Until(_ => TitlePredicate);

        return loginPage;
    }
    
    public static LoginPage OpenLoginPage(this IWebDriver driver)
    {
        if (!TitlePredicate(driver))
            driver.Navigate().GoToUrl("http://localhost:5000/Login");
        return driver.OnLoginPage();
    }
}