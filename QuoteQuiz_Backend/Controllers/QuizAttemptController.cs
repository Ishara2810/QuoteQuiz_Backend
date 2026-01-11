using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quiz;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_Domain.Interfaces.IServices;

namespace QuoteQuiz_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAttemptController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuizAttemptService _quizAttemptService;
        public QuizAttemptController(IMapper mapper, IQuizAttemptService quizAttemptService)
        {
            _mapper = mapper;
            _quizAttemptService = quizAttemptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _quizAttemptService.GetAllAsync();
            var itemsDto = _mapper.Map<List<QuizAttemptDto>>(items);
            return Ok(new Result<List<QuizAttemptDto>>(itemsDto));
        }
    }
}
