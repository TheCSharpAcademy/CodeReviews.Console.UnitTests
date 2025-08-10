# Unit Testing project by C# Academy
## Project Overview:

Simple Unit Test project for one of previous project in the curriculum - Coding Tracker App

Project link: [Unit Testing](https://www.thecsharpacademy.com/project/21/unit-testing)
## Project Requirements:

- In this project, you'll create unit tests for the Coding Tracker App, the second project in the course
- You'll need to create a CodingTracker.Tests project, parallel to your coding tracker and reference it in your csproj file
- You'll only test the validation methods, making sure the app correctly prevents the user from giving incorrect inputs
- You should use .NETs MSTest Library
- You should test both correct and incorrect inputs

## Lessons Learned:

- Gained a solid foundation in unit testing using .NET's MSTest framework and Test-Driven Development (TDD).
- Initially, all validation logic was embedded directly in Spectre.Console's prompt validation, making unit testing difficult.
- Refactored the logic into a standalone static Validator.cs class to enable straightforward unit testing.
- This refactor clarified the importance of writing testable, modular code.

## Main resources used:
- [MS documentation](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-mstest)
