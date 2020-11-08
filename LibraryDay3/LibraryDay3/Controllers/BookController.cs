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

        // When the page loads with the checkbox checked (query string parameter), call the “GetOverdueBooks()” method instead of the “GetBooks()” method

        public IActionResult List(string filter)
        {
            if ( filter=="overdue" )
            {
                ViewBag.Books=GetOverdueBooks();
                ViewBag.Filter=true;
            }
            else
            {
                ViewBag.Books=GetBooks();
                ViewBag.Filter=false;
            }

            return View();
        }


        public IActionResult Details(string id,string extend,string returned,string delete,string borrow)
        {
            int bookId = int.Parse(id);
            Book getBook = null;
            //  Validation for ID If validation fails

            if ( bookId==null )
            {
                ViewBag.Error="No book selected.";
                //return View();
            }
            else
            {
                getBook=GetBookByID(bookId);
                ViewBag.Book=getBook;
            }

            if ( getBook==null )
            {
                ViewBag.Error="No book selected.";
                //return View();
            }

            if ( delete!=null )
            {
                DeleteBookByID(bookId);
                return RedirectToAction("List");
            }

            if ( extend!=null )
            {
                try
                {
                    ExtendDueDateForBookByID(bookId);
                    return RedirectToAction("List");
                }
                catch ( ValidationException e )
                {
                    ViewBag.Exceptions=e;
                    //return View();
                }
            }

            if ( returned!=null )
            {
                try
                {
                    ReturnBookByID(bookId);
                    //BorrowController.CreateBorrow(bookId);
                    //return View();
                }
                catch ( ValidationException e )
                {
                    ViewBag.Exceptions=e;
                    //return View();
                }
            }

            if ( borrow!=null )
            {
                try
                {
                    CreateBorrow(bookId);
                    //return View();
                }
                catch ( ValidationException e )
                {
                    ViewBag.Exceptions=e;
                    //return View();
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

            //   String data cannot exceed its database size.//All comparison validation must be case insensitive.Book titles must be unique for that author.
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
                getBook=context.Books.Where(x => x.ID==(id)).Include(x => x.Author).Include(x => x.Borrows).SingleOrDefault();

            }
            return getBook;
        }

        //Create Borrow
        public void CreateBorrow(int id)
        {
            BorrowController.CreateBorrow(id);
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
            using ( LibraryContext context = new LibraryContext() )
            {
                /*Long Way: Book deleteBook=context.Books.Where(x => x.ID==id).Single();
                  context.Books.Remove(deleteBook);*/
                context.Books.Remove(GetBookByID(id));
                context.SaveChanges();
            }
        }

        //Add a “GetBooks()” method to get a list of all books.
        public List<Book> GetBooks()
        {
            List<Book> books;
            using ( LibraryContext context = new LibraryContext() )
            {
                books=context.Books.Include(x => x.Author).Include(x => x.Borrows).ToList();
            }
            return books;
        }

        //Add a “GetOverdueBooks()” method to get a list of all books that have a due date prior to the current date.
        public List<Book> GetOverdueBooks()
        {
            List<Borrow> overdueList = new List<Borrow>();
            List<Book> result = new List<Book>();

            using ( LibraryContext context = new LibraryContext() )
            {
                overdueList=context.Borrows
                        .Where(x => x.ReturnedDate==null)
                        .Where(x => x.DueDate<DateTime.Today)
                        .ToList();
            }

            foreach ( Borrow borrow in overdueList )
            {
                result.Add(GetBookByID(borrow.BookID));//Borrow obj does not have a valid Book so we must use BookID
            }

            return result;
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