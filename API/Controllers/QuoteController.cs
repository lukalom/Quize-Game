using Application.Services.Quote_Management;
using Application.Services.Quote_Management.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _quoteService.GetQuoteById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilterQuotParameters filterQuery)
        {
            var result = await _quoteService.GetQuotes(filterQuery);

            if (result.IsSuccess) return Ok(result);

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuote(int id)
            => Ok(await _quoteService.DeleteQuote(id));


        [HttpPost]
        public async Task<IActionResult> CreateQuote(CreateQuoteDto requestDto)
            => Ok(await _quoteService.CreateQuote(requestDto));
    }
}
