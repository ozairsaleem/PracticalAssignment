using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Threading;


namespace WpacTestProject1
{
    [Binding]
    public class UserFeatues
    {
        public IWebDriver driver;
        private IConfiguration config;
        
        private HomePage homePage;
        private RegisterPage registerPage;
        private ProfilePage profilePage;
        private OverallRatingPage overAllRatingPage;
        private CarModelPage carModelPage;

        
        public static string username= "Alpha";
        public static string firstName = "Alpha";
        public static string lastName = "beta";
        public static string password= "Alphabeta@1#";
        public static int currentVoteCount = 0;
        public static string voteComment = "";

        public UserFeatues(IWebDriver Driver, IConfiguration Config)
        {
            this.driver = Driver;
            this.config = Config;
        }

        
        [Given(@"user is on the Home page")]
        public void GivenUserIsOnTheHomePage()
        {
            this.homePage = new HomePage(driver);            
        }
        
        [When(@"user click on register button")]
        public void WhenUserClickOnRegisterButton()
        {            
            this.registerPage = this.homePage.click_on_RegisterButton();
        }
        
        [When(@"user is navigated to Registration page")]
        public void WhenUserIsNavigatedToRegistrationPage()
        {            
            Assert.IsTrue(driver.Url.Contains("buggy.justtestit.org/register"));
        }
        
        [When(@"user enters the requied fields")]
        public void WhenUserEntersTheRequiedFields()
        {
            username = Faker.User.Email();
            firstName = Faker.Name.FirstName();
            lastName = Faker.Name.LastName();

            this.registerPage.enter_UserDetails(username, firstName, lastName, password, password);
        }

        [When(@"click on the Register button")]
        public void WhenClickOnTheRegisterButton()
        {
            this.registerPage.click_on_RegisterButton();
        }



        [Then(@"message is shown ""(.*)""")]
        public void ThenMessageIsShown(string p0)
        {
            Assert.AreEqual(p0, this.registerPage.elem_RegisterMessage.Text);
        }



        [When(@"user enters the requied fields with not matching password")]
        public void WhenUserEntersTheRequiedFieldsWithNotMatchingPassword()
        {
            this.registerPage.enter_UserDetails(username, firstName, lastName, "ThisPassword", "IsNotMatching");
        }

        [Then(@"user gets message ""(.*)""")]
        public void ThenUserGetsMessage(string p0)
        {
            Assert.AreEqual(this.registerPage.elem_RegisterWarningMessage.Text, p0);
        }

        [When(@"user enters the requied fields with not secure password")]
        public void WhenUserEntersTheRequiedFieldsWithNotSecurePassword()
        {
            this.registerPage.enter_UserDetails(username, firstName, lastName, "NotSecurePwd", "NotSecurePwd");
        }

        [When(@"user enters the requied fields with not secure password without symbol characters")]
        public void WhenUserEntersTheRequiedFieldsWithNotSecurePasswordWithoutSymbolCharacters()
        {
            this.registerPage.enter_UserDetails(username, firstName, lastName, "NotSecurePwd1", "NotSecurePwd1");
        }

        [When(@"user enters the requied fields of already existing user")]
        public void WhenUserEntersTheRequiedFieldsOfAlreadyExistingUser()
        {
            this.registerPage.enter_UserDetails(username, firstName, lastName, password, password);
        }








        [Given(@"new user is already registered")]
        public void GivenNewUserIsAlreadyRegistered()
        {           
            GivenUserIsOnTheHomePage();
            WhenUserClickOnRegisterButton();
            WhenUserIsNavigatedToRegistrationPage();
            WhenUserEntersTheRequiedFields();
            WhenClickOnTheRegisterButton();
        }

        [Given(@"user login to application")]
        public void GivenUserLoginToApplication()
        {
            this.homePage = new HomePage(driver);
            this.homePage.GoToUrl(config.GetValue<string>("AppSettings:TestURL"));

            this.homePage.enter_LoginAndPassword(username, password);
            this.homePage.click_on_LoginButton();

            Assert.AreEqual(this.homePage.elem_AfterLoginText.Text, "Hi, "+firstName);
        }

