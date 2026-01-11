using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Application.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepo;

        public QuizAttemptService(IQuizAttemptRepository quizAttemptRepo)
        {
            _quizAttemptRepo = quizAttemptRepo;
        }

        public async Task<IReadOnlyList<QuizAttemptEntity>> GetAllAsync()
        {
            return await _quizAttemptRepo.GetAllAsync();
        }
    }
}
