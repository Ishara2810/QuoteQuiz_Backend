using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public UserRoleRepository(AppDbContext context, IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        public async Task<AspNetUserRoleEntity> AssignUserToRole(AspNetUserRoleEntity entity)
        {
            var existingRoles = _context.UserRoles
                .Where(ur => ur.UserId == entity.UserId);

            _context.UserRoles.RemoveRange(existingRoles);

            var model = _mapper.Map<IdentityUserRole<string>>(entity);

            _context.UserRoles.Add(model);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<List<AspNetRoleEntity>> GetRolesByUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
