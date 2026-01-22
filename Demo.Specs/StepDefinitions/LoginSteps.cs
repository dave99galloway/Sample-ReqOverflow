using NUnit.Framework;
using Reqnroll;
using SimpleSeleniumFramework.Support;
using Demo.Specs.Pages;
using OpenQA.Selenium;

namespace Demo.Specs.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        [Given(@"(.*) is on the login page")]
        public void GivenUserIsOnTheLoginPage(BrowserUser user)
        {
            user.Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        }

        [When(@"(.*) logs in with ""(.*)"" and ""(.*)""")]
        public void WhenUserLogsIn(BrowserUser user, string username, string password)
        {
            // Resolve root: Body
            var loginPage = new LoginPage(() => user.Driver.FindElement(By.TagName("body")));
            
            loginPage.UsernameField.SendKeys(username);
            loginPage.PasswordField.SendKeys(password);
            loginPage.LoginButton.Click();
        }

        [Then(@"(.*) should see the secure area")]
        public void ThenUserShouldSeeTheSecureArea(BrowserUser user)
        {
            var securePage = new SecureAreaPage(() => user.Driver.FindElement(By.TagName("body")));
            
            Assert.That(securePage.Header.Text, Does.Contain("Secure Area"));
            Assert.That(securePage.LogoutButton.Displayed, Is.True);
        }
    }
}
