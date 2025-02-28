using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

public class BaseTest
{
    protected IWebDriver Driver = null!;

    public void Setup()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        //options.AddArgument("--headless");
        options.AddArgument("--disable-gpu");

        Driver = new ChromeDriver();
        Driver.Navigate().GoToUrl("https://www.matchingengine.com/");
    }

  
    public void TearDown()
    {
        if (Driver != null)
        {
            Driver.Quit();
        }
    }
        
}
