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
  public static void WhenSubmitsAQuestion(BrowserUser user, IEnumerable<QuestionData> data)
  {
    var questionPage = user.Page<QuestionPage>();
    foreach (var question in data)
    {
      questionPage.PostQuestion(question);
    }

  }


  [Then("{string} sees an error")]
  public static void ThenSeesAnError(BrowserUser user)
  {
    var questionPage = user.Page<QuestionPage>();
    //todo: add a transform to create this from a string or table
    ErrorMessage expected = new(Text: "Not logged in");
    Assert.That(questionPage.ErrorMessage, Is.EqualTo(expected));
  }
}
