using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChucksUsedDealership.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedsunjectline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectLine",
                table: "ContactForms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectLine",
                table: "ContactForms");
        }
    }
}
