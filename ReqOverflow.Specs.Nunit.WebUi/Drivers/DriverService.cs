using System.Collections.Concurrent;
using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqOverflow.Specs.Nunit.WebUi.Drivers;

[Binding]
public class WebDriverService
{
    private readonly ConcurrentDictionary<string, IWebDriver> _userDrivers =
        new();
    private readonly ConcurrentDictionary<IWebDriver, string> _driverUsers =
        new();
    public IWebDriver GetDriverForUser(string userName) =>
        _userDrivers.GetOrAdd(userName, _ =>
        {
            // var profileBaseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
            //     "Library", "Application\\ Support", "Google", "Chrome");
            Console.WriteLine($"[DriverFactory] Creating new ChromeDriver session for user: {userName}");
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
       
            // 1. Disable the "Save Password" prompt/feature
            options.AddUserProfilePreference("credentials_enable_service", false);

// 2. Disable the password manager security warnings
            options.AddUserProfilePreference("password_manager_enabled", false);
            options.AddArgument("--disable-web-security");
            options.AddArgument("disable-infobars");
            
            // // 1. Point to the base User Data Directory
            // options.AddArgument($"user-data-dir={profileBaseDir}");

// // 2. Specify the exact profile folder name
//             options.AddArgument("profile-directory=Profile 1");
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            var driver = new ChromeDriver(options);
            _driverUsers[driver] = userName;
            return driver;
        });

    public string GetDriverUser( IWebDriver driver) => _driverUsers[driver];
    
    public void QuitAllDrivers(params Action<IWebDriver>[] actions)
    {
        foreach (var user in _userDrivers.Keys)
        {
            try
            {
                var driver = _userDrivers[user];
                foreach (Action<IWebDriver> action in actions)
                {
                    try
                    {
                        action(driver);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                driver.Quit();
                driver.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error quitting driver for {user}: {ex.Message}");
            }
        }

        _userDrivers.Clear();
    }
}