using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Infrastructure.Data;
using QuoteQuiz_Infrastructure.DBContext;
using QuoteQuiz_Infrastructure.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Repositories
{
    public class QuoteRepository : GenericRepository<QuoteEntity, Quote>, IQuoteRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public QuoteRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> QuoteExistsAsync(string text, string author, Guid? excludeId = null)
        {
            var query = _context.Quotes.AsQueryable().Where(q =>
                q.Text.ToLower() == text.ToLower() &&
                q.Author.ToLower() == author.ToLower() &&
                !q.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(q => q.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
