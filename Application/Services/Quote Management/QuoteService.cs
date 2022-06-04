using Application.Extensions;
using Application.Services.Quote_Management.Dto;
using Infrastructure.Entities;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Exceptions;

namespace Application.Services.Quote_Management
{
    public class QuoteService : IQuoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Quote>> GetQuotes(FilterQuotParameters filterQuery)
        {
            var result = new PagedResult<Quote>();
            var query = _unitOfWork.Quote.Query();

            if (!string.IsNullOrEmpty(filterQuery.Author))
            {
                var filteredQuotes = await query
                    .Where(u => u.Author.Contains(filterQuery.Author.ToLowerInvariant()))
                    .PaginateAsync(filterQuery.Page, filterQuery.PageSize);

                result = filteredQuotes;
                return result;
            }

            return await query.PaginateAsync(filterQuery.Page, filterQuery.PageSize);
        }

        public async Task<Quote> GetQuoteById(int id)
        {
            if (id <= 0)
            {
                throw new AppException("Invalid Id");
            }

            var quote = await _unitOfWork.Quote.Find(id);
            if (quote == null) throw new AppException($"Quote with this id does not exist = {id}");
            return quote;
        }

        public async Task<string> CreateQuote(CreateQuoteDto requestDto)
        {

            var quote = await _unitOfWork.Quote
                .Query(x => x.Description.ToLower() == requestDto.Description.ToLower()).FirstOrDefaultAsync();

            if (quote != null)
            {
                throw new AppException("Quote already exists");
            }

            await _unitOfWork.Quote.AddAsync(new Quote()
            {
                Author = requestDto.Author,
                Description = requestDto.Description
            });

            await _unitOfWork.SaveAsync();

            return "quote create successfully";
        }

        public async Task<bool> DeleteQuote(int id)
        {
            var quote = await _unitOfWork.Quote.Find(id);

            if (quote == null)
            {
                throw new AppException("Invalid quote");
            }

            quote.IsDisabled = true;
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
