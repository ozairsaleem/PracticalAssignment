using System;
using TechTalk.SpecFlow;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpacTestProject1
{
    [Binding]
    public class UserAuthenticate
    {
        public static IWebDriver driver;
        public string baseURL = "https://buggy.justtestit.org/";
        private HomePage homePage;
        

        [Given(@"user is on the Home")]
        public void GivenUserIsOnTheHome()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            this.homePage = new HomePage(driver);
            this.homePage.GoToUrl(baseURL);
        }

        [When(@"user enter username and password")]
        public void WhenUserEnterUsernameAndPassword()
        {
            this.homePage.enter_LoginAndPassword("Alpha", "Alphabeta@1#");    
        }
        
        [When(@"user click on Login button")]
        public void WhenUserClickOnLoginButton()
        {
            this.homePage.click_on_LoginButton();
        }

        [Then(@"user should be logged into the application")]
        public void ThenUserShouldBeLoggedIntoTheApplication()
        {                       
            Assert.AreEqual(this.homePage.elem_AfterLoginText().Text, "Hi, Alpha");
        }

        [When(@"user enter Invalid username and password")]
        public void WhenUserEnterInvalidUsernameAndPassword()
        {
            this.homePage.enter_LoginAndPassword("Alpha", "InValidPassword@4");
        }

        [Then(@"user should be Not logged into the application")]
        public void ThenUserShouldBeNotLoggedIntoTheApplication()
        {
            Assert.AreEqual(this.homePage.elem_WarningMessage().Text, "Invalid username/password");
        }




        [Given(@"user is already logged in the application")]
        public void GivenUserIsAlreadyLoggedInTheApplication()
        {
            GivenUserIsOnTheHome();
            WhenUserEnterUsernameAndPassword();
            WhenUserClickOnLoginButton();
        }

        [When(@"user clicks on Logout button")]
        public void WhenUserClicksOnLogoutButton()
        {
            this.homePage.click_on_LogoutButton();
        }

        [Then(@"user should be logged out of the application")]
        public void ThenUserShouldBeLoggedOutOfTheApplication()
        {
            Assert.IsTrue(this.homePage.elem_LoginButton().Displayed);
            Assert.IsTrue(this.homePage.elem_RegisterButton().Displayed);
        }

        [Then(@"user close browser")]
        public void ThenUserCloseBrowser()
        {
            driver.Quit();
            driver.Dispose();
        }    

    }
}
