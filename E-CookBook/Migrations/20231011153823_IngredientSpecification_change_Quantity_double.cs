using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CookBook.Migrations
{
    /// <inheritdoc />
    public partial class IngredientSpecification_change_Quantity_double : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qunatity",
                table: "IngredientSpecification");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "IngredientSpecification",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "IngredientSpecification");

            migrationBuilder.AddColumn<int>(
                name: "Qunatity",
                table: "IngredientSpecification",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
