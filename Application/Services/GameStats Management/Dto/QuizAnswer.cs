using System.ComponentModel.DataAnnotations;

namespace Application.Services.GameStats_Management.Dto
{
    public class QuizAnswer
    {
        [Required]
        public int QuoteId { get; set; } //Question is quote

        [Required]
        public string Author { get; set; } // Answer is Author

        [Required]
        public bool IsCorrect { get; set; }
    }
}
