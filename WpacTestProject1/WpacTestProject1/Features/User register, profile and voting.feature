Feature: User register, profile and voting	
	As a user of 'Buggy Car Rating' web application
	I should to be able to register a new user
	I should be able to edit user profile
	I should be able to vote in car ratings

@PositiveUseCase
Scenario: Register new user 
	Given the browser is open
	And user is on the Home page
	When user click on register button
	And user is navigated to Registration page 
	And user enters the requied fields
	And click on the Register button
	Then message is shown "Registration is successful"
	And user close the browser

@PositiveUseCase
Scenario: For a new user add addional profile information
	Given new user is already registered
	And user login to application
	When user click profile page
	And user enters Addional information fields
	And user clicks on save profile button
	Then user profile information is saved and message is shown "The profile has been saved successful"
	And user close the browser

@PositiveUseCase
Scenario: For a new user vote and comment for registered models
	Given new user is already registered
	And user login to application
	When user click on Overall car rating
	And click on one of models on the page
	And User add a comment and press Vote button
	Then votes should be incremented 
	And comment should be listed in the comment secion
	And user close the browser

@PositiveUseCase
Scenario: For a new user only vote without comment for registered models
	Given new user is already registered
	And user login to application
	When user click on Overall car rating
	And click on one of models on the page
	And User press Vote button without comment
	Then votes should be incremented 	
	And user close the browser


@NegativeUseCase
Scenario: Register same user twice 
	Given the browser is open
	And user is on the Home page
	When user click on register button
	And user is navigated to Registration page 
	And user enters the requied fields of already existing user
	And click on the Register button	
	Then message is shown "UsernameExistsException: User already exists"
	And user close the browser


@NegativeUseCase
Scenario: Register new user with not matching password
	Given the browser is open
	And user is on the Home page
	When user click on register button
	And user is navigated to Registration page 
	And user enters the requied fields with not matching password
	Then user gets message "Passwords do not match"	
	And user close the browser


@NegativeUseCase
Scenario: Register new user with password not having numeric characters
	Given the browser is open
	And user is on the Home page
	When user click on register button
	And user is navigated to Registration page 
	And user enters the requied fields with not secure password
	And click on the Register button	
	Then message is shown "InvalidPasswordException: Password did not conform with policy: Password must have numeric characters"
	And user close the browser


@NegativeUseCase
Scenario: Register new user with password not having symbol characters
	Given the browser is open
	And user is on the Home page
	When user click on register button
	And user is navigated to Registration page 
	And user enters the requied fields with not secure password without symbol characters
	And click on the Register button	
	Then message is shown "InvalidPasswordException: Password did not conform with policy: Password must have symbol characters"
	And user close the browser
