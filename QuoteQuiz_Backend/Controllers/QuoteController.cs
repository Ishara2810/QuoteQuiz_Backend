using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Domain.Interfaces.IServices;
using System.Security.Claims;

namespace QuoteQuiz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuoteService _quoteService;
        public QuoteController(IMapper mapper, IQuoteService quoteService)
        {
            _mapper = mapper;
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _quoteService.GetAllAsync();
            var itemsDto = _mapper.Map<List<QuoteDto>>(items);
            return Ok(new Result<List<QuoteDto>>(itemsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Add(QuotePostDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var exists = await _quoteService.QuoteExistsAsync(model.Text, model.Author);
            if (exists)
            {
                return BadRequest("Quote with same text and author already exists");
            }

            var item = _mapper.Map<QuoteEntity>(model);
            item.CreatedBy = userId;
            await _quoteService.AddAsync(item);

            return Ok(new Result<QuotePostDto>(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, QuotePostDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var existing = await _quoteService.GetByIdAsync(id);
            if (existing == null) return BadRequest("Quote Not Found");

            var exists = await _quoteService.QuoteExistsAsync(model.Text, model.Author, excludeId: id);
            if (exists)
            {
                return BadRequest("Quote with same text and author already exists");
            }

            var item = _mapper.Map<QuoteEntity>(model);
            item.Id = id;
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;
            await _quoteService.UpdateAsync(item);

            return Ok(new Result<QuotePostDto>(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var item = await _quoteService.GetByIdAsync(id);
            if (item == null) return BadRequest("Quote Not Found");

            item.IsDeleted = true;
            item.IsActive = false;
            item.DeletedBy = userId;
            item.DeletedOn = DateTime.UtcNow;

            await _quoteService.UpdateAsync(item);

            return Ok(new Result<QuotePostDto>(null));

        }
    }
}
