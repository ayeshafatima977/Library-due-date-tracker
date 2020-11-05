using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDay3.Models
{
    public class Author
    {
        public Author()
        {
            Books=new HashSet<Book>();
        }
        // int “ID” - int(10) (primary key)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id",TypeName = "int(10)")]
        public int ID { get; set; }

        // string “Name” - varchar(60)
        [Column("name",TypeName = "varchar(60)")]
        [Required]
        public string Name { get; set; }

        //date "BirthDate" NOT NULL
        [Column(TypeName = "date")]
        [Required]
        public DateTime BirthDate { get; set; }

        //date "DeathDate" date -NULL
        [Column("deathDate",TypeName = "date")]
        public DateTime DeathDate { get; set; }

        [InverseProperty(nameof(Models.Book.Author))]
        public virtual ICollection<Book> Books { get; set; }


    }
}