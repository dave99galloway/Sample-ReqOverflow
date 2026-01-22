
using OpenQA.Selenium;
using Reqnroll;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;

namespace ReqOverflow.Specs.Nunit.Web.Steps;

[Binding]
public static class QuestionSteps
{
  [Given("{string} is on the questions page")]
  public static void GivenIsOnTheQuestionsPage(BrowserUser user)
  {
    //todo: add config to user so we can read the url from appsettings etc.
    //todo: add this diect nav to the questions page object
    user.Driver.Navigate().GoToUrl("http://localhost:5000/Ask");
  }

  [When("{string} submits a question")]
  public static void WhenSubmitsAQuestion(BrowserUser user, DataTable dataTable)
  {
    //todo: extract body tag to core framework
    var questionPage = new QuestionPage(() => user.Driver.FindElement(By.TagName("body")));
    //todo: move to data entry method in page object taking question data
    questionPage.Title.SendKeys("data");
    questionPage.Body.SendKeys("body");
    questionPage.Tags.SendKeys("tag");
    questionPage.Post.Click();
  }

  [Then("{string} sees an error")]
  public static void ThenSeesAnError(BrowserUser user)
  {
    var questionPage = new QuestionPage(() => user.Driver.FindElement(By.TagName("body")));
    Assert.That(questionPage.ErrorMessage.Text, Is.EqualTo("Not logged in"));
  }
}

public class QuestionPage(Func<IWebElement> rootResolver) : PageObject(() => rootResolver().FindElement(By.Id("ask")))
{
  public IWebElement Title => Find(By.Id("TitleInput"));
  public IWebElement Body => Find(By.Id("BodyInput"));
  public IWebElement Tags => Find(By.Id("Tags"));
  public IWebElement Post => Find(By.Id("PostQuestionButton"));

  public IWebElement ErrorMessage => Find(By.Id("ErrorMessage"));
}