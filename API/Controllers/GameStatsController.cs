using Application.Services.GameStats_Management;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {
        private readonly IGameStatsService _gameStatsService;

        public GameStatsController(IGameStatsService gameStatsService)
        {
            _gameStatsService = gameStatsService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> UserGeneralGameRating(string userName)
        {
            return Ok(await _gameStatsService.GetUserGeneralStats(userName));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserQuizStats(string userName, int quizId)
        {
            return Ok(await _gameStatsService.GetUserQuizStats(userName, quizId));
        }
    }
}
