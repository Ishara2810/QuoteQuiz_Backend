using QuoteQuiz_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IRepositories
{
    public interface IQuizAttemptRepository
    {
        public Task<IReadOnlyList<QuizAttemptEntity>> GetAllAsync();
        public Task AddAsync(QuizAttemptEntity entity);
    }
}
