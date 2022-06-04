using System.ComponentModel.DataAnnotations;

namespace Application.Services.Quote_Management.Dto
{
    public class CreateQuoteDto
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
