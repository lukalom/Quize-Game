using Application.Services.GameStats_Management.Dto;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace Application.Services.GameStats_Management
{
    public class GameStatsService : IGameStatsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameStatsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<GetUserGeneralStatsDto> GetUserGeneralStats(string userName)
        {
            var user = await _unitOfWork.User.Query(x =>
                    x.UserName.ToLower() == userName.ToLower())
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new AppException("User Does not Exists");
            }

            var gameStat = await _unitOfWork.GameStats
                .Query(x => x.UserId == user.Id).Include(x => x.Quiz)
                .ToListAsync();

            if (gameStat == null || !gameStat.Any())
            {
                throw new AppException("You don't have taken any quiz");
            }

            var answers = gameStat.Count(x => x.IsCorrect);
            var totalQuestions = gameStat.Count;
            var answersRate = (double)answers / totalQuestions * 100;
            var response = new GetUserGeneralStatsDto()
            {
                Answers = answers,
                AnswerRate = $"{(int)answersRate}%",
                QuizzesTaken = gameStat.Select(x => x.Quiz!.Title).Distinct().ToList(),
                TotalQuestions = totalQuestions
            };

            return response;
        }

        public async Task<List<GetUserQuizStats>> GetUserQuizStats(string userName, int quizId)
        {
            var user = await _unitOfWork.User.Query(x =>
                    x.UserName.ToLower() == userName.ToLower() && x.IsDisabled == false)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new AppException("Invalid User");
            }

            var gameStats = await _unitOfWork.GameStats
                .Query(x => x.UserId == user.Id
                                      && x.QuizId == quizId)
                .Include(x => x.Quiz)
                .ToListAsync();

            if (gameStats == null || !gameStats.Any())
            {
                throw new AppException($"You don't have taken any quiz with this id = {quizId}");
            }

            var response = new List<GetUserQuizStats>() { };

            foreach (var quizQuestion in gameStats)
            {
                var question = await _unitOfWork.Quote
                    .Query(x =>
                        x.Id == quizQuestion.QuotId)
                    .FirstOrDefaultAsync();

                response.Add(new GetUserQuizStats()
                {
                    IsCorrect = quizQuestion.IsCorrect,
                    Answered = quizQuestion.Answer,
                    Question = question.Description
                });
            }

            return response;
        }
    }
}
