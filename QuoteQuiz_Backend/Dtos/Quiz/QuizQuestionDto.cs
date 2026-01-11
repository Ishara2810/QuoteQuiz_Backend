using QuoteQuiz_Domain.Enums;

namespace QuoteQuiz_API.Dtos.Quiz
{
    public class QuizQuestionDto
    {
        public Guid QuoteId { get; set; }
        public string QuoteText { get; set; } = null!;
        public QuizMode Mode { get; set; }

        // Binary
        public string? BinaryQuestionAuthor { get; set; }

        // Multiple choice
        public IList<string>? Options { get; set; }
    }
}
