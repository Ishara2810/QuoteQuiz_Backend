using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteQuiz_API.Dtos.Quiz;
using QuoteQuiz_API.Dtos.Quote;
using QuoteQuiz_API.Dtos.Result;
using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Interfaces.IServices;
using System.Security.Claims;

namespace QuoteQuiz_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            _quizService = quizService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetNext()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var que = await _quizService.GetNextQuestionAsync(userId);
            var itemDto = _mapper.Map<QuizQuestionDto>(que);
            return Ok(new Result<QuizQuestionDto>(itemDto));
        }

        [HttpPost("submit-answer")]
        public async Task<IActionResult> SubmitAnswer(SubmitAnswerDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var answerEntity = _mapper.Map<QuizAnswerSubmitEntity>(dto);
            var result = await _quizService.SubmitAnswerAsync(answerEntity);
            var resultDto = _mapper.Map<QuizAttemptDto>(result);

            return Ok(new Result<QuizAttemptDto>(resultDto));
        }
    }
}
