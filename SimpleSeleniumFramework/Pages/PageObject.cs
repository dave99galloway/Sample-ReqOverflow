using System;
using OpenQA.Selenium;

namespace SimpleSeleniumFramework.Pages
{
    public abstract class PageObject(Func<IWebElement> rootResolver)
    {
        private readonly Func<IWebElement> _rootResolver = rootResolver;

        protected IWebElement Root => _rootResolver();

        protected IWebElement Find(By locator) => Root.FindElement(locator);
    }

}
