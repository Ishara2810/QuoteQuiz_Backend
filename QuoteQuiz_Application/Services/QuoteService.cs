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
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }
        public async Task<QuoteEntity?> AddAsync(QuoteEntity entity)
        {
            return await _quoteRepository.AddAsync(entity);
        }

        public async Task<IReadOnlyList<QuoteEntity>> GetAllAsync()
        {
            return await _quoteRepository.GetAllAsync();
        }

        public async Task<QuoteEntity?> GetByIdAsync(Guid id)
        {
            return await _quoteRepository.GetByIdAsync(id);
        }

        public async Task<QuoteEntity> UpdateAsync(QuoteEntity entity)
        {
            return await _quoteRepository.UpdateAsync(entity);
        }
    }
}
