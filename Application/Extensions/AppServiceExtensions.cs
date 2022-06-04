using Application.Services.GameStats_Management;
using Application.Services.Quiz_Management;
using Application.Services.Quote_Management;
using Application.Services.User_Management;
using Infrastructure.Data;
using Infrastructure.IConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection ApplicationServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IGameStatsService, GameStatsService>();

            return services;
        }
    }
}
