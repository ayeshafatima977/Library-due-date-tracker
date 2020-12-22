using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDay3.Models
{
    public class Borrow
    {
        // int “ID” - int(10) (primary key)
        [Key]
        [Column("id", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Column("book_id", TypeName = "int(10)")]
        [Required]
        public int BookID { get; set; }

        // DateTime “CheckedOutDate” - date
        [Column("checked_out_date", TypeName = "date")]
        [Required]
        public DateTime CheckedOutDate { get; set; }

        // DateTime “DueDate” - date
        [Column("due_date", TypeName = "date")]
        [Required]
        public DateTime DueDate { get; set; }

        // DateTime “ReturnedDate” - date (NULL)
        [Column("returned_date", TypeName = "date")]
        public DateTime? ReturnedDate { get; set; }

        // Add Property to use “ExtensionCount” - int(10), not nullable. For Checking Extension 
        [Required]
        [Column("ExtensionCount", TypeName = "int(10)")]
        public int ExtensionCount { get; set; }

        [ForeignKey(nameof(BookID))]
        // Requisite virtual property and constructor for foreign key.
        [InverseProperty(nameof(Models.Book.Borrows))]
        public virtual Book Book { get; set; }
    }
}