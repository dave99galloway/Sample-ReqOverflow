using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Reqnroll;
using SimpleSeleniumFramework.Pages;
using SimpleSeleniumFramework.Support;
using ReqOverflow.Specs.Nunit.Web.Pages;

namespace ReqOverflow.Specs.Nunit.Web.Steps;

[Binding]
public static class QuestionSteps
{
  [Given("{string} is on the questions page")]
  public static void GivenIsOnTheQuestionsPage(BrowserUser user) => user.Page<QuestionPage>().Visit();

  [When("{string} submits a question")]
  public static void WhenSubmitsAQuestion(BrowserUser user, DataTable dataTable)
  {
    var questionPage = user.Page<QuestionPage>();
    //todo: move to data entry method in page object taking question data
    questionPage.Title.SendKeys("data");
    questionPage.Body.SendKeys("body");
    questionPage.Tags.SendKeys("tag");
    questionPage.Post.Click();
  }


  [Then("{string} sees an error")]
  public static void ThenSeesAnError(BrowserUser user)
  {
    var questionPage = user.Page<QuestionPage>();
    Assert.That(questionPage.ErrorMessage.Text, Is.EqualTo("Not logged in"));
  }
}