using System;
using System.Reflection;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Support;

namespace SimpleSeleniumFramework.Pages
{
    public abstract class PageObject(BrowserUser user, Func<IWebElement>? rootResolver = null)
    {
        protected readonly BrowserUser User = user;
        protected readonly Func<IWebElement> RootResolver = rootResolver ?? user.FindBodySelector();

        protected IWebElement Root => RootResolver();

        protected IWebElement Find(By locator) => Root.FindElement(locator);
    }

    public static class PageFactory
    {
        public static T Page<T>(this BrowserUser user) where T : PageObject
        {
            if (user.PageCache.TryGetValue(typeof(T), out var cachedPage))
            {
                return (T)cachedPage;
            }

            var newPage = (T)Activator.CreateInstance(typeof(T), 
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, 
                null, 
                [user, null], 
                null)!;

            user.PageCache[typeof(T)] = newPage;
            return newPage;
        }
    }
}
