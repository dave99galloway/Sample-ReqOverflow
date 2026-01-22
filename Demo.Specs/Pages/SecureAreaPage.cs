using System;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Pages;

namespace Demo.Specs.Pages
{
    public class SecureAreaPage : PageObject
    {
        public SecureAreaPage(Func<IWebElement> rootResolver) : base(rootResolver)
        {
        }

        public IWebElement LogoutButton => Find(By.CssSelector("a.button.secondary"));
        public IWebElement Header => Find(By.TagName("h2"));
    }
}
