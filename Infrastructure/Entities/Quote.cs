using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Quote : BaseEntity
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }


    }
}
