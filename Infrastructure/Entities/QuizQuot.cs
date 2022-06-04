using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class QuizQuot : BaseEntity
    {
        [Required]
        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz? Quiz { get; set; }

        [Required]
        public int QuoteId { get; set; }
        [ForeignKey(nameof(QuoteId))]
        public Quote? Quote { get; set; }
    }
}
