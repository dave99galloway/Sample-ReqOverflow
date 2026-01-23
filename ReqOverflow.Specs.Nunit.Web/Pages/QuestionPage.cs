using System;
using OpenQA.Selenium;
using ReqOverflow.Specs.Nunit.Web.Steps;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;

namespace ReqOverflow.Specs.Nunit.Web.Pages
{
    public class QuestionPage(BrowserUser user, Func<IWebElement>? rootResolver = null)
    : PageObject(user, rootResolver ?? (() => user.Driver.FindElement(By.Id("ask"))))
    {
        private IWebElement Title => Find(By.Id("TitleInput"));
        private IWebElement Body => Find(By.Id("BodyInput"));
        private IWebElement Tags => Find(By.Id("Tags"));
        private IWebElement Post => Find(By.Id("PostQuestionButton"));

        private IWebElement errorMessage => Find(By.Id("ErrorMessage"));

        public QuestionPage Visit()
        {
            var baseUrl = User.Configuration["BaseUrl"] ?? "http://localhost:5000";
            User.Driver.Navigate().GoToUrl($"{baseUrl}/Ask");
            return this;
        }

        public QuestionPage PostQuestion(QuestionData question)
        {
            Title.SendKeys(question.Title);
            Body.SendKeys(question.Body);
            Tags.SendKeys(question.Tags);
            Post.Click();
            return this;
        }
        public ErrorMessage ErrorMessage => new(Text: errorMessage.Text);
    }

    public record ErrorMessage(string Text)
    {
    }
}
