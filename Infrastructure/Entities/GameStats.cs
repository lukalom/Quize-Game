using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class GameStats : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz? Quiz { get; set; }

        [Required]
        public int QuotId { get; set; }
        [ForeignKey(nameof(QuotId))]
        public Quote? Quote { get; set; }

        public string Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
