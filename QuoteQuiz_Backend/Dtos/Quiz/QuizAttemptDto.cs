using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.User;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Enums;

namespace QuoteQuiz_API.Dtos.Quiz
{
    public class QuizAttemptDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;
        //public AspNetUserEntity User { get; set; } = null!;

        public Guid QuoteId { get; set; }
        //public QuoteEntity Quote { get; set; } = null!;

        public QuizMode QuizMode { get; set; }

        // What user selected (Yes/No OR option text)
        public string UserAnswer { get; set; } = null!;

        // Actual correct answer
        public string? CorrectAnswer { get; set; } = null;

        public string? DisplayedAuthor { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        public UserDto User { get; set; } = null!;
        public QuoteDto Quote { get; set; } = null!;
    }
}
