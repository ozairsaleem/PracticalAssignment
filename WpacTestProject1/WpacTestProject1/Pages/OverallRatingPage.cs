using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WpacTestProject1
{
    class OverallRatingPage
    {

        private IWebDriver driver;
        private WebDriverWait webDriverWait;

        public OverallRatingPage(IWebDriver driver)
        {
            this.driver = driver;
            this.webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Elements
        /// </summary>

        private IWebElement elem_RandomModelFromFirstPage()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-overall/div/div/table/tbody/tr[" + Faker.Number.RandomNumber(2, 5).ToString() + "]/td[3]/a")); }
        
       
        
        /// <summary>
        /// Actions
        /// </summary>
        public CarModelPage click_on_RandomModelFromFirstPage()
        {
            elem_RandomModelFromFirstPage().Click();
            return new CarModelPage(driver);
        }    

        private void WaitForClickableElement(By by)
        {
            webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        private IWebElement WaitForFindingElement(By by)
        {
            return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
