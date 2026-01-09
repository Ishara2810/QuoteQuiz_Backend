using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Domain.Interfaces.IRepositories.Shared;
using QuoteQuiz_Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Repositories.Shared
{
    public class GenericRepository<T, M> : IGenericRepository<T> where T : BaseEntity where M : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GenericRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetAllAsync()
        {
            IReadOnlyList<M> items = await _context.Set<M>().Where(x => !x.IsDeleted).OrderByDescending(x => x.Id).ToListAsync();
            return _mapper.Map<IReadOnlyList<T>>(items)!;
        }

        async Task<T?> IGenericRepository<T>.GetByIdAsync(Guid id)
        {
            M? item = await _context.Set<M>().Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            return _mapper.Map<T>(item);
        }

        async Task<T> IGenericRepository<T>.UpdateAsync(T entity)
        {
            var existing = await _context.Set<M>()
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted);

            if (existing == null)
                throw new Exception("Not Found");

            _mapper.Map(entity, existing);

            // Protect audit fields
            _context.Entry(existing).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(existing).Property(x => x.CreatedOn).IsModified = false;

            await _context.SaveChangesAsync();

            return _mapper.Map<T>(existing);
        }

        async Task<T?> IGenericRepository<T>.AddAsync(T entity)
        {
            M model = _mapper.Map<M>(entity)!;
            _context.Add(model);
            await _context.SaveChangesAsync();
            T result = _mapper.Map<T>(model);
            result.Id = model.Id;
            return result;
        }
    }
}
