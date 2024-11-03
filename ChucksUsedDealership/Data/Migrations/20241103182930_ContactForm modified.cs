using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChucksUsedDealership.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContactFormmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactForms",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ContactForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ContactForms");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ContactForms",
                newName: "Name");
        }
    }
}
