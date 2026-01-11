using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteQuiz_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDisplayedAuthorColumnToQuizAttemptTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayedAuthor",
                table: "QuizAttempts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayedAuthor",
                table: "QuizAttempts");
        }
    }
}
