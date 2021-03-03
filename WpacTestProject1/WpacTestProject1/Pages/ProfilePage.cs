using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WpacTestProject1
{
    class ProfilePage
    {
        private IWebDriver driver;
        private WebDriverWait webDriverWait;

        public ProfilePage(IWebDriver driver)
        {
            this.driver = driver;
            this.webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Elements
        /// </summary>

        private IWebElement elem_Gender()
        { return WaitForFindingElement(By.Id("gender")); }

        private IWebElement elem_Age()
        { return WaitForFindingElement(By.Id("age")); }

        private IWebElement elem_Address()
        { return WaitForFindingElement(By.Id("address")); }

        private IWebElement elem_Phone()
        { return WaitForFindingElement(By.Id("phone")); }

        private IWebElement elem_Hobby()
        { return WaitForFindingElement(By.Id("hobby")); }

        public IWebElement elem_SaveProfileButton()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-profile/div/form/div[2]/div/button")); }

        public IWebElement elem_ProfileMessage()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-profile/div/form/div[1]/div[1]/div[2]")); }


        /// <summary>
        /// Actions
        /// </summary>

        public void enter_ProfileAddionalDetails(string gender, string age, string address, string phone, string hobby)
        {
            elem_Gender().SendKeys(gender);
            elem_Age().SendKeys(age);
            elem_Address().SendKeys(address);
            elem_Phone().SendKeys(phone);
            elem_Hobby().SendKeys(hobby);
        }

        public void click_on_SaveProfileButton()
        {
            elem_SaveProfileButton().Click();
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
