using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DB_Seed
{
    public class QuizzesSeed : ISeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizzesSeed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Index { get; set; } = 3;

        public async Task Seed()
        {
            if (await _unitOfWork.Quiz.Query().CountAsync() == 0)
            {
                var quizzes = new List<Quiz>()
                {
                    new()
                    {
                        Title = "Binary Quiz 1",
                        GameMode = QuizGameMode.Binary
                    },
                    new()
                    {
                        Title = "Binary Quiz 2",
                        GameMode = QuizGameMode.Binary
                    },new()
                    {
                        Title = "Binary Quiz 3",
                        GameMode = QuizGameMode.Binary
                    },new()
                    {
                        Title = "Multiple Choice Quiz 1",
                        GameMode = QuizGameMode.MultipleChoice
                    },
                    new()
                    {
                        Title = "Multiple Choice Quiz 2",
                        GameMode = QuizGameMode.MultipleChoice
                    }

                };
                await _unitOfWork.Quiz.AddRangeAsync(quizzes);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
