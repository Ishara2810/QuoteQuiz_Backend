using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_API.Dtos.Role;
using QuoteQuiz_Domain.Interfaces.IServices;

namespace QuoteQuiz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _roleService.GetAllAsync();
            var itemsDto = _mapper.Map<List<RoleDto>>(items);
            return Ok(new Result<List<RoleDto>>(itemsDto));
        }
    }
}
