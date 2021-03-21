using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WpacTestProject1
{
    class RegisterPage
    {
    
        private IWebDriver driver;
        private WebDriverWait webDriverWait;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            this.webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }        
        
        /// <summary>
        /// Elements
        /// </summary>
        
        private IWebElement elem_UserName => WaitForFindingElement(By.Id("username"));
        private IWebElement elem_FirstName => WaitForFindingElement(By.Id("firstName"));
        private IWebElement elem_LastName => WaitForFindingElement(By.Id("lastName"));
        private IWebElement elem_Password => WaitForFindingElement(By.Id("password"));
        private IWebElement elem_ConfirmPassword => WaitForFindingElement(By.Id("confirmPassword"));
        public IWebElement elem_RegisterMessage => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-register/div/div/form/div[6]"));
        public IWebElement elem_RegisterWarningMessage => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-register/div/div/form/div[5]/div"));
        private IWebElement elem_RegisterButton => WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-register/div/div/form/button"));



        /// <summary>
        /// Actions
        /// </summary>
        
        public void enter_UserDetails(string username, string firstName,string lastName, string password, string confirmPassword)
        {
            elem_UserName.SendKeys(username);
            elem_FirstName.SendKeys(firstName);
            elem_LastName.SendKeys(lastName);
            elem_Password.SendKeys(password);
            elem_ConfirmPassword.SendKeys(confirmPassword);
        }

        public void click_on_RegisterButton()
        {
            elem_RegisterButton.Click();
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
