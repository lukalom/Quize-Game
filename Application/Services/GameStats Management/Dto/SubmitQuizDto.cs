using System.ComponentModel.DataAnnotations;

namespace Application.Services.GameStats_Management.Dto
{
    public class SubmitQuizDto
    {
        public SubmitQuizDto() => QuizAnswers = new List<QuizAnswer>();


        [Required]
        public string UserName { get; set; }

        [Required]
        public int QuizId { get; set; }

        public List<QuizAnswer> QuizAnswers { get; set; }
    }
}
