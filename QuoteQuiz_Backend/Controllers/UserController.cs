using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_API.Dtos.User;
using QuoteQuiz_Application.Services;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IServices;
using QuoteQuiz_Infrastructure.Data;
using System.Diagnostics;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserPostDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            // Email already exists check
            var existingUser = await _userService.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }

            AspNetUserEntity user = new();
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.IsActive = true;
            user.CreatedOn = DateTime.UtcNow;
            user.CreatedBy = userId;

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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, UserPostDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            AspNetUserEntity? exists = await _userService.GetByIdAsync(id);
            if (exists == null) return BadRequest("User Doesn't Exist");

            var emailUser = await _userService.GetByEmailAsync(dto.Email);
            if (emailUser != null && emailUser.Id != id)
            {
                return BadRequest("Email already exists");
            }

            AspNetUserEntity user = new();
            user.Id = id;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UserName = dto.Email;
            user.NormalizedUserName = dto.Email!.ToUpper();
            user.Email = dto.Email;
            user.NormalizedEmail = dto.Email!.ToUpper();
            user.IsActive = dto.IsActive;
            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedBy = userId;

            await _userService.UpdateAsync(user);

            AspNetUserRoleEntity userRole = new AspNetUserRoleEntity()
            {
                UserId = id,
                RoleId = dto.RoleId,
            };
            var UserRole = await _userRoleService.AssignUserToRole(userRole);

            return Ok(new Result<UserPostDto>(dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _userService.GetAllAsync();
            var userDto = _mapper.Map<IReadOnlyList<UserDto>>(items);

            return Ok(new Result<IReadOnlyList<UserDto>>(userDto));
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(item);

            return Ok(new Result<UserDto>(userDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var item = await _userService.GetByIdAsync(id);
            if (item == null) return BadRequest("User Doesn't Exist");

            item.IsDeleted = true;
            item.IsActive = false;
            item.DeletedBy = userId;
            item.DeletedOn = DateTime.UtcNow;

            await _userService.RemoveAsync(item);

            return Ok(new Result<UserPostDto>(null));

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateUserStatusDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var item = await _userService.GetByIdAsync(id);
            if (item == null) return BadRequest("User Doesn't Exist");

            item.IsActive = dto.IsActive;
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;

            await _userService.UpdateStatusAsync(item);

            return Ok(new Result<UserPostDto>(null));

        }

        [Authorize(Roles = "Admin,User")]
        [HttpPatch("update-quiz-mode/{id}")]
        public async Task<IActionResult> UpdateQuizMode(string id, [FromBody] UpdateQuizModeDto dto)
        {
            var item = await _userService.GetByIdAsync(id);
            if (item == null) return BadRequest("User Doesn't Exist");

            item.QuizMode = dto.QuizMode;
            item.ModifiedBy = id;
            item.ModifiedOn = DateTime.UtcNow;

            await _userService.UpdateQuizMode(item);

            return Ok(new Result<UserPostDto>(null));

        }
    }
}
