using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_CookBook.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedDataForCategoryAndPriceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Soup" },
                    { 2, "Appetizer" },
                    { 3, "Main Course" },
                    { 4, "Salad" },
                    { 5, "Drink" },
                    { 6, "Dessert" },
                    { 7, "Side Dish" }
                });

            migrationBuilder.InsertData(
                table: "PriceCategory",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The total cost of the ingredients is below 3000 Ft.", "Inexpensive" },
                    { 2, "The total cost of the ingredients is between 3001 and 6000 Ft.", "Moderate" },
                    { 3, "The total cost of the ingredients is over 6001 Ft.", "Expensive" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PriceCategory",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PriceCategory",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PriceCategory",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
