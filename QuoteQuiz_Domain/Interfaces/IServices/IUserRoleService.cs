using QuoteQuiz_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IServices
{
    public interface IUserRoleService
    {
        public Task<AspNetUserRoleEntity> AssignUserToRole(AspNetUserRoleEntity entity);
        public Task<List<AspNetRoleEntity>> GetRolesByUserAsync(string userId);
    }
}
