using OpenQA.Selenium;

namespace SimpleSeleniumFramework.Support;

public class BrowserUser(string name, IWebDriver driver)
{
    public string Name { get; } = name;
    public IWebDriver Driver { get; } = driver;
    public Dictionary<string, object> Context { get; } = [];
}
public static class BrowserUserExtensions
{
    public static Func<IWebElement> FindBodySelector(this BrowserUser user) => () => user.Driver.FindElement(By.TagName("body"));

}


