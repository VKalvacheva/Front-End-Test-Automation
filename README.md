# Front-End Test Automation - Movie Catalog

This repository contains **front-end automated tests** for the **Movie Catalog** web application. The tests are implemented in **C#** using **Selenium WebDriver** and **NUnit**.

---

## Repository Contents

- **MovieCatalogTests.sln** – Solution file for Visual Studio.  
- **MovieCatalogTests.csproj** – Project file for the test automation project.  
- **UnitTest1.cs** – Contains the main automated test scripts.  
- **Task conditions.docx** – Describes the testing requirements and tasks.  

---

## Purpose

The purpose of this repository is to:

- Automate front-end testing of the Movie Catalog web application  
- Validate UI functionality, form validation, navigation, and CRUD operations  
- Ensure the application works correctly from the user interface perspective  
- Provide a foundation for integrating automated front-end tests into CI/CD pipelines  

---

## How to Run Tests

1. Clone the repository:

```bash
git clone https://github.com/VKalvacheva/Front-End-Test-Automation.git
```

2. Open the solution in **Visual Studio**.  
3. Restore NuGet packages.  
4. Run the tests using **NUnit Test Explorer** or the command line:

```bash
dotnet test
```

---

## Features

- Automated UI tests for the Movie Catalog web app  
- Validates form inputs, adding, editing, viewing, and deleting movies  
- Handles pagination and navigation testing  
- Ready for integration with CI/CD workflows  

---

## Future Improvements

- Add more negative test cases and edge scenarios  
- Implement cross-browser testing  
- Enhance reporting with screenshots and logs  

