# Project Name: 
ASP.NET Core Practice - Library Due Date Tracker Day3

# Author:
Ayesha Fatima

# Date Last Edited: 
Nov 8th,2020

# Purpose of the Project:
The purpose of the project is to create a tool that will help you keep track of all the books that are checked out of a library.

# Requirements of Project:

## Note:This is a Cumalitive Assignment and Continuation of https://github.com/TECHCareers-by-Manpower/dotnet-core-practice---library-due-date-tracker-day-2-ayeshafatima977.git 
Please visit above link or visit Trelo Board for full requirement of the project.

1. Modify “Borrow” (Model):
2. Add a property “ExtensionCount” - int(10), not nullable.
3. Update your seed data for this table to include values for this field. Add a migration.
4. Update the database.
5.0 Modify “List” (View / Action):
	5.1 Create a form with a checkbox “Filter to Overdue”. When the page loads with the checkbox checked (query string parameter), call the “GetOverdueBooks()” method instead of the “GetBooks()” method.
6.0 Modify “Details” (View / Action):
	6.1 Add a “Number of Extensions” line / output.
7.0 Add the following business logic to the controllers: General Validation:
	7.1 Trimmed all data prior to processing.
	7.2 All comparison validation must be case insensitive.
	7.3 String data cannot exceed its database size.
	7.4 NOT NULL fields must have values that are not whitespace.
	7.5 All numeric/date fields must successfully parse.
	7.6 Primary keys on ‘ByID’ methods must exist.
	7.7 Reference IDs (foreign keys) must exist in their respective tables.
8.0 Library Business Logic:
	8.1 “CheckedOutDate” cannot be prior to “PublicationDate”.
	8.2 “ReturnedDate” cannot be prior to “CheckedOutDate”.
	8.3 “PublishedDate” cannot be in the future.
	8.4 An extension must actually extend the due date in order to be valid.
	8.5 Overdue books cannot be extended.
	8.6 Books cannot be extended more than 3 times.
	8.7 Book titles must be unique for that author.
9.0 Display itemized errors on all appropriate “Book” view pages.


# Challenges
1. Make it look nice with CSS.
2. Modify “List” (View) to show the user how many days a book is overdue, and make the text dark red.
3.0 Add an “Archived” flag to “Book” that will become the new method for “DeleteBookByID”.
	3.1  Set the flag to true when a book is deleted.
	3.2 Don’t allow a book that isn’t returned to be archived.
	3.3 Don’t show the book on “List” unless archived books are being shown.
	3.4 Don’t allow any borrows to take place for the book.
4.0 Modify the checkbox on “List” (View) to be a dropdown with multiple types of filters:
	4.1 All Books
	4.2 In-Stock Books
	4.3 Lent Books
	4.4 Overdue Books (Included in Lent Books)
	4.5 Archived Books
5.0 Create a “Report” Action / View that will provide some summaries about the data.
5.1 Determine which author’s books have the longest total checked-out time.
(This should work with books that haven’t been returned, as well as on books that have been returned.)
6.0 Have an unexpected feature.

# Trello Link:
1. https://trello.com/b/2rbxpdVk/library-due-date-tracker-aspnet

# Citations:
1. MVC Review: https://github.com/TECHCareers-by-Manpower/4.2-MVC
2. Add Data in Home Page :https://www.learnrazorpages.com/razor-pages/tutorial/bakery/working-with-data
3. Layout view :https://docs.microsoft.com/en-us/aspnet/core/mvc/views/layout?view=aspnetcore-3.1
4. Good resource for ASP .NET : https://www.tutorialsteacher.com/mvc/action-method-in-mvc
5. Date Time Today :https://docs.microsoft.com/en-us/dotnet/api/system.datetime.today?view=netcore-3.1
6. DateTime.AddDays :https://docs.microsoft.com/en-us/dotnet/api/system.datetime.adddays?view=netcore-3.1
7. DateTime? :NULL value types :https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types
8. Getter Only Auto properties:https://www.c-sharpcorner.com/UploadFile/a20beb/getter-only-auto-properties-in-C-Sharp-6-0/
9. Table Elemenet :https://developer.mozilla.org/en-US/docs/Web/HTML/Element/table
10 .Bootstrap Button Classes :https://www.w3schools.com/bootstrap/bootstrap_buttons.asp
11. https://www.w3resource.com/csharp-exercises/datetime/csharp-datetime-exercise-17.php
12. Loading Related Data https://docs.microsoft.com/en-us/ef/core/querying/related-data/
13. DateTime Compare : https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
14. Lambda EXpressions: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
15. HomeWork Help Assistance :Aaron.C for Debugging Bugs and Helping Fixing Error for Object Reference on Extend Book and 
16. Peer Support from Birm and Oleg as they had similar earlier issues as I had.And Aaron.B for checking my Extension logic Functioning.
17.  BootStrap: https://getbootstrap.com/docs/4.0/content/typography/#description-list-alignment
18. Bootstrap Form fileds: https://getbootstrap.com/docs/4.0/components/list-group/
19 Form : https://www.codeply.com/go/bp/
20. BootStrap:Tables: https://www.w3schools.com/bootstrap/bootstrap_tables.asp



