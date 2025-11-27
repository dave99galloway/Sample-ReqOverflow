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
            Console.WriteLine($"[DriverFactory] Creating new ChromeDriver session for user: {userName}");
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var driver = new ChromeDriver(options);
            _driverUsers[driver] = userName;
            return driver;
        });

    public string GetDriverUser( IWebDriver driver) => _driverUsers[driver];
    
    public void QuitAllDrivers()
    {
        foreach (var user in _userDrivers.Keys)
        {
            try
            {
                var driver = _userDrivers[user];
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