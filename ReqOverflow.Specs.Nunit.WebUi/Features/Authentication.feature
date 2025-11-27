Feature: Can Authenticate to site
    
    Scenario: Login as Marvin
        Given "Marvin" is on the Home page
        When "Marvin" hits Login
        Then "Marvin" is on the "Login" page
        
        