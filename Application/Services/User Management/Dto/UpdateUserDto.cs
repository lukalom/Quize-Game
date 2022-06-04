using System.ComponentModel.DataAnnotations;

namespace Application.Services.User_Management.DTO
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }
}
