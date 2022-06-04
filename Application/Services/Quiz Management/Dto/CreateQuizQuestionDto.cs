using System.ComponentModel.DataAnnotations;

namespace Application.Services.Quiz_Management.Dto
{
    public class CreateQuizQuestionDto
    {
        public CreateQuizQuestionDto()
        {
            QuotesIdList = new List<int>();
        }

        [Required]
        public List<int> QuotesIdList { get; set; }

        public int QuizId { get; set; }
    }
}
