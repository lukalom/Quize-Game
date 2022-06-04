using Infrastructure.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class BaseEntity : IEntity, IPrimaryKey
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDisabled { get; set; }
    }
}
