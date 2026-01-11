using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Enums;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuoteRepository _quoteRepo;
        private readonly IUserRepository _userRepo;
        private readonly IQuizAttemptRepository _quizAttemptRepo;

        public QuizService(IQuoteRepository quoteRepo, IUserRepository userRepo, IQuizAttemptRepository quizAttemptRepo)
        {
            _quoteRepo = quoteRepo;
            _userRepo = userRepo;
            _quizAttemptRepo = quizAttemptRepo;
        }
        public async Task<QuizQuestionEntity> GetNextQuestionAsync(string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
            ?? throw new Exception("User not found");

            var quote = await GetRandomQuoteAsync();

            if (quote == null)
                return new QuizQuestionEntity
                {
                    QuoteId = Guid.Empty,
                    QuoteText = "",
                    Mode = user.QuizMode
                };

            return user.QuizMode switch
            {
                QuizMode.Binary => await BuildBinaryQuestion(quote),
                QuizMode.MultipleChoice => await BuildMultipleChoiceQuestionAsync(quote),
                _ => throw new Exception("Invalid quiz mode")
            };
        }

        public async Task<QuizAttemptEntity> SubmitAnswerAsync(QuizAnswerSubmitEntity entity)
        {
            var user = await _userRepo.GetByIdAsync(entity.UserId)
        ?? throw new Exception("User not found");

            var quote = await _quoteRepo.GetByIdAsync(entity.QuoteId)
                ?? throw new Exception("Quote not found");

            string correctAnswer;
            bool isCorrect;

            switch (entity.QuizMode)
            {
                case QuizMode.Binary:
                    bool authorMatches = string.Equals(
                        quote.Author?.Trim(),
                        entity.BinaryQuestionAuthor?.Trim(),
                        StringComparison.OrdinalIgnoreCase
                    );

                    correctAnswer = authorMatches ? "Yes" : "No";

                    isCorrect = string.Equals(
                        entity.UserAnswer.Trim(),
                        correctAnswer,
                        StringComparison.OrdinalIgnoreCase
                    );
                    break;

                case QuizMode.MultipleChoice:
                    correctAnswer = quote.Author;

                    isCorrect = string.Equals(
                        entity.UserAnswer.Trim(),
                        quote.Author.Trim(),
                        StringComparison.OrdinalIgnoreCase
                    );
                    break;

                default:
                    throw new Exception("Invalid quiz mode");
            }

            var attempt = new QuizAttemptEntity
            {
                UserId = entity.UserId,
                QuoteId = quote.Id,
                QuizMode = entity.QuizMode,
                UserAnswer = entity.UserAnswer,
                CorrectAnswer = correctAnswer,
                IsCorrect = isCorrect,
                DisplayedAuthor = entity.QuizMode == QuizMode.Binary ? entity.BinaryQuestionAuthor : "N/A",
                AnsweredAt = DateTime.UtcNow
            };

            await _quizAttemptRepo.AddAsync(attempt);

            attempt.Quote = quote;

            return attempt;
        }

        private async Task<QuizQuestionEntity> BuildBinaryQuestion(QuoteEntity quote)
        {
            
            var showCorrectAuthor = Random.Shared.Next(0, 2) == 1;
            var randomAuthor = (await _quoteRepo.GetAllAsync())
                .Select(q => q.Author)
                .Distinct()
                .OrderBy(_ => Guid.NewGuid())
                .First();
            var displayedAuthor = showCorrectAuthor
                ? quote.Author
                : randomAuthor;

            return new QuizQuestionEntity
            {
                QuoteId = quote.Id,
                QuoteText = quote.Text,
                Mode = QuizMode.Binary,
                BinaryQuestionAuthor = displayedAuthor
            };
        }

        private async Task<QuizQuestionEntity> BuildMultipleChoiceQuestionAsync(QuoteEntity quote)
        {
            var authors = (await _quoteRepo.GetAllAsync())
                .Select(q => q.Author)
                .Distinct()
                .Where(a => a != quote.Author)
                .OrderBy(_ => Guid.NewGuid())
                .Take(2)
                .ToList();

            var options = new List<string>
            {
                quote.Author,
                authors[0],
                authors[1]
            }.OrderBy(_ => Guid.NewGuid()).ToList();

            return new QuizQuestionEntity
            {
                 QuoteId = quote.Id,
                 QuoteText = quote.Text,
                 Mode = QuizMode.MultipleChoice,
                 Options = options
            };
        }

        private async Task<QuoteEntity?> GetRandomQuoteAsync()
        {
            var quotes = await _quoteRepo.GetAllAsync();

            if (quotes == null || !quotes.Any())
                return null;

            return quotes
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
