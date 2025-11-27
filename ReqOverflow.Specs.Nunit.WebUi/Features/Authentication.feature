Feature: Can Authenticate to site

    Scenario: Marvin can open the login page
        Given "Marvin" has opened the Home page
        When "Marvin" hits Login
        Then "Marvin" can see the Login page has opened

    Scenario: Marvin can login
        Given "Marvin" has opened the Login page
        #todo centralise passwords so they don't need to be repeated and publicly visible...
        When "Marvin" logs in with password "1234"
        Then "Marvin" is on the Home page