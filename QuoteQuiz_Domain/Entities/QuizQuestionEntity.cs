using QuoteQuiz_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Entities
{
    public class QuizQuestionEntity
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
