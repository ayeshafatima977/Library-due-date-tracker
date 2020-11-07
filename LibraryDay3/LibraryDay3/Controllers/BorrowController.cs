
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
    public class BorrowController :Controller
    { 

        public IActionResult Index()
        {
            return View();
        }

        /*Add a “CreateBorrow()” method that will accept a “Book.ID” as a parameter and create a borrow for it. The “CheckOutDate” will be today.The “DueDate” will be 14 days from today. The “ReturnedDate” will be null.     */

        //Static is used so that it can be acessed from anywhere else
        public static void CreateBorrow(int id)
        {

            using ( LibraryContext context = new LibraryContext() )
            {
                context.Borrows.Add(new Borrow()
                {
                    BookID = id,
                    CheckedOutDate=DateTime.Today,
                    DueDate=DateTime.Today.AddDays(14),
                    ReturnedDate=null,
                    ExtensionCount=0

                });
                context.SaveChanges();
            }
            
        }


        //Add a “ExtendDueDateForBorrowByID()” method that will extend the “DueDate” by 7 days from today.
        //An extension must actually extend the due date in order to be valid. Overdue books cannot be extended.  Books cannot be extended more than 3 times.


        public static void ExtendDueDateForBorrowByID(int id)
        {
            ValidationException exception = new ValidationException();
            using ( LibraryContext context = new LibraryContext() )
            {
              Borrow extendBook = context.Borrows.Where(x =>x.ID==id).SingleOrDefault();

                // A book can only be extended a maximum of 3 times.
                if ( extendBook.ExtensionCount < 3 && DateTime.Compare(DateTime.Today, extendBook.DueDate)<0 )
                {
                    //Extend by one more time
                    extendBook.ExtensionCount+=1;
                    extendBook.DueDate=DateTime.Today.AddDays(7);
                    context.SaveChanges();
                }
                //  if extension > 3 donot extend and throw Exception
                else
                if ( extendBook.ExtensionCount >=3 )  
                {  
                    exception.newExceptions.Add(new Exception("Maximum Limit of Extensions Reached,Please Return the Book"));
                    throw exception;
                } 
                else if ( DateTime.Compare(DateTime.Today,extendBook.DueDate)>0 )
                {
                    // Overdue books cannot be extended.
                    exception.newExceptions.Add(new Exception("Please Note: Book is Overdue and cannot be extended, Please return the Book and try again later."));
                    throw exception;
                }

                context.SaveChanges();
            }
        }

        //Add a “ReturnBorrowByID()” method that will set the “Borrow”s “ReturnedDate” to today.//“ReturnedDate” cannot be prior to “CheckedOutDate”.

        public static void ReturnBorrowByID(int id)
        {
            ValidationException exception = new ValidationException();

            using ( LibraryContext context = new LibraryContext() )
            {
              Borrow returnBook=context.Borrows.Where(x => x.ID==id).SingleOrDefault();
                if ( DateTime.Compare(DateTime.Today,returnBook.DueDate)<0 )
                {
                    returnBook.ReturnedDate=DateTime.Today;
                    context.SaveChanges();
                }
                else
                {
                    exception.newExceptions.Add(new Exception("Cannot return book,Please visit Reception Area."));
                    throw exception;
                }


            }
        }

     

    }
}