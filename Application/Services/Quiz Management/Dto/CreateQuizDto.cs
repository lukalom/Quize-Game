using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Quiz_Management.Dto
{
    public class CreateQuizDto
    {

        public QuizGameMode Mode { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
