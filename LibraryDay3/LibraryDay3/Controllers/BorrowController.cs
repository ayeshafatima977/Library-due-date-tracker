
/*“BorrowController” (Controller) class created:
Add a “ExtendDueDateForBorrowByID()” method that will extend the “DueDate” by 7 days from today.
Add a “ReturnBorrowByID()” method that will set the “Borrow”s “ReturnedDate” to today.
Add a “CreateBorrow()” method that will accept a “Book.ID” as a parameter and create a borrow for it.
The “CheckOutDate” will be today.
The “DueDate” will be 14 days from today.
The “ReturnedDate” will be null.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryDay3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LibraryDay3.Controllers
{
    public class BorrowController :Controller
    {
        private readonly LibraryContext _context;

        public static Borrow requiredBorrow { get; private set; }
     
        public DateTime DueDate { get; set; }
        public DateTime CheckedOutDate { get; set; }

        public DateTime? ReturnedDate { get; set; }


        public BorrowController(LibraryContext context)
        {
            _context=context;
        }


        public IActionResult Index()
        {
            return View();
        }


        //Add a “ExtendDueDateForBorrowByID()” method that will extend the “DueDate” by 7 days from today.

        public static void ExtendDueDateForBorrowByID(int id)
        {
            Book extendBook;
            using ( LibraryContext context = new LibraryContext() )
            {
                extendBook=context.Books.Where(x => x.ID==id).Single();

                extendBook.DueDate=DateTime.Now.AddDays(7);

                context.SaveChanges();
            }
        }



        //Add a “ReturnBorrowByID()” method that will set the “Borrow”s “ReturnedDate” to today.

        public static void ReturnBorrowByID(int id)
        {
            Book returnBook;
            using ( LibraryContext context = new LibraryContext() )
            {
                returnBook=context.Books.Where(x => x.ID==id).Single();

                returnBook.ReturnedDate=DateTime.Today;
                context.SaveChanges();
            }
        }

        /*Add a “CreateBorrow()” method that will accept a “Book.ID” as a parameter and create a borrow for it. The “CheckOutDate” will be today.The “DueDate” will be 14 days from today. The “ReturnedDate” will be null.     */

        public static void CreateBorrow(int id)
        {

            Borrow newBorrow = new Borrow()
            {
                CheckedOutDate=DateTime.Today,
                DueDate=DateTime.Today.AddDays(14),
                ReturnedDate=null

            };
            using ( LibraryContext context = new LibraryContext() )
            {

                context.Borrows.Add(newBorrow);
                context.SaveChanges();
            }


        }


    }
}