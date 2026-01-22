using System;
using OpenQA.Selenium;

namespace SimpleSeleniumFramework.Pages
{
    public abstract class PageObject
    {
        private readonly Func<IWebElement> _rootResolver;

        protected PageObject(Func<IWebElement> rootResolver)
        {
            _rootResolver = rootResolver;
        }

        protected IWebElement Root => _rootResolver();

        protected IWebElement Find(By locator)
        {
            return Root.FindElement(locator);
        }
    }
}