        [When(@"user click profile page")]
        public void WhenUserClickProfilePage()
        {
            this.profilePage = this.homePage.click_on_ProfileButton();            
        }

        [When(@"user enters Addional information fields")]
        public void WhenUserEntersAddionalInformationFields()
        {
            this.profilePage.enter_ProfileAddionalDetails(Faker.Name.Gender(), Faker.Number.RandomNumber(18, 60).ToString(), Faker.Address.StreetName() + " " + Faker.Address.SecondaryAddress() + " " + Faker.Address.USCity(), Faker.Phone.GetPhoneNumber(), "Working");
        }

        [When(@"user clicks on save profile button")]
        public void WhenUserClicksOnSaveProfileButton()
        {
            this.profilePage.click_on_SaveProfileButton();
        }

        [Then(@"user profile information is saved and message is shown ""(.*)""")]
        public void ThenUserProfileInformationIsSavedAndMessageIsShown(string p0)
        {            
            Assert.AreEqual(p0, this.profilePage.elem_ProfileMessage.Text);
        }


        [When(@"user click on Overall car rating")]
        public void WhenUserClickOnOverallCarRating()
       {
            this.overAllRatingPage = this.homePage.click_on_OverallRatingButton();
        }

        [When(@"click on one of models on the page")]
        public void WhenClickOnOneOfModelsOnThePage()
        {
           this.carModelPage = this.overAllRatingPage.click_on_RandomModelFromFirstPage();
        }

        [When(@"User add a comment and press Vote button")]
        public void WhenUserAddACommentAndPressVoteButton()
        {         
            string currentVoterName = this.carModelPage.elem_VoteUserLatest.Text;
            currentVoteCount = Convert.ToInt32(this.carModelPage.elem_VoteCount.Text);
            voteComment = Faker.Lorem.Sentence();

            this.carModelPage.enter_Comment(voteComment);
            this.carModelPage.click_on_VoteButton();




            // possible it can be infinite loop
            //Wait for new comment to be added. 
            //while (true)
            //{
            //    if (currentVoterName != this.carModelPage.elem_VoteUserLatest().Text)
            //        break;
            //    else
            //        Thread.Sleep(500);
            //}


            for (int i = 0; i < 1000; i++)
            {
                if (currentVoteCount != Convert.ToInt32(this.carModelPage.elem_VoteCount.Text))
                    break;
                else
                    Thread.Sleep(500);
            }

            //for (int i = 0; i < 1000; i++)
            //{
            //    if (currentVoterName != this.carModelPage.elem_VoteUserLatest().Text)
            //        break;
            //    else
            //        Thread.Sleep(500);
            //}

        }

        [When(@"User press Vote button without comment")]
        public void WhenUserPressVoteButtonWithoutComment()
        {
            currentVoteCount = Convert.ToInt32(this.carModelPage.elem_VoteCount.Text);
            this.carModelPage.click_on_VoteButton();


            // possible it can be infinite loop
            //Wait for new vote to be changed.
            //while (true)
            //{
            //    if (currentVoteCount != Convert.ToInt32(this.carModelPage.elem_VoteCount().Text))
            //        break;
            //    else
            //        Thread.Sleep(500);
            //}

            for (int i = 0; i < 1000; i++)
            {
                if (currentVoteCount != Convert.ToInt32(this.carModelPage.elem_VoteCount.Text))
                    break;
                else
                    Thread.Sleep(500);
            }
        }


        [Then(@"votes should be incremented")]
        public void ThenVotesShouldBeIncremented()
        {
            Assert.AreEqual(currentVoteCount + 1, Convert.ToInt32(this.carModelPage.elem_VoteCount.Text));
        }

        [Then(@"comment should be listed in the comment secion")]
        public void ThenCommentShouldBeListedInTheCommentSecion()
        {            
            Assert.AreEqual(firstName+" "+lastName, this.carModelPage.elem_VoteUserLatest.Text);            
            Assert.AreEqual(voteComment, this.carModelPage.elem_VoteCommentLatest.Text);                                                                       
        }



    }
}
