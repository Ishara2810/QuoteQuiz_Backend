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
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<AspNetUserRoleEntity> AssignUserToRole(AspNetUserRoleEntity entity)
        {
            return await _userRoleRepository.AssignUserToRole(entity);
        }

        public async Task<List<AspNetRoleEntity>> GetRolesByUserAsync(string userId)
        {
            return await _userRoleRepository.GetRolesByUserAsync(userId);
        }
    }
}
