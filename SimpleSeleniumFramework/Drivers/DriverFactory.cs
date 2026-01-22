using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SimpleSeleniumFramework.Drivers
{
    public class DriverFactory
    {
        private readonly IConfiguration _configuration;

        public DriverFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();

            // Defaults
            bool headless = _configuration.GetValue("Selenium:Headless", true);
            bool maximized = _configuration.GetValue("Selenium:Maximized", true);

            if (headless)
            {
                options.AddArgument("--headless=new");
            }

            if (maximized)
            {
                options.AddArgument("--start-maximized");
            }

            // Common stability options
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--window-size=1920,1080"); // Ensure decent size if headless

            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
