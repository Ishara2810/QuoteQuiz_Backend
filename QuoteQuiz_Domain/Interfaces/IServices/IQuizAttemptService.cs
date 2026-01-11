using QuoteQuiz_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IServices
{
    public interface IQuizAttemptService
    {
        public Task<IReadOnlyList<QuizAttemptEntity>> GetAllAsync();
    }
}
