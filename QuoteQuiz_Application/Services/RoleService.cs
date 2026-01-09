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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task AddAsync(AspNetRoleEntity entity)
        {
            await _roleRepository.AddAsync(entity);
        }

        public async Task<IReadOnlyList<AspNetRoleEntity>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<AspNetRoleEntity?> GetByIdAsync(string id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(AspNetRoleEntity entity)
        {
            await _roleRepository.UpdateAsync(entity);
        }
    }
}
