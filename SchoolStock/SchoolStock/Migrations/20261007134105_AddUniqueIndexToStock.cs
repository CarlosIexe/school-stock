using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolStock.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId_SchoolId",
                table: "Stocks",
                columns: new[] { "ProductId", "SchoolId" },
                unique: true,
                filter: "[SchoolId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId_SchoolId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");
        }
    }
}
