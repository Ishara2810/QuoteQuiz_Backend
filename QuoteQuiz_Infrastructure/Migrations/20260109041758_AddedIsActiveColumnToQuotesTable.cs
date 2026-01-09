using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteQuiz_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveColumnToQuotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Quotes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Quotes");
        }
    }
}
