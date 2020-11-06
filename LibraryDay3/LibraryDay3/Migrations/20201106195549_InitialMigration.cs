using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDay3.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    deathDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    author_id = table.Column<int>(type: "int(10)", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    publicationDate = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_Author",
                        column: x => x.author_id,
                        principalTable: "Authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    book_id = table.Column<int>(type: "int(10)", nullable: false),
                    checked_out_date = table.Column<DateTime>(type: "date", nullable: false),
                    due_date = table.Column<DateTime>(type: "date", nullable: false),
                    returned_date = table.Column<DateTime>(type: "date", nullable: true),
                    ExtensionCount = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.id);
                    table.ForeignKey(
                        name: "FK_Borrow_Book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "id", "BirthDate", "deathDate", "name" },
                values: new object[,]
                {
                    { -1, new DateTime(1985, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K.Rowling" },
                    { -2, new DateTime(1676, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William ShakeSpeare" },
                    { -3, new DateTime(1865, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Walker" },
                    { -4, new DateTime(1735, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1910, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rachel Kushner" },
                    { -5, new DateTime(1775, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Austen" }
                });

            migrationBuilder.InsertData(
                table: "book",
                columns: new[] { "ID", "author_id", "DueDate", "publicationDate", "ReturnedDate", "title" },
                values: new object[,]
                {
                    { -1, -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Measure for Measure" },
                    { -2, -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1800, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter and the Order of the Phoenix" },
                    { -4, -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter And The Philosopher's Stone" },
                    { -3, -2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hamlet" },
                    { -5, -3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "The Casual Vacancy" }
                });

            migrationBuilder.InsertData(
                table: "Borrows",
                columns: new[] { "id", "book_id", "checked_out_date", "due_date", "ExtensionCount", "returned_date" },
                values: new object[,]
                {
                    { -3, -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null },
                    { -5, -1, new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -1, -2, new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -2, -4, new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -4, -5, new DateTime(2020, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "FK_Book_Author",
                table: "book",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "FK_Borrow_Book",
                table: "Borrows",
                column: "book_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
