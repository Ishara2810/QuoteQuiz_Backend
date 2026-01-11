using QuoteQuiz_Domain.Enums;

namespace QuoteQuiz_API.Dtos.Quiz
{
    public class SubmitAnswerDto
    {
        public Guid QuoteId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserAnswer { get; set; } = null!;
        public string? BinaryQuestionAuthor { get; set; }
        public QuizMode QuizMode { get; set; }
    }
}
