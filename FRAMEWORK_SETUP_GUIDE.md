# Simple Selenium Framework Setup Guide

This guide describes how to build a simple, scalable, multi-user Selenium framework using Reqnroll and NUnit.

## 1. Solution & Project Setup

Execute the following commands in your terminal to set up the solution structure from scratch.

### Create Solution and Projects
```bash
# Create Solution
dotnet new sln -n ReqOverflow

# Create Framework Library
dotnet new classlib -n SimpleSeleniumFramework -f net8.0

# Create Test Projects
dotnet new nunit -n Demo.Specs -f net8.0
dotnet new nunit -n ReqOverflow.Specs.Nunit.Web -f net8.0

# Add Projects to Solution
dotnet sln add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj
dotnet sln add Demo.Specs/Demo.Specs.csproj
dotnet sln add ReqOverflow.Specs.Nunit.Web/ReqOverflow.Specs.Nunit.Web.csproj
```

### Add References
```bash
# Test projects must reference the framework
dotnet add Demo.Specs/Demo.Specs.csproj reference SimpleSeleniumFramework/SimpleSeleniumFramework.csproj
dotnet add ReqOverflow.Specs.Nunit.Web/ReqOverflow.Specs.Nunit.Web.csproj reference SimpleSeleniumFramework/SimpleSeleniumFramework.csproj

# (Optional) Add reference to your web app if needed
# dotnet add ReqOverflow.Specs.Nunit.Web/ReqOverflow.Specs.Nunit.Web.csproj reference ReqOverflow.Web/ReqOverflow.Web.csproj
```

## 2. Dependencies (NuGet Packages)

Install the necessary packages for the framework and test projects.

### Framework Library (`SimpleSeleniumFramework`)
```bash
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Selenium.WebDriver
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Selenium.WebDriver.ChromeDriver
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Reqnroll
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Reqnroll.NUnit
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Microsoft.Extensions.Configuration
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Microsoft.Extensions.Configuration.Binder
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Microsoft.Extensions.Configuration.Json
dotnet add SimpleSeleniumFramework/SimpleSeleniumFramework.csproj package Microsoft.Extensions.Configuration.EnvironmentVariables
```

### Test Projects (`Demo.Specs` & `ReqOverflow.Specs.Nunit.Web`)
```bash
# Repeat for both test projects
dotnet add Demo.Specs/Demo.Specs.csproj package Reqnroll
dotnet add Demo.Specs/Demo.Specs.csproj package Reqnroll.NUnit

dotnet add ReqOverflow.Specs.Nunit.Web/ReqOverflow.Specs.Nunit.Web.csproj package Reqnroll
dotnet add ReqOverflow.Specs.Nunit.Web/ReqOverflow.Specs.Nunit.Web.csproj package Reqnroll.NUnit
```
*Note: `Microsoft.NET.Test.Sdk`, `NUnit`, and `NUnit3TestAdapter` are usually included by default in the `nunit` template.*

## 3. Configuration

### Reqnroll Configuration (`reqnroll.json`)
**CRITICAL:** You must create a `reqnroll.json` file in the root of each **Test Project** to register the framework assembly. Without this, Reqnroll will not find the Step Argument Transformations or Hooks defined in the library.

**File:** `Demo.Specs/reqnroll.json`
```json
{
  "stepAssemblies": [
    { "assembly": "SimpleSeleniumFramework" }
  ]
}
```

**Update .csproj:**
Ensure this file is copied to the output directory. Add this to your `.csproj` file if not using Visual Studio properties:
```xml
<ItemGroup>
  <None Update="reqnroll.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### App Settings (`appsettings.json`)
Create an `appsettings.json` in your test projects for configuration.

```json
{
  "Selenium": {
    "Headless": true,
    "Maximized": true
  }
}
```
*Remember to set `CopyToOutputDirectory` to `Always` for this file as well.*

## 4. Core Framework Implementation

The following files constitute the "engine" of the framework.

*   **`Drivers/DriverFactory.cs`**: Instantiates `ChromeDriver` with options.
*   **`Support/BrowserUser.cs`**: Represents a user, holding their unique `IWebDriver` instance.
*   **`Support/UserManager.cs`**: Manages the lifecycle of `BrowserUser` instances (creation and disposal).
*   **`Support/Hooks.cs`**:
    *   `[BeforeScenario]`: Registers `DriverFactory` and `UserManager` in the DI container.
    *   `[AfterScenario]`: Takes screenshots on failure and disposes `UserManager` (quitting browsers).
*   **`Support/UserTransformations.cs`**:
    *   **IMPORTANT:** This class must be `public` (not abstract) and have a `public` constructor.
    *   Uses `[StepArgumentTransformation]` to turn `Given "Alice" ...` into a `BrowserUser` object.
*   **`Pages/PageObject.cs`**: Base class using `Func<IWebElement>` for lazy root resolution.

**References / Inspiration:**
*   **Reqnroll/SpecFlow Context Injection:** [Reqnroll Documentation](https://docs.reqnroll.net/latest/automation/context-injection.html)
*   **Selenium Page Object Model:** [Selenium Documentation](https://www.selenium.dev/documentation/test_practices/encouraged/page_object_models/)
*   **Lazy Element Resolution:** Inspired by standard robust Selenium practices to avoid `StaleElementReferenceException`.

## 5. Execution & Reading Results

### Option A: Command Line Interface (CLI)

Run the tests from the solution root:
```bash
dotnet test Demo.Specs
```

#### Reading Results
1.  **Console Output:** The primary feedback is in the terminal. You will see `Passed!`, `Failed!`, and stack traces for failures.
2.  **Screenshots:** If configured (as in this framework), screenshots for failed tests are saved to the `bin/Debug/net8.0/` folder of the test project.
3.  **TRX Report (Structured File):**
    To generate a machine-readable report (XML), use:
    ```bash
    dotnet test Demo.Specs --logger "trx;LogFileName=TestResults.trx"
    ```
    *   **Location:** The file will be generated in a `TestResults` folder in your project or solution root (or wherever specified).
    *   **Viewing:**
        *   **Visual Studio:** Open the `.trx` file directly.
        *   **VS Code:** Install an extension like **"Test Results"** or rely on the console output.
        *   **CI/CD:** Pipelines (Azure DevOps, GitHub Actions) automatically parse `.trx` files to display results in the UI.

### Option B: Visual Studio Code

1.  **Extensions:** Ensure you have the **C# Dev Kit** extension installed.
2.  **Test Explorer:**
    *   Click the **Testing** icon (beaker) in the left sidebar.
    *   You should see your projects (`Demo.Specs`, etc.) listed.
    *   *If empty:* Build the solution (`dotnet build`) to discover tests.
3.  **Running:**
    *   Click the "Play" button next to a test or a project.
4.  **Reading Results:**
    *   **Pass/Fail:** Indicated by green checks or red crosses in the Test Explorer.
    *   **Details:** Click on a failed test in the Test Explorer. The **Test Results** panel will open, showing the error message, stack trace, and standard output (logs).
    *   **Screenshots:** Paths to screenshots (printed to Console.WriteLine in our Hooks) will appear in the **Test Output** section of the results panel.