using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ReqOverflow.Specs.Nunit.WebUi.Drivers;

public static class WebDriverExtensions
{
    public static WebDriverWait Wait(this IWebDriver driver, TimeSpan? @for = null)
    {
        //get timeout from config later
        var wait = new WebDriverWait(driver, @for ?? TimeSpan.FromSeconds(5));
        return wait;
    }

    #region TestElemet to be clickable

    private static int _counter = 0;

    /// <summary>
    /// DO NOT USE :- this test method shows that when using the ExpectedConditions methods with this signature
    /// <br/><c>Func&lt;IWebDriver, IWebElement> Method(IWebElement element)</c><br/>
    /// e.g. ExpectedConditions.ElementToBeClickable  inside an WebDriverWait.Until block the func needs to be executed to yield the element.
    /// `you cn use .Invoke with the driver, or call the method <see cref="GetElementIf"/> to do it for you fluently
    /// </summary>
    /// <param name="element"></param>
    /// <returns>a func that needs to be executed inside a webDriverWait.Until block to yield an element</returns>
    public static Func<IWebDriver, IWebElement> TestElementToBeClickable(IWebElement element)
    {
        return _ =>
        {
            Console.WriteLine($"Pretending to attempt to find an element. count {_counter}");
            try
            {
                if (_counter > 1)
                {
                    _counter = 0;
                    return element;
                }

                _counter++;
                return null!;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _counter++;
                return null!;
            }
        };
    }

    #endregion


    /// <summary>
    /// use this to call ExpectedConditions methods that return a func yielding an element
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static IWebElement? GetElementIf(this IWebDriver driver, Func<IWebDriver, IWebElement?> condition) =>
        condition(driver) ?? null;
}