using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<IReadOnlyList<AspNetUserEntity>> GetAllAsync();
        public Task<AspNetUserEntity> GetByIdAsync(string id);
        Task<AspNetUserEntity?> GetByEmailAsync(string email);
        public Task CreateAsync(AspNetUserEntity user, string password);
        public Task UpdateAsync(AspNetUserEntity user);
        public Task RemoveAsync(AspNetUserEntity user);
        public Task UpdateStatusAsync(AspNetUserEntity user);
        public Task UpdateQuizMode(AspNetUserEntity user);
    }
}
