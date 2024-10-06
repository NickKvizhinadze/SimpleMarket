using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMarket.Customers.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "catalog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "catalog",
                newName: "Customers");
        }
    }
}
