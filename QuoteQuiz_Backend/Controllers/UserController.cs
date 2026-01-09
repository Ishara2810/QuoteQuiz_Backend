using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_API.Dtos.User;
using QuoteQuiz_Application.Services;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IServices;
using QuoteQuiz_Infrastructure.Data;
using System.Diagnostics;

namespace QuoteQuiz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IUserRoleService userRoleService, IMapper mapper)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserPostDto dto)
        {
            AspNetUserEntity user = new();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.IsActive = true;
            user.CreatedOn = DateTime.UtcNow;

            await _userService.CreateAsync(user, dto.Password);

            AspNetUserEntity? createdUser = await _userService.GetByEmailAsync(user.Email);

            AspNetUserRoleEntity userRole = new AspNetUserRoleEntity()
            {
                UserId = createdUser!.Id,
                RoleId = dto.RoleId,
            };
            var UserRole = await _userRoleService.AssignUserToRole(userRole);

            return Ok(new Result<AspNetUserEntity>(createdUser));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UserPostDto dto)
        {
            AspNetUserEntity? exists = await _userService.GetByIdAsync(id);
            if (exists == null) throw new Exception("User Doesn't Exist");

            AspNetUserEntity user = new();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.IsActive = true;
            user.Id = id;
            user.ModifiedOn = DateTime.UtcNow;
            //user.ModifiedBy = access!.UserId;

            await _userService.UpdateAsync(user);

            return Ok(new Result<UserPostDto>(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _userService.GetAllAsync();
            var userDto = _mapper.Map<IReadOnlyList<UserDto>>(items);

            return Ok(new Result<IReadOnlyList<UserDto>>(userDto));
        }
    }
}
