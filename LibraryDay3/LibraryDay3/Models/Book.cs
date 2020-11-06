using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDay3.Models
{
    [Table("book")]
    public class Book
    {


        public Book()
        {
            Borrows=new HashSet<Borrow>();
        }

        //Thankyou Oleg for the Work Around 
        public string GetCheckOutDate
        {

           get {
                var checkout = "This book hasnt been Checked out";
                    if(Borrows.LastOrDefault()!=null)
                {
                    checkout=Borrows.LastOrDefault().CheckedOutDate.ToShortDateString();
                }
                return checkout;
                    }
        }
     public string GetDueDate
        {
            get
            {
                var due = "This book Passed Due date";
                if ( Borrows.LastOrDefault()!=null )
                {
                    due=Borrows.LastOrDefault().DueDate.ToShortDateString();
                }
                return due;
            }
        }
        public string GetReturnDate
        {

            get
            {
                var returnDate = "This book hasnt been Returned";
                if ( Borrows.LastOrDefault()!=null )
                {
                    returnDate=Borrows.LastOrDefault().CheckedOutDate.ToShortDateString();
                }
                return returnDate;
            }
        }

        
        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        // int “AuthorID” - int(10) (foreign key)
        [Column("author_id",TypeName = "int(10)")]
        [Required]
        public int AuthorID { get; set; }

        [Column("title",TypeName = "varchar(100)")]
        [Required]
        public string Title { get; set; }

        [Column("publicationDate",TypeName = "date")]
        [Required]
        public DateTime PublicationDate { get; set; }

    //    public DateTime DueDate { get; set; }

       // public DateTime? ReturnedDate { get; set; }


        // Points to the property representing the foreign key column.
        [ForeignKey(nameof(AuthorID))]

        [InverseProperty(nameof(Models.Author.Books))]
        public virtual Author Author { get; set; }

        [InverseProperty(nameof(Models.Borrow.Book))]
        public virtual ICollection<Borrow> Borrows { get; set; }
        //public DateTime DueDate { get; internal set; }
    }
}