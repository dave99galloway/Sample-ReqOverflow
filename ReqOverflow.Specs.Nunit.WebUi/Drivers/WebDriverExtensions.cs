using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ReqOverflow.Specs.Nunit.WebUi.Drivers;

public readonly record struct WaitResult<T>(IWebDriver Driver, T Result);

public static class WebDriverExtensions
{
    public static WaitResult<T> UntilAssertionPasses<T>(this IWebDriver driver,Func<IWebDriver, T> condition, TimeSpan? timeout =null )
    {
        
        var wait = new WebDriverWait(driver, timeout ?? TimeSpan.FromSeconds(5));
        
        wait.IgnoreExceptionTypes(typeof(AssertionException));
        var result = wait.Until(condition);
        return new WaitResult<T>(driver, result);
    }

    public static WebDriverWait Wait(this IWebDriver driver, TimeSpan? @for = null)
    {
        //get timeout from config later
        var wait = new WebDriverWait(driver, @for ?? TimeSpan.FromSeconds(5));
        
        return wait;
    }
    
    public static WaitResult<T> UntilAssertionPasses<T>(this WebDriverWait wait,Func<IWebDriver, T> condition )
    {
        wait.IgnoreExceptionTypes(typeof(AssertionException));
        IWebDriver? driver = null;
        var result = wait.Until(  d =>
        {
            driver = d;
            return condition(d);
        });
        return new WaitResult<T>(driver!, result);
    }
    
    public static IWebDriver UntilAssertionsPass(this WebDriverWait wait, params Action <IWebDriver>[] conditions )
    {
        if (conditions.Length == 0) throw new ArgumentNullException(nameof(conditions));
        wait.IgnoreExceptionTypes(typeof(AssertionException));
        IWebDriver? driver = null;
        foreach (var condition in conditions)
        {
            wait.Until(  d =>
            {
                driver = d;
                condition(d);
                return true;
            });
        }
       
        return driver!;
    }
}