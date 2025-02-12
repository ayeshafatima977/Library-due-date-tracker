﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDay3.Models
{
    [Table("book")]
    public class Book
    {
        public Book()
        {
            Borrows = new HashSet<Borrow>();
        }


        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        // int “AuthorID” - int(10) (foreign key)
        [Column("author_id", TypeName = "int(10)")]
        [Required]
        public int AuthorID { get; set; }

        [Column("title", TypeName = "varchar(100)")]
        [Required]
        public string Title { get; set; }

        [Column("publicationDate", TypeName = "date")]
        [Required]
        public DateTime PublicationDate { get; set; }

        // Points to the property representing the foreign key column.
        [ForeignKey(nameof(AuthorID))]
        [InverseProperty(nameof(Models.Author.Books))]
        public virtual Author Author { get; set; }

        [InverseProperty(nameof(Borrow.Book))] public virtual ICollection<Borrow> Borrows { get; set; }
    }
}