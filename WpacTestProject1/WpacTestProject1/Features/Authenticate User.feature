Feature: AuthenticateUser
	As a user of 'Buggy Car Rating' web application
	I should to be able to login and logout of application

@PositiveUseCase
Scenario: Login to application	
	Given user is on the Home
	When user enter username and password
	And user click on Login button
	Then user should be logged into the application	

@PositiveUseCase
Scenario: Logout of application
	Given user is already logged in the application
	When user clicks on Logout button
	Then user should be logged out of the application
	

@NegtiveUseCase
Scenario: Login to application with Invalid username or password
	Given user is on the Home
	When user enter Invalid username and password
	And user click on Login button
	Then user should be Not logged into the application
	