using Application.Services.Quote_Management.Dto;
using Infrastructure.Entities;
using Shared.Dto;

namespace Application.Services.Quote_Management
{
    public interface IQuoteService
    {
        Task<PagedResult<Quote>> GetQuotes(FilterQuotParameters filterQuery);
        Task<Quote> GetQuoteById(int id);
        Task<string> CreateQuote(CreateQuoteDto requestDto);
        Task<bool> DeleteQuote(int id);
    }
}
