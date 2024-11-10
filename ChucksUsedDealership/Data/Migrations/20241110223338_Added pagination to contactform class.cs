using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChucksUsedDealership.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedpaginationtocontactformclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactFormId1",
                table: "ContactForms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "ContactForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageSize",
                table: "ContactForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPages",
                table: "ContactForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_ContactFormId1",
                table: "ContactForms",
                column: "ContactFormId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_ContactForms_ContactFormId1",
                table: "ContactForms",
                column: "ContactFormId1",
                principalTable: "ContactForms",
                principalColumn: "ContactFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_ContactForms_ContactFormId1",
                table: "ContactForms");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_ContactFormId1",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "ContactFormId1",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "PageSize",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "TotalPages",
                table: "ContactForms");
        }
    }
}
