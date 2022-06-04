using Application.Services.GameStats_Management.Dto;
using Application.Services.Quiz_Management.Dto;
using Application.Services.Quote_Management.Dto;
using Infrastructure.Enums;

namespace Application.Services.Quiz_Management
{
    public interface IQuizService
    {
        Task<string> CreateQuiz(CreateQuizDto requestDto);
        Task<GetQuizDto> GetQuiz(int quizId);
        Task<string> AddQuotInQuiz(CreateQuizQuestionDto requestDto);
        Task<List<GetQuizzesByGameModeDto>> GetQuizzesByGameMode(QuizGameMode mode);
        Task<bool> CheckAnswer(CheckAnswerDto requestDto);
        Task<string> SubmitQuiz(SubmitQuizDto requestDto);
        Task<bool> CheckIfUserCompletedQuiz(string username, int quizId);
    }
}
