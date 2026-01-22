using System;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;

namespace Demo.Specs.Pages
{
    public class SecureAreaPage : PageObject
    {
        protected SecureAreaPage(BrowserUser user, Func<IWebElement>? rootResolver = null) : base(user, rootResolver)
        {
        }

        public IWebElement LogoutButton => Find(By.CssSelector("a.button.secondary"));
        public IWebElement Header => Find(By.TagName("h2"));
    }
}
