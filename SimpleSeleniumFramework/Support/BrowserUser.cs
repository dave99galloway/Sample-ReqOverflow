using OpenQA.Selenium;

namespace SimpleSeleniumFramework.Support
{
    public class BrowserUser(string name, IWebDriver driver)
    {
        public string Name { get; } = name;
        public IWebDriver Driver { get; } = driver;
        public Dictionary<string, object> Context { get; } = [];
    }
}
