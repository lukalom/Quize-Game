using System.ComponentModel.DataAnnotations;

namespace Application.Services.Quote_Management.Dto
{
    public class CheckAnswerDto
    {

        [Required]
        public int QuizId { get; set; }

        [Required]
        public int QuoteId { get; set; } //Question is quote

        [Required]
        public string Author { get; set; } // Answer is Author

        [Required]
        public bool IsCorrect { get; set; }

    }
}
