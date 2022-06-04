using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Quiz : BaseEntity
    {
        [Required]
        public QuizGameMode GameMode { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
