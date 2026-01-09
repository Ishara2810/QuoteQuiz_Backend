using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Domain.Interfaces.IServices;

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
            //var access = HttpContext.Items["Access"] as AccessDto;
            var item = _mapper.Map<QuoteEntity>(model);
            //item.CreatedBy = access!.UserId;
            await _quoteService.AddAsync(item);

            return Ok(new Result<QuotePostDto>(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, QuotePostDto model)
        {
            //var access = HttpContext.Items["Access"] as AccessDto;
            var existing = await _quoteService.GetByIdAsync(id);
            if (existing == null) throw new Exception("Bank Not Found");

            var item = _mapper.Map<QuoteEntity>(model);
            item.Id = id;
            //item.ModifiedBy = access!.UserId;
            item.ModifiedOn = DateTime.UtcNow;
            await _quoteService.UpdateAsync(item);

            return Ok(new Result<QuotePostDto>(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var access = HttpContext.Items["Access"] as AccessDto;

            var item = await _quoteService.GetByIdAsync(id);
            if (item == null) throw new Exception("Bank Not Found");

            item.IsDeleted = true;
            item.IsActive = false;
            //item.DeletedBy = access!.UserId;
            item.DeletedOn = DateTime.UtcNow;

            await _quoteService.UpdateAsync(item);

            return Ok(new Result<QuotePostDto>(null));

        }
    }
}
