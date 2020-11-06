using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LibraryDay3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryDay3.Models.Exceptions;
using System.Linq.Expressions;

/*“BookController” (Controller) class modified:
Modify “ExtendDueDateForBookByID()” to call “BorrowController.ExtendDueDateForBorrowByID()”.
Modify “ReturnBookByID()” to call “BorrowController.ReturnBorrowByID()”.
Modify “DeleteBookByID()” to delete a book.
Add a “GetBooks()” method to get a list of all books.
Add a “GetOverdueBooks()” method to get a list of all books that have a due date prior to the current date.
Modify “GetBookByID()” to get a specific book from the database.
Ensure that the necessary virtual properties are populated on all ‘Get’ methods before returning results.
Ensure that “CreateBook()” is no longer accepting an ID, as it is database generated.
*/
namespace LibraryDay3.Controllers
{
    public class BookController :Controller
    {

        public IActionResult Index()
        {

            return RedirectToAction("List");
        }



        /*Action/View “Create”*/

        public IActionResult Create(string title,string authorID,string publicationDate)
        {

            ViewBag.Authors=GetAuthors();

            if ( Request.Query.Count>0 )
            {
                try
                {
                    Book createBook = CreateBook(title,int.Parse(authorID),publicationDate);
                    ViewBag.Sucess=($"You have successfully checked out {createBook.Title}.");

                }
                // Catch ONLY ValidationException. Any other Exceptions (FormatException, DivideByZeroException, etc) will not get caught, and will break the whole program.
                catch ( ValidationException e )
                {
                    ViewBag.Exception=e;
                    //To have the data stored on the form even if it refreshes / their is an  error
                    ViewBag.BookTitle=title;
                    ViewBag.AuthorID=authorID;
                    ViewBag.PublicationDate=publicationDate;

                }

            }

            return View();

        }


        //Modify the output to account for the new models (show the property values from the related table

        public IActionResult List(string filter)
        {
            /*  When the page loads with the checkbox checked (query string parameter), call the “GetOverdueBooks()” method instead of the “GetBooks()” method
              */
            if ( filter=="on" )
            {
                ViewBag.Books=GetOverdueBooks();
            }
            else
            {
                ViewBag.Books=GetBooks();
            }

            return View();
        }


        public IActionResult Details(string id,string extend,string returned,string delete)
        {
            //  Validation for ID If validation fails

            if ( !int.TryParse(id,out int parsedID)|| id==null )
            {
                ViewBag.Error="No book selected.";
                return View();
            }
            else
                if ( delete!=null )
            {
                DeleteBookByID(int.Parse(id));
                return RedirectToAction("List");
            }
            else
            {

                if ( extend!=null )
                {
                    try
                    {
                        ExtendDueDateForBookByID(int.Parse(id));
                    }
                    catch(ValidationException e)
                    {
                        ViewBag.Exceptions = e;
                    }
                }

                if ( returned!=null )
                {
                    try {
                        ReturnBookByID(int.Parse(id));
                    }
                    catch(ValidationException e)
                    {
                        ViewBag.Exceptions=e;
                    }
                }


                Book getBook = GetBookByID(int.Parse(id));

                if ( getBook==null )
                {
                    ViewBag.Error="No book selected.";
                }
                else
                {
                    ViewBag.Book=getBook;
                }
               

            }
            return View();

        }
        // These methods are for data management.Ensure that “CreateBook()” is no longer accepting an ID, as it is database generated
        public Book CreateBook(string title,int authorID,string publicationDate)
        {
            ValidationException exception = new ValidationException();
            //Trimmed all data prior to processing.,NOT NULL fields must have values that are not whitespace.

            if ( string.IsNullOrWhiteSpace(title) )
            {
                exception.newExceptions.Add(new Exception("The title cannot be empty."));

            }
            else

            //   String data cannot exceed its database size.//All comparison validation must be case insensitive

            //Book titles must be unique for that author.

            {
                if ( title.Trim().Length>30 )
                {
                    exception.newExceptions.Add(new Exception("The Maximum Length of Title is 30 Characters"));
                }
                else
            if ( GetBooks().Any(x => x.Title.ToLower()==title.Trim().ToLower()) )
                {   
                    exception.newExceptions.Add(new Exception("Book Title already Exists,Only New Books allowed "));
                }

            }

            /*“CheckedOutDate” cannot be prior to “PublicationDate”.
           “ReturnedDate” cannot be prior to “CheckedOutDate”.
“           PublishedDate” cannot be in the future
            */
       
            if ( publicationDate==null )
            {
                exception.newExceptions.Add(new Exception("Publication Date is Required to Proceed Further."));
            }
            else
            if ( DateTime.Compare(DateTime.Parse(publicationDate),DateTime.Today)>0 )
            {
                exception.newExceptions.Add(new Exception("Publication date cannot be in the future."));
            }

            if ( exception.newExceptions.Count>0 )
            {
                throw exception;
            }


           //Create a Book
            Book newBook = new Book()
                {

                Title=title.Trim().ToLower(),

                AuthorID=authorID,

                PublicationDate=DateTime.Parse(publicationDate)
                };

            using ( LibraryContext context = new LibraryContext() )
            {
                context.Books.Add(newBook);
                context.SaveChanges();
            }

            return newBook;
        }


