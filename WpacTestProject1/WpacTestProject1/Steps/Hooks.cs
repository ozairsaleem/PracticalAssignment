using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Edge;
using Microsoft.Edge.SeleniumTools;
using BoDi;
using Microsoft.Extensions.Configuration;

namespace WpacTestProject1.Steps
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks        
        private readonly IObjectContainer objectContainer;
        private IConfiguration config;

        public Hooks( IObjectContainer ObjectContainer)
        {            
            objectContainer = ObjectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (config == null)
                config = new ConfigurationBuilder().AddJsonFile("appconfig.json", optional: false, reloadOnChange: true).Build();
            objectContainer.RegisterInstanceAs<IConfiguration>(config);

            IWebDriver driver;
            string browser = config.GetValue<string>("AppSettings:Browser");

            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(config.GetValue<string>("AppSettings:TestURL"));
                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(config.GetValue<string>("AppSettings:TestURL"));
                    objectContainer.RegisterInstanceAs<IWebDriver>(driver);
                    break;

                case "edge":
                    var options = new EdgeOptions();
                    options.UseChromium = true;
                    driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(options);
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(config.GetValue<string>("AppSettings:TestURL"));
                    objectContainer.RegisterInstanceAs<IWebDriver>(driver);
                    break;

                default:
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(config.GetValue<string>("AppSettings:TestURL"));
                    break;
            }
            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = objectContainer.Resolve<IWebDriver>();
            driver.Quit();
            driver.Dispose();
        }

    }
}
