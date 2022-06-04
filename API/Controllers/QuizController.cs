using Application.Services.GameStats_Management.Dto;
using Application.Services.Quiz_Management;
using Application.Services.Quiz_Management.Dto;
using Application.Services.Quote_Management.Dto;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromQuery] CreateQuizDto requestDto)
        {
            return Ok(await _quizService.CreateQuiz(requestDto));
        }

        [HttpGet("{quizId:int}")]
        public async Task<IActionResult> GetQuiz(int quizId)
        {
            return Ok(await _quizService.GetQuiz(quizId));
        }

        [HttpPost("AddQuotInQuiz")]
        public async Task<IActionResult> AddQuotInQuiz(CreateQuizQuestionDto requestDto)
        {
            return Ok(await _quizService.AddQuotInQuiz(requestDto));
        }

        [HttpPost("SubmitQuiz")]
        public async Task<IActionResult> Submit(SubmitQuizDto requestDto)
        {
            return Ok(await _quizService.SubmitQuiz(requestDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizzesByGameMode(QuizGameMode gameMode = QuizGameMode.Binary)
        {
            return Ok(await _quizService.GetQuizzesByGameMode(gameMode));
        }

        [HttpPost("CheckAnswer")]
        public async Task<IActionResult> CheckAnswer(CheckAnswerDto requestDto)
        {
            return Ok(await _quizService.CheckAnswer(requestDto));
        }

        [HttpGet("CheckIfCompleted")]
        public async Task<IActionResult> CheckIfUserCompletedQuiz(string username, int quizId)
            => Ok(await _quizService.CheckIfUserCompletedQuiz(username, quizId));

    }
}
