using QuoteQuiz_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IRepositories
{
    public interface IUserRoleRepository
    {
        public Task<List<AspNetRoleEntity>> GetRolesByUserAsync(string userId);
        public Task<AspNetUserRoleEntity> AssignUserToRole(AspNetUserRoleEntity entity);
    }
}
