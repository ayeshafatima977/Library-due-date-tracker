# Project Name: 
ASP.NET Core Practice - Library Due Date Tracker Day3

# Author:
Ayesha Fatima

# Date Last Edited: 
Nov 8th,2020

# Purpose of the Project:
Thhe purpose of the project is to create a tool that will help you keep track of all the books that are checked out of a library.

# Requirements of Project:
Note:This is a Cumalitiva Assignment and Continuation of https://github.com/TECHCareers-by-Manpower/dotnet-core-practice---library-due-date-tracker-day-2-ayeshafatima977.git 

1. Generate ERD using code-first database generation techniques
2. Ensure all controller methods interface with Entity Framework and the database, and not the list from ASP.NET Core Practice - Library Due Date Tracker Day 1.
3. Add a “LibraryContext” class (Context):
	All requisite methods and properties to function as a context.
	Database connection string to a database called “mvc_library”.
	Ensure the delete behaviour for “Author” is “Restrict”.
	Ensure the delete behaviour for “Book” is “Cascade”.
	Seed the database with:
	At least 5 “Authors” of your choice.
	At least 3 “Books” by the same Author.
		These three books must have a “CheckoutDate” equal to December 25, 2019.
		One book must be returned on-time with no extension.
		One book must be returned on-time with one extension.
		One book must not have been returned at all!
	Add migrations and update the database once this and the models are completed.
4.	Add a scaffolded controller and views for the “Author” model (using “LibraryContext”).
	Add a “GetAuthors()” method that will return a list of authors (for use in the Create “Book” view).
	Do NOT use scaffolding for the “Book” or “Borrow” models, continue to use the manually generated Controller and Views from ASP.NET Core Practice - Library Due Date Tracker Day 1.
5. “BorrowController” (Controller) class created:
	Add a “ExtendDueDateForBorrowByID()” method that will extend the “DueDate” by 7 days from today.
	Add a “ReturnBorrowByID()” method that will set the “Borrow”s “ReturnedDate” to today.
	Add a “CreateBorrow()” method that will accept a “Book.ID” as a parameter and create a borrow for it.
		The “CheckOutDate” will be today.
		The “DueDate” will be 14 days from today.
		The “ReturnedDate” will be null.
6.“BookController” (Controller) class modified:
	Remove the “Books” property (static list of Books).
	Modify “ExtendDueDateForBookByID()” to call “BorrowController.ExtendDueDateForBorrowByID()”.
	Modify “ReturnBookByID()” to call “BorrowController.ReturnBorrowByID()”.
	Modify “DeleteBookByID()” to delete a book.
	Add a “GetBooks()” method to get a list of all books.
	Add a “GetOverdueBooks()” method to get a list of all books that have a due date prior to the current date.
	Modify “GetBookByID()” to get a specific book from the database.
	Ensure that the necessary virtual properties are populated on all ‘Get’ methods before returning results.
	Ensure that “CreateBook()” is no longer accepting an ID, as it is database generated.
7. “Book” “Create” (View) modified:
	Have a dropdown (select) to select the “Author

		Populate the dropdown (select) based on the “Author” table (Call “AuthorController.GetAuthors()”).
	Remove the field for “ID”.
8. “Book” “List” (View) modified:
	Modify the output to account for the new models (show the property values from the related tables).
9. “Book” “Details” (View) modified:
	Modify the output to account for the new models (show the property values from the related tables).
	Add a Borrow Book button that will create a Borrow for the book.

# Challenges
1. Make it look nice with CSS.
2.Modify “List” (View) to show the user how many days a book is overdue, and make the text dark red.
3.Add an “Archived” flag to “Book” that will become the new method for “DeleteBookByID”.
		Set the flag to true when a book is deleted.
		Don’t allow a book that isn’t returned to be archived.
		Don’t show the book on “List” unless archived books are being shown.
		Don’t allow any borrows to take place for the book.
4.Modify the checkbox on “List” (View) to be a dropdown with multiple types of filters:
		All Books
		In-Stock Books
		Lent Books
		Overdue Books (Included in Lent Books)
		Archived Books
5. Create a “Report” Action / View that will provide some summaries about the data.
6. Determine which author’s books have the longest total checked-out time.
		This should work with books that haven’t been returned, as well as on books that have been returned.
7. Have an unexpected feature.
# Trello Link:
1. https://trello.com/b/2rbxpdVk/library-due-date-tracker-aspnet

# Citations:
1. https://github.com/TECHCareers-by-Manpower/4.2-MVC
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
15. HomeWork Help Assistance :Aaron.C for Debugging Bugs and Helping Fixing Error for Object Reference on Extend Book and Peer Support from Birm and Oleg as they had similar earlier issues as I had.

