using System;
using OpenQA.Selenium;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;

namespace ReqOverflow.Specs.Nunit.Web.Pages
{
    public class QuestionPage : PageObject
    {
        public QuestionPage(BrowserUser user, Func<IWebElement>? rootResolver = null)
            : base(user, rootResolver ?? (() => user.Driver.FindElement(By.Id("ask"))))
        {
        }

        public IWebElement Title => Find(By.Id("TitleInput"));
        public IWebElement Body => Find(By.Id("BodyInput"));
        public IWebElement Tags => Find(By.Id("Tags"));
        public IWebElement Post => Find(By.Id("PostQuestionButton"));

        public IWebElement ErrorMessage => Find(By.Id("ErrorMessage"));

        public QuestionPage Visit()
        {
            var baseUrl = User.Configuration["BaseUrl"] ?? "http://localhost:5000";
            User.Driver.Navigate().GoToUrl($"{baseUrl}/Ask");
            return this;
        }
    }
}
