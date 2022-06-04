using Infrastructure.Entities;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DB_Seed
{
    public class QuoteSeed : ISeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuoteSeed(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Index { get; set; } = 2;

        public async Task Seed()
        {
            if (await _unitOfWork.Quote.Query().CountAsync() == 0)
            {
                var quotes = new List<Quote>()
                {
                    new() {
                        Author = "Mahatma Gandhi",
                        Description = "Live as if you were to die tomorrow. Learn as if you were to live forever."
                    },
                    new() {
                        Author = "Friedrich Nietzsche",
                        Description = "hat which does not kill us makes us stronger."
                    },
                    new() {
                        Author = "Confucius",
                        Description = "Everything has beauty, but not everyone can see."
                    },
                    new() {
                        Author = "Norman Vincent",
                        Description = "Change your thoughts and you change your world."
                    },
                    new() {
                        Author = "Muhammad Ali",
                        Description = "I'm going to show you how great I am."
                    },
                };
                await _unitOfWork.Quote.AddRangeAsync(quotes);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
