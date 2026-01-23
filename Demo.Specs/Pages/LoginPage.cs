using System;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;

namespace Demo.Specs.Pages
{
    public class LoginPage : PageObject
    {
        public LoginPage(BrowserUser user, Func<IWebElement>? rootResolver = null) : base(user, rootResolver)
        {
        }

        public IWebElement UsernameField => Find(By.Id("username"));
        public IWebElement PasswordField => Find(By.Id("password"));
        public IWebElement LoginButton => Find(By.CssSelector("button[type='submit']"));
        public IWebElement FlashMessage => Find(By.Id("flash"));
    }
}
