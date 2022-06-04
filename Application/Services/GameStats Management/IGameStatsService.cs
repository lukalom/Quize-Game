using Application.Services.GameStats_Management.Dto;

namespace Application.Services.GameStats_Management
{
    public interface IGameStatsService
    {
        Task<GetUserGeneralStatsDto> GetUserGeneralStats(string userName);
        Task<List<GetUserQuizStats>> GetUserQuizStats(string userName, int quizId);
    }
}
