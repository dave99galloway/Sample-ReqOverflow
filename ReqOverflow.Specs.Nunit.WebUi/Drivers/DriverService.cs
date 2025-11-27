namespace ReqOverflow.Specs.Nunit.WebUi.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class WebDriverService
{
    private readonly Dictionary<string, IWebDriver> _userDrivers = 
        new ();

    public IWebDriver GetDriverForUser(string userName)
    {
        if (_userDrivers.TryGetValue(userName, out IWebDriver driver))
        {
            Console.WriteLine($"[DriverFactory] Returning cached driver for user: {userName}");
            return driver;
        }

        Console.WriteLine($"[DriverFactory] Creating new ChromeDriver session for user: {userName}");
        
        var options = new ChromeOptions();
        
        options.AddArgument("--start-maximized"); 
        
        driver = new ChromeDriver(options); 
        
        _userDrivers.Add(userName, driver);
        return driver;
    }

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