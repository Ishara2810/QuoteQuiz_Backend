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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(AspNetUserEntity user, string password)
        {
            await _userRepository.CreateAsync(user, password);
        }

        public async Task<IReadOnlyList<AspNetUserEntity>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<AspNetUserEntity?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<AspNetUserEntity> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(AspNetUserEntity user)
        {
            await _userRepository.RemoveAsync(user);
        }

        public async Task UpdateAsync(AspNetUserEntity user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateStatusAsync(AspNetUserEntity user)
        {
            await _userRepository.UpdateStatusAsync(user);
        }

        public async Task UpdateQuizMode(AspNetUserEntity user)
        {
            await _userRepository.UpdateQuizMode(user);
        }
    }
}
