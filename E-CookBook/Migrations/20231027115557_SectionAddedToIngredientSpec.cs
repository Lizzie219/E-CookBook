using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CookBook.Migrations
{
    /// <inheritdoc />
    public partial class SectionAddedToIngredientSpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "IngredientSpecification",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "IngredientSpecification");
        }
    }
}
