Feature: Login
    As a user
    I want to log in
    So that I can access secure areas

Scenario: Successful Login
    Given "Alice" is on the login page
    When "Alice" logs in with "tomsmith" and "SuperSecretPassword!"
    Then "Alice" should see the secure area
