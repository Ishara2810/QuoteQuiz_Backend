using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Infrastructure.Data;
using QuoteQuiz_Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuoteQuiz_Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(AppDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task AddAsync(AspNetRoleEntity entity)
        {
            IdentityRole model = _mapper.Map<IdentityRole>(entity);
            await _context.Roles.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<AspNetRoleEntity>> GetAllAsync()
        {
            var modelList = await _context.Roles
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return _mapper.Map<List<AspNetRoleEntity>>(modelList);
        }

        public async Task<AspNetRoleEntity?> GetByIdAsync(string id)
        {
            var model = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;
            return _mapper.Map<AspNetRoleEntity>(model);
        }

        public Task<AspNetRoleEntity> GetByNameAsync(string name, int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AspNetRoleEntity entity)
        {
            var exists = _context.Roles.Any(r => r.Id == entity.Id);
            if (!exists)
                throw new Exception("Not Found");
            IdentityRole model = _mapper.Map<IdentityRole>(entity);
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
