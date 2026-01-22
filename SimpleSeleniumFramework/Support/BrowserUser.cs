using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Pages;

namespace SimpleSeleniumFramework.Support;

public class BrowserUser(string name, IWebDriver driver, IConfiguration configuration)
{
    public string Name { get; } = name;
    public IWebDriver Driver { get; } = driver;
    public IConfiguration Configuration { get; } = configuration;
    public Dictionary<string, object> Context { get; } = [];
    public Dictionary<Type, PageObject> PageCache { get; } = [];
}

public static class BrowserUserExtensions
{
    public static Func<IWebElement> FindBodySelector(this BrowserUser user) => () => user.Driver.FindElement(By.TagName("body"));
}


