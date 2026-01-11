using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteQuiz_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuizModeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizMode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizMode",
                table: "AspNetUsers");
        }
    }
}
