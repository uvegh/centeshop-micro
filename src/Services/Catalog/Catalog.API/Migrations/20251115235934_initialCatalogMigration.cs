using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class initialCatalogMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "StockQuantity", "UpdatedAT" },
                values: new object[,]
                {
                    { new Guid("0bdab38a-2dd1-4ec5-90ef-3eac44b19444"), new DateTime(2025, 11, 15, 23, 59, 33, 694, DateTimeKind.Utc).AddTicks(8810), "Lenovo", 999.99m, 10, new DateTime(2025, 11, 15, 23, 59, 33, 694, DateTimeKind.Utc).AddTicks(8812) },
                    { new Guid("d9261391-ea5a-486d-aa28-abeb52653d74"), new DateTime(2025, 11, 15, 23, 59, 33, 694, DateTimeKind.Utc).AddTicks(8815), "Iphone 15", 800.77m, 10, new DateTime(2025, 11, 15, 23, 59, 33, 694, DateTimeKind.Utc).AddTicks(8815) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
