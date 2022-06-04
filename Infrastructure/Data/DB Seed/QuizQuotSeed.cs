using Infrastructure.Entities;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DB_Seed
{
    public class QuizQuotSeed : ISeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizQuotSeed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Index { get; set; } = 4;

        public async Task Seed()
        {
            if (await _unitOfWork.QuizQuot.Query().CountAsync() == 0)
            {
                var quizQuotes = new List<QuizQuot>()
                {
                    new QuizQuot(){QuizId = 1, QuoteId = 5},
                    new QuizQuot(){QuizId = 1, QuoteId = 2},
                    new QuizQuot(){QuizId = 1, QuoteId = 1},
                    new QuizQuot(){QuizId = 1, QuoteId = 4},

                    new QuizQuot(){QuizId = 2, QuoteId = 3},
                    new QuizQuot(){QuizId = 2, QuoteId = 5},
                    new QuizQuot(){QuizId = 2, QuoteId = 1},


                    new QuizQuot(){QuizId = 3, QuoteId = 3},
                    new QuizQuot(){QuizId = 3, QuoteId = 5},
                    new QuizQuot(){QuizId = 3, QuoteId = 1},
                    new QuizQuot(){QuizId = 3, QuoteId = 2},
                    new QuizQuot(){QuizId = 3, QuoteId = 4},


                    new QuizQuot(){QuizId = 4, QuoteId = 2},
                    new QuizQuot(){QuizId = 4, QuoteId = 4},
                    new QuizQuot(){QuizId = 4, QuoteId = 1},

                    new QuizQuot(){QuizId = 5, QuoteId = 1},
                    new QuizQuot(){QuizId = 5, QuoteId = 3},
                    new QuizQuot(){QuizId = 5, QuoteId = 2},
                    new QuizQuot(){QuizId = 5, QuoteId = 5}

                };

                //foreach (var quizQuote in quizQuotes)
                //{
                await _unitOfWork.QuizQuot.AddRangeAsync(quizQuotes);
                await _unitOfWork.SaveAsync();
                //}

                //await _unitOfWork.QuizQuot.AddRangeAsync(quizQuotes);
                //await _unitOfWork.SaveAsync();
            }
        }
    }
}
