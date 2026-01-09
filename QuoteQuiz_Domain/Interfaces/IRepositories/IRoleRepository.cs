using QuoteQuiz_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        public Task<IReadOnlyList<AspNetRoleEntity>> GetAllAsync();
        public Task<AspNetRoleEntity?> GetByIdAsync(string id);
        public Task<AspNetRoleEntity> GetByNameAsync(string name, int id);
        public Task AddAsync(AspNetRoleEntity entity);
        public Task UpdateAsync(AspNetRoleEntity entity);
    }
}
