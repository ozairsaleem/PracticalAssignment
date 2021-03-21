using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WpacTestProject1
{
    class CarModelPage
    {
        private IWebDriver driver;
        private WebDriverWait webDriverWait;

        public CarModelPage(IWebDriver driver)
        {
            this.driver = driver;
            this.webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }


        /// <summary>
        /// Elements
        /// </summary>

        public IWebElement elem_Comment => WaitForFindingElement(By.Id("comment"));
        public IWebElement elem_VoteCount => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-model/div/div[1]/div[3]/div[2]/div[1]/h4/strong"));
        private IWebElement elem_VoteButton => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-model/div/div[1]/div[3]/div[2]/div[2]/div/button"));
        public IWebElement elem_VoteUserLatest => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-model/div/div[3]/table/tbody/tr[1]/td[2]"));
        public IWebElement elem_VoteCommentLatest => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-model/div/div[3]/table/tbody/tr[1]/td[3]"));



        /// <summary>
        /// Actions
        /// </summary>
        

        public void click_on_VoteButton()
        {
            elem_VoteButton.Click();
        }

        public void enter_Comment(string comment)
        {
            elem_Comment.SendKeys(comment);
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
