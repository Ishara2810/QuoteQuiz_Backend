using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Infrastructure.Data;
using QuoteQuiz_Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Repositories
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuizAttemptRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(QuizAttemptEntity entity)
        {
            QuizAttempt model = _mapper.Map<QuizAttempt>(entity);
            await _context.QuizAttempts.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<QuizAttemptEntity>> GetAllAsync()
        {
            var modelList = await _context.QuizAttempts
                .Include(x => x.User)
                .Include(x => x.Quote)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return _mapper.Map<List<QuizAttemptEntity>>(modelList);
        }
    }
}
