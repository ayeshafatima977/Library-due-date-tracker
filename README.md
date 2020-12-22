# Project Name:

C# ASP.NET Core Practice - Library Due Date Tracker

## Author:

Ayesha Fatima

# Date Last Edited:

December 21st,2020

## Purpose of the Project:

The purpose of the project is to create a tool that will help to keep track of all the books that are checked out of a library.

## Technical Lessons Learnt related to ASP.NET Web Application MVC:

- The use of scaffold `Author.cs` model with MVC Controller with Views, using Entity Framework(EF) to create `AuthorController.cs` and **Author Views**. The views scaffolded are `Create.cshtml`, `Delete.cshtml`, `Details.cshtml`, `Edit.cshtml`, and `Index.cshtml`.
- The creation of model context, `LibraryContext.cs` from scratch.
- Created relational database between **author** and **book** using EF migrations within NuGet Package Manager Console.
- The creation of `BookController.cs` from scratch using empty MVC controller class.
- The creation of **BookController** and `Views()` from scratch using empty controller files.
- The customization of views within `cshtml` type files.
- The creation of customized **Exceptions** as `ValidationExceptions.cs`to generate custom **Exception** messages.
- The use of LINQ to conduct queries.

## Installation

```bash
$ git clone https://github.com/ayeshafatima977/Library-Day3.git
$ cd Library-Day3
$ cd Library-Day3
$ start devenv Library.sln
```

Use the NuGet Package Manager to install packages:

- Entity Framework [ASP.NET Core Design](https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli).
- Entity Framework [Pomelo Entity Framework Core](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql).
- Entity Framework [ASP.Net Core SqlServer](https://docs.microsoft.com/en-us/ef/core/).

```bash
PM> dotnet add package Microsoft.EntityFrameworkCore.Design
PM> dotnet add package Pomelo.EntityFrameworkCore.MySQL
PM> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Initiate initial migration to create a database with data seeded.

```bash
PM> dotnet ef migrations add InitialCreation
PM> dotnet ef update database
```

## Usage/Approach

- Start the Debugging tool within Visual Studio 2019.
- A browser will automatically open to show a view of the database.

## Requirements of Project:

## Note:This is a Cumalative Assignment and Continuation of [Library Day2] (https://github.com/ayeshafatima977/Library-Day-2.git)

Please visit above link or visit Trelo Board for full requirement of the project.

## Trello Link:

1. https://trello.com/b/2rbxpdVk/library-due-date-tracker-aspnet

# References:

1. [MVC Review](https://github.com/TECHCareers-by-Manpower/4.2-MVC)
2. [Add Data in Home Page](https://www.learnrazorpages.com/razor-pages/tutorial/bakery/working-with-data)
3. [Layout view](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/layout?view=aspnetcore-3.1)
4. [Good resource for ASP .NET](https://www.tutorialsteacher.com/mvc/action-method-in-mvc)
5. [Date Time Today](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.today?view=netcore-3.1)
6. [DateTime.AddDays](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays?view=netcore-3.1)
7. [DateTime? NULL value types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types)
8. [Getter Only Auto properties](https://www.c-sharpcorner.com/UploadFile/a20beb/getter-only-auto-properties-in-C-Sharp-6-0/)
9. [Table Element](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/table)
10. [Bootstrap Button Classes](https://www.w3schools.com/bootstrap/bootstrap_buttons.asp)
11. [DateTime](https://www.w3resource.com/csharp-exercises/datetime/csharp-datetime-exercise-17.php)
12. [Loading Related Data](https://docs.microsoft.com/en-us/ef/core/querying/related-data/)
13. [DateTime Compare](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) 14.[Lambda Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)
14. HomeWork Help Assistance :Aaron.C for Debugging Bugs and Helping Fixing Error for Object Reference on Extend Book and
15. Peer Support from Birm and Oleg as they had similar earlier issues as I had.And Aaron.B for checking my Extension logic Functioning.
16. [BootStrap Typography](https://getbootstrap.com/docs/4.0/content/typography/#description-list-alignment)
17. [Bootstrap Form fields](https://getbootstrap.com/docs/4.0/components/list-group/)
18. [Form](https://www.codeply.com/go/bp/)
19. [BootStrap:Tables](https://www.w3schools.com/bootstrap/bootstrap_tables.asp)
20. [Format Syntax on GitHub](https://docs.github.com/en/free-pro-team@latest/github/writing-on-github/basic-writing-and-formatting-syntax#lists)
