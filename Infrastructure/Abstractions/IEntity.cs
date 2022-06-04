namespace Infrastructure.Abstractions
{
    public interface IEntity
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDisabled { get; set; }
    }
}
