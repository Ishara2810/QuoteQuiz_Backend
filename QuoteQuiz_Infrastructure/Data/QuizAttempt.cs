using QuoteQuiz_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Data
{
    public class QuizAttempt
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey("Quote")]
        public Guid QuoteId { get; set; }
        public Quote Quote { get; set; } = null!;

        public QuizMode QuizMode { get; set; }

        // What user selected (Yes/No OR option text)
        public string UserAnswer { get; set; } = null!;

        // Actual correct answer
        public string CorrectAnswer { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public string? DisplayedAuthor { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
