using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IRepositories;
using QuoteQuiz_Infrastructure.Data;
using QuoteQuiz_Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(AspNetUserEntity user, string password)
        {
            ApplicationUser userModel = _mapper.Map<ApplicationUser>(user);
            userModel.UserName = userModel.Email;
            var result = await _userManager.CreateAsync(userModel, password);
            if (!result.Succeeded)
                throw new Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<IReadOnlyList<AspNetUserEntity>> GetAllAsync()
        {
            var usersWithRoles = await _context.Users
            .Where(u => !u.IsDeleted)
            .GroupJoin(
                _context.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, urj) => new { u, urj }
            )
            .SelectMany(
                x => x.urj.DefaultIfEmpty(),
                (x, ur) => new { x.u, ur }
            )
            .GroupJoin(
                _context.Roles,
                x => x.ur.RoleId,
                r => r.Id,
                (x, rj) => new { x.u, x.ur, rj }
            )
            .SelectMany(
                x => x.rj.DefaultIfEmpty(),
                (x, r) => new AspNetUserEntity
                {
                    Id = x.u.Id,
                    UserName = x.u.UserName,
                    Email = x.u.Email,
                    FirstName = x.u.FirstName,
                    LastName = x.u.LastName,
                    IsActive = x.u.IsActive,
                    RoleId = r != null ? r.Id : null,
                    RoleName = r != null ? r.Name : null
                }
            )
            .ToListAsync();

            return usersWithRoles;
        }

        public async Task<AspNetUserEntity?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<AspNetUserEntity?>(user);
        }

        public async Task<AspNetUserEntity> GetByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<AspNetUserEntity>(user);
        }

        public async Task RemoveAsync(AspNetUserEntity user)
        {
            var exists = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(t => t.Id == user.Id && !t.IsDeleted);
            if (exists == null) throw new Exception("User Not Found");
            exists.IsDeleted = user.IsDeleted;
            exists.DeletedBy = user.DeletedBy;
            exists.DeletedOn = user.DeletedOn;
            await _userManager.UpdateAsync(exists);
        }

        public async Task UpdateAsync(AspNetUserEntity user)
        {
            var exists = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(t => t.Id == user.Id && !t.IsDeleted);
            if (exists == null) throw new Exception("User Not Found");
            exists.FirstName = user.FirstName;
            exists.LastName = user.LastName;
            exists.Email = user.Email;
            exists.NormalizedEmail = user.Email!.ToUpper();
            exists.UserName = user.Email;
            exists.NormalizedUserName = user.Email!.ToUpper();
            exists.ModifiedBy = user.ModifiedBy;
            exists.ModifiedOn = user.ModifiedOn;
            await _userManager.UpdateAsync(exists);
        }

        public async Task UpdateStatusAsync(AspNetUserEntity user)
        {
            var exists = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(t => t.Id == user.Id && !t.IsDeleted);
            if (exists == null) throw new Exception("User Not Found");
            exists.IsActive = user.IsActive;
            exists.ModifiedBy = user.ModifiedBy;
            exists.ModifiedOn = user.ModifiedOn;
            await _userManager.UpdateAsync(exists);
        }

        public async Task UpdateQuizMode(AspNetUserEntity user)
        {
            var exists = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(t => t.Id == user.Id && !t.IsDeleted);
            if (exists == null) throw new Exception("User Not Found");
            exists.QuizMode = user.QuizMode;
            exists.ModifiedBy = user.ModifiedBy;
            exists.ModifiedOn = user.ModifiedOn;
            await _userManager.UpdateAsync(exists);
        }
    }
}
