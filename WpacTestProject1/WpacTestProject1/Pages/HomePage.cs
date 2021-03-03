using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace WpacTestProject1
{
    class HomePage
    {    
        private IWebDriver driver;
        private WebDriverWait webDriverWait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }        
        
        /// <summary>
        /// Elements
        /// </summary>
        
        private IWebElement elem_Login()
        {return WaitForFindingElement(By.Name("login"));}

        private IWebElement elem_Password()
        {return WaitForFindingElement(By.Name("password"));}

        public IWebElement elem_LoginButton()
        {return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/form/button"));}

        private IWebElement elem_LogOutButton()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/ul/li[3]/a")); }

        public IWebElement elem_RegisterButton()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/form/a")); }

        public IWebElement elem_ProfileButton()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/ul/li[2]/a")); }

        public IWebElement elem_AfterLoginText()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/ul/li[1]/span")); }

        public IWebElement elem_OverallRating()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/div/main/my-home/div/div[3]/div/a/img")); }

        public IWebElement elem_HomeButton()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/a")); }

        public IWebElement elem_WarningMessage()
        { return WaitForFindingElement(By.XPath("/html/body/my-app/header/nav/div/my-login/div/form/div/span")); }


        /// <summary>
        /// Actions
        /// </summary>

        public void enter_LoginAndPassword(string login, string password)
        {
            elem_Login().SendKeys(login);
            elem_Password().SendKeys(password);           
        }

        public void click_on_LoginButton()
        {
            elem_LoginButton().Click();            
        }

        public void click_on_LogoutButton()
        {
            elem_LogOutButton().Click();
        }

        public RegisterPage click_on_RegisterButton()
        {
            elem_RegisterButton().Click();
            return new RegisterPage(driver);
        }

        public ProfilePage click_on_ProfileButton()
        {
            elem_ProfileButton().Click();
            return new ProfilePage(driver);
        }

        public void click_on_HomeButton()
        {
            elem_HomeButton().Click();            
        }

        public OverallRatingPage click_on_OverallRatingButton()
        {
            elem_OverallRating().Click();
            return new OverallRatingPage(driver);
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
