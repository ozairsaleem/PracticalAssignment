using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

namespace WpacTestProject1
{
    [Binding]
    public class UserAuthenticate
    {
        private IWebDriver driver;
        private IConfiguration config;
        private HomePage homePage;

        public UserAuthenticate(IWebDriver Driver, IConfiguration Config)
        {
            this.driver = Driver;
            this.config = Config;
        }

        [Given(@"user is on the Home")]
        public void GivenUserIsOnTheHome()
        {
            this.homePage = new HomePage(driver);          
        }

        [When(@"user enter username and password")]
        public void WhenUserEnterUsernameAndPassword()
        {
            this.homePage.enter_LoginAndPassword(config.GetValue<string>("AppSettings:User"), config.GetValue<string>("AppSettings:Password"));    
        }
        
        [When(@"user click on Login button")]
        public void WhenUserClickOnLoginButton()
        {
            this.homePage.click_on_LoginButton();
        }

        [Then(@"user should be logged into the application")]
        public void ThenUserShouldBeLoggedIntoTheApplication()
        {                       
            Assert.AreEqual(this.homePage.elem_AfterLoginText.Text, "Hi, Alpha");
        }

        [When(@"user enter Invalid username and password")]
        public void WhenUserEnterInvalidUsernameAndPassword()
        {
            this.homePage.enter_LoginAndPassword("Alpha", "InValidPassword@4");
        }

        [Then(@"user should be Not logged into the application")]
        public void ThenUserShouldBeNotLoggedIntoTheApplication()
        {
            Assert.AreEqual(this.homePage.elem_WarningMessage.Text, "Invalid username/password");
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
            Assert.IsTrue(this.homePage.elem_LoginButton.Displayed);
            Assert.IsTrue(this.homePage.elem_RegisterButton.Displayed);
        }
 

    }
}
