using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Product_Model_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                schema: "Catalog",
                table: "ProductImage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryImageFileName",
                schema: "Catalog",
                table: "Product",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                schema: "Catalog",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "PrimaryImageFileName",
                schema: "Catalog",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                schema: "Catalog",
                table: "ProductImage",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
