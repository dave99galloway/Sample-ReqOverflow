using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using SimpleSeleniumFramework.Drivers;

namespace SimpleSeleniumFramework.Support
{
    [Binding]
    public class DependencyRegistrationHooks(IObjectContainer container)
    {
        private readonly IObjectContainer _container = container;

        [BeforeScenario(Order = 0)]
        public void RegisterDependencies()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            var configuration = configBuilder.Build();

            //todo: parse configuration into a type or types so values can be read more safely
            _container.RegisterInstanceAs<IConfiguration>(configuration);
            _container.RegisterTypeAs<DriverFactory, DriverFactory>();
            _container.RegisterTypeAs<UserManager, UserManager>();
        }
    }

    [Binding]
    public class ScenarioLifecycleHooks(UserManager userManager, ScenarioContext scenarioContext)
    {
        private readonly UserManager _userManager = userManager;
        private readonly ScenarioContext _scenarioContext = scenarioContext;

        [AfterScenario]
        public void TearDown()
        {
            if (_scenarioContext.TestError != null)
            {
                TakeScreenshots();
            }

            _userManager.Dispose();
        }

        private void TakeScreenshots()
        {
            foreach (var user in _userManager.ActiveUsers)
            {
                try
                {
                    if (user.Driver is ITakesScreenshot takesScreenshot)
                    {
                        var screenshot = takesScreenshot.GetScreenshot();
                        var title = _scenarioContext.ScenarioInfo.Title;
                        var fileName = $"{title}_{user.Name}_{DateTime.Now:yyyyMMddHHmmss}.png";
                        
                        foreach (char c in Path.GetInvalidFileNameChars())
                        {
                            fileName = fileName.Replace(c, '_');
                        }

                        var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, fileName);
                        screenshot.SaveAsFile(path);
                        TestContext.AddTestAttachment(path);
                        Console.WriteLine($"Screenshot saved: {path}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to take screenshot for user {user.Name}: {ex.Message}");
                }
            }
        }
    }
}
