using System;
using System.Linq;
using LibraryDay3.Models;
using LibraryDay3.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDay3.Controllers
{
    public class BorrowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /*Add a “CreateBorrow()” method that will accept a “Book.ID” as a parameter and create a borrow for it. The “CheckOutDate” will be today.The “DueDate” will be 14 days from today. The “ReturnedDate” will be null. */

        //Static is used so that it can be acessed from anywhere else
        public static void CreateBorrow(int id)
        {
            using (var context = new LibraryContext())
            {
                context.Borrows.Add(new Borrow
                {
                    BookID = id,
                    CheckedOutDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(14),
                    ReturnedDate = null,
                    ExtensionCount = 0
                });
                context.SaveChanges();
            }
        }


        //Add a “ExtendDueDateForBorrowByID()” method that will extend the “DueDate” by 7 days from today.
        //An extension must actually extend the due date in order to be valid. Overdue books cannot be extended. Books cannot be extended more than 3 times.

        public static void ExtendDueDateForBorrowByID(int id)
        {
            var exception = new ValidationExceptions();
            using (var context = new LibraryContext())
            {
                var extendBook = context.Borrows.Where(x => x.BookID == id).SingleOrDefault();

                // A book can only be extended a maximum of 3 times.
                //Thanks Aaron.B for helping me correcting my logic.The Book can be extended once in a day and Can be extended maximum 3 times.If user extend the book it will extend it and set due date to be 14 days from Todays date;Note:Extension will be updated Next day becuase of DateTime.Today,So We can test this by manually changing the due date to 18/11/2020 For Example in database we changed Due date to be 18/11 and hit Extend it will increase the Extension Count by 1.Please check the image folder for test result

                if (extendBook.ExtensionCount < 3 && extendBook.DueDate < DateTime.Today.AddDays(14))
                {
                    //Extend the due date 
                    extendBook.DueDate = DateTime.Today.AddDays(14);
                    //Extend by one more time
                    extendBook.ExtensionCount += 1;
                    context.SaveChanges();
                }
                //  if extension is morethan 3  donot extend and throw Exception
                else if (extendBook.ExtensionCount == 3)
                {
                    exception.newExceptions.Add(
                        new Exception("Maximum Limit of Extensions Reached,Please Return the Book"));
                    throw exception;
                }
                else if (DateTime.Compare(DateTime.Today, extendBook.DueDate) > 0)
                {
                    // Overdue books cannot be extended.
                    exception.newExceptions.Add(new Exception(
                        "Please Note: Book is Overdue and cannot be extended, Please return the Book and try again later."));
                    throw exception;
                }
            }
        }

        //Add a “ReturnBorrowByID()” method that will set the “Borrow”s “ReturnedDate” to today.//“ReturnedDate” cannot be prior to “CheckedOutDate”.

        public static void ReturnBorrowByID(int id)
        {
            var exception = new ValidationExceptions();

            using (var context = new LibraryContext())
            {
                {
                    var returnBook = context.Borrows.Where(x => x.BookID == id).SingleOrDefault();
                    if (DateTime.Compare(DateTime.Now, returnBook.DueDate) < 0)
                    {
                        returnBook.ReturnedDate = DateTime.Now;
                        returnBook.DueDate = DateTime.Now;
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
}