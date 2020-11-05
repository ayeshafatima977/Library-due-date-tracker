using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//@Link :https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application#create-the-data-model 

namespace LibraryDay3.Models
{
    public class LibraryContext :DbContext
    {
        public LibraryContext()
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if ( !optionsBuilder.IsConfigured )
            {
                string connection =
                    "server=localhost;"+
                    "port=3306;"+
                    "user=root;"+
                    "database=mvc_library;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection,x => x.ServerVersion(version));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
                entity.HasData(
                     new Author()
                     {
                         ID=-1,
                         Name="J.K.Rowling",
                         BirthDate=new DateTime(1985,12,19)
                     },
                     new Author()
                     {
                         ID=-2,
                         Name="William ShakeSpeare",
                         BirthDate=new DateTime(1676,10,19)
                     },
                     new Author()
                     {
                         ID=-3,
                         Name="Alice Walker",
                         BirthDate=new DateTime(1865,05,14)
                     },
                    new Author()
                    {
                        ID=-4,
                        Name="Rachel Kushner",
                        BirthDate=new DateTime(1735,11,30),
                        DeathDate=new DateTime(1910,04,21)
                    },
                   new Author()
                   {
                       ID=-5,
                       Name="J.K. Rowling",
                       BirthDate=new DateTime(1965,05,31)
                   }
                );


            });

            //Model Builder for each Foreign Key Index

            modelBuilder.Entity<Book>(entity =>
            {
                string keyForBook = "FK_"+nameof(Book)+
                   "_"+nameof(Author);

                //ForeigKey
                entity.HasIndex(e => e.AuthorID)
                .HasName(keyForBook);
                //Book title
                entity.Property(e => e.Title)
                .HasCharSet("utf8mb4")
                 .HasCollation("utf8mb4_general_ci");
                entity.HasOne(thisEntity => thisEntity.Author)
                .WithMany(parent => parent.Books)
                .HasForeignKey(thisEntity => thisEntity.AuthorID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName(keyForBook);

                /*“Books” by the same Author.   At least 3 “Books” by the same Author.
                These three books must have a “CheckoutDate” equal to December 25, 2019.One book must be returned on-time with no extension. One book must be returned on-time with one extension. One book must not have been returned at all!*/

                entity.HasData(
                            new Book
                            {
                                ID=-1,
                                Title="Measure for Measure",
                                AuthorID=-1,
                                PublicationDate=new DateTime(1994,04,01),

                            },

                            new Book
                            {
                                ID=-2,
                                Title="Harry Potter and the Order of the Phoenix",
                                AuthorID=-5,
                                PublicationDate=new DateTime(1800,02,04),
                            },
                            new Book
                            {
                                ID=-3,
                                Title="Hamlet",
                                AuthorID=-1,
                                PublicationDate=new DateTime(1980,03,06),

                            },

                            new Book
                            {
                                ID=-4,
                                Title="Harry Potter And The Philosopher's Stone",
                                AuthorID=-5,
                                PublicationDate=new DateTime(2002,07,20),
                            },

                            new Book
                            {
                                ID=-5,
                                Title="The Casual Vacancy",
                                AuthorID=-5,
                                PublicationDate=new DateTime(2012,11,10),
                            }
                            );
            });

            //  These three books must have a “CheckoutDate” equal to December 25, 2019.One book must be returned on-time with no extension. One book must be returned on-time with one extension. One book must not have been returned at all!*/


            modelBuilder.Entity<Borrow>(entity =>
            {
                string keyForBorrow = "FK_"+nameof(Borrow)+
                    "_"+nameof(Book);
                entity.HasIndex(e => e.BookID)
                    .HasName(keyForBorrow);
                entity.HasOne(thisEntity => thisEntity.Book)
                    .WithMany(parent => parent.Borrows)
                    .HasForeignKey(thisEntity => thisEntity.BookID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName(keyForBorrow);
                entity.HasData(
                    new Borrow()
                    {
                        ID=-1,
                        BookID=-2,
                        CheckedOutDate=new DateTime(2019,12,25),
                        DueDate=new DateTime(2019,12,25).AddDays(14),
                        ReturnedDate=new DateTime(2020,01,02),
                        //One book must be returned on-time with no extension
                        //Setting extension Count to 0 by default
                        ExtensionCount=0

                    },
                    new Borrow()
                    {
                        ID=-2,
                        BookID=-4,
                        CheckedOutDate=new DateTime(2019,12,25),
                        //ne book must be returned on-time with one extension.
                        DueDate=new DateTime(2019,12,25).AddDays(14).AddDays(7),
                        ReturnedDate=new DateTime(2019,12,25).AddDays(14).AddDays(4),
                        ExtensionCount=0
                    },
                   new Borrow()
                   {
                       ID=-3,
                       BookID=-5,
                       DueDate=new DateTime(2019,12,25).AddDays(14),
                       //One book must not have been returned at all!
                       ReturnedDate=null,
                       ExtensionCount=0
                   },
                   new Borrow()
                   {
                       ID=-4,
                       BookID=-5,
                       CheckedOutDate=new DateTime(2020,11,04),
                       DueDate=new DateTime(2020,11,23).AddDays(14),
                       ReturnedDate=new DateTime(2020,11,22),
                       ExtensionCount=0
                   },
                    new Borrow()
                    {
                        ID=-5,
                        BookID=-1,
                        CheckedOutDate=new DateTime(2020,11,05),
                        DueDate=new DateTime(2020,11,23).AddDays(14),
                        ReturnedDate=new DateTime(2020,11,22),
                        ExtensionCount=0
                    }
                );
            });

        }
    }
}