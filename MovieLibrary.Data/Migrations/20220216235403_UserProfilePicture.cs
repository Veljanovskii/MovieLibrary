using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieLibrary.Data.Migrations
{
    public partial class UserProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Profile picture",
                table: "Customers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile picture",
                table: "Customers");
        }
    }
}
