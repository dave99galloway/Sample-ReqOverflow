using System.Collections.Generic;
using OpenQA.Selenium;

namespace SimpleSeleniumFramework.Support
{
    public class BrowserUser
    {
        public string Name { get; }
        public IWebDriver Driver { get; }
        public Dictionary<string, object> Context { get; } = new Dictionary<string, object>();

        public BrowserUser(string name, IWebDriver driver)
        {
            Name = name;
            Driver = driver;
        }
    }
}
