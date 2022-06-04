using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
    }
}
