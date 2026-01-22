Feature: anonymous user
In order to attract non members to our site
as an admin
I want to enable anonymous users to view questions but not ask them

  Scenario: anonymous users can't ask questions
    Given "anonymous" is on the questions page
    When "anonymous" submits a question
      | Title                      | Body                                      | Tags |
      | why can't I ask a question | Whenever I post a question I get an error | bug? |
    Then "anonymous" sees an error
