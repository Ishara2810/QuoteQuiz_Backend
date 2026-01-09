using QuoteQuiz_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Entities
{
    public class QuoteEntity : BaseEntity
    {
        public string Text { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}