        //  Modify “GetBookByID()” to get a specific book from the database.
        public Book GetBookByID(int id)
        {
            Book getBook;
            using ( LibraryContext context = new LibraryContext() )
            {
                getBook=context.Books.Where(x => x.ID==id).Single();
                getBook.Author=context.Authors.Where(x => x.ID==getBook.AuthorID).SingleOrDefault();
            }
            return getBook;
        }

        //Modify “ExtendDueDateForBookByID()” to call “BorrowController.ExtendDueDateForBorrowByID()”.


        public static void ExtendDueDateForBookByID(int id)
        {
            BorrowController.ExtendDueDateForBorrowByID(id);
        }

        //Modify “ReturnBookByID()” to call “BorrowController.ReturnBorrowByID()”.

        public static void ReturnBookByID(int id)
        {
            BorrowController.ReturnBorrowByID(id);
        }

        // Method “DeleteBookByID()” to delete a book.
        public void DeleteBookByID(int id)
        {
            Book deleteBook;
            using ( LibraryContext context = new LibraryContext() )
            {
                deleteBook=context.Books.Where(x => x.ID==id).Single();
                context.Books.Remove(deleteBook);
                context.SaveChanges();
            }
        }

        //Add a “GetBooks()” method to get a list of all books.
        public List<Book> GetBooks()
        {
            List<Book> books;
            using ( LibraryContext context = new LibraryContext() )
            {
                books=context.Books.ToList();
                foreach ( Book book in books )
                {
                    book.Author=context.Authors.Where(x => x.ID==book.AuthorID).Single();
                }
            }
            return books;
        }

        //Add a “GetOverdueBooks()” method to get a list of all books that have a due date prior to the current date.
        public List<Book> GetOverdueBooks()
        {
            List<Book> overdueList = new List<Book>();

            using ( LibraryContext context = new LibraryContext() )

            //@Link: https://www.w3resource.com/csharp-exercises/datetime/csharp-datetime-exercise-17.php

            {
                overdueList=context.Books.Where(x => DateTime.Compare(DateTime.Today,x.DueDate)>0&&x.ReturnedDate==null).ToList();

                foreach ( Book book in overdueList )
                {
                    book.Author=context.Authors.Where(x => x.ID==book.AuthorID).Single();
                }
            }
            return overdueList;
        }



        //Add a “GetAuthors()” method that will return a list of authors (for use in the Create “Book” view).


        public List<Author> GetAuthors()
        {
            List<Author> authors;
            using ( LibraryContext context = new LibraryContext() )
            {
                authors=context.Authors.ToList();
                foreach ( Author author in authors )
                {
                    author.Books=context.Books.Where(x => x.AuthorID==author.ID).ToList();
                }
            }
            return authors;
        }

    }
}