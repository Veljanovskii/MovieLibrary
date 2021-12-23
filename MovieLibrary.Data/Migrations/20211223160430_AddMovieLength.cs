using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieLibrary.Data.Migrations
{
    public partial class AddMovieLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    MaritalStatusID = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.MaritalStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    MovieLength = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaritalStatusID = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_MaritalStatuses",
                        column: x => x.MaritalStatusID,
                        principalTable: "MaritalStatuses",
                        principalColumn: "MaritalStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MaritalStatuses",
                columns: new[] { "MaritalStatusID", "Caption" },
                values: new object[,]
                {
                    { 1, "Single" },
                    { 2, "Married" },
                    { 3, "Divorced" },
                    { 4, "Widowed" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieID", "Caption", "DeleteDate", "InsertDate", "MovieLength", "ReleaseYear" },
                values: new object[,]
                {
                    { 1, "The Wolf of Wallstreet", null, new DateTime(2021, 12, 21, 15, 50, 35, 703, DateTimeKind.Unspecified), 180, 2013 },
                    { 2, "Pulp Fiction", null, new DateTime(2021, 12, 22, 11, 43, 11, 777, DateTimeKind.Unspecified), 140, 1994 },
                    { 3, "Avatar", new DateTime(2021, 12, 22, 16, 17, 50, 937, DateTimeKind.Unspecified), new DateTime(2021, 12, 22, 16, 14, 25, 437, DateTimeKind.Unspecified), 170, 2009 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DeleteDate", "FirstName", "IDNumber", "InsertDate", "LastName", "MaritalStatusID" },
                values: new object[] { 1, "123 Main St", null, "John", "123456789", new DateTime(2021, 12, 22, 18, 30, 28, 537, DateTimeKind.Unspecified), "Doe", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DeleteDate", "FirstName", "IDNumber", "InsertDate", "LastName", "MaritalStatusID" },
                values: new object[] { 2, "321 5th St", null, "Jane", "987654321", new DateTime(2021, 12, 22, 18, 40, 11, 610, DateTimeKind.Unspecified), "Doe", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MaritalStatusID",
                table: "Users",
                column: "MaritalStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");
        }
    }
}
