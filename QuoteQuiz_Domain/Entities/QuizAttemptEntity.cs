using QuoteQuiz_Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Entities
{
    public class QuizAttemptEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;
        public AspNetUserEntity User { get; set; } = null!;

        public Guid QuoteId { get; set; }
        public QuoteEntity Quote { get; set; } = null!;

        public QuizMode QuizMode { get; set; }

        // What user selected (Yes/No OR option text)
        public string UserAnswer { get; set; } = null!;

        // Actual correct answer
        public string CorrectAnswer { get; set; } = null!;

        public string? DisplayedAuthor { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
