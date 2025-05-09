using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMarket.Catalog.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Brands_BrandId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Categories_CategoryId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "catalog");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brands",
                newSchema: "catalog");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Products",
                newSchema: "catalog");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CategoryId",
                schema: "catalog",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BrandId",
                schema: "catalog",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "catalog",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalog",
                table: "Products",
                column: "BrandId",
                principalSchema: "catalog",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalog",
                table: "Products",
                column: "CategoryId",
                principalSchema: "catalog",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "catalog",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "catalog",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "catalog",
                newName: "Brands");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "catalog",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Customers",
                newName: "IX_Customers_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Customers",
                newName: "IX_Customers_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Brands_BrandId",
                table: "Customers",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Categories_CategoryId",
                table: "Customers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
