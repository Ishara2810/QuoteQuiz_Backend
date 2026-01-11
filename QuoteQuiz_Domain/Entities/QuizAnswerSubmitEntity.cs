using QuoteQuiz_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Entities
{
    public class QuizAnswerSubmitEntity
    {
        public Guid QuoteId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserAnswer { get; set; } = null!;
        public string? BinaryQuestionAuthor { get; set; }
        public QuizMode QuizMode { get; set; }
    }
}
