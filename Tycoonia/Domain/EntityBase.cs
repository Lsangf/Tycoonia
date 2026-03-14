namespace Tycoonia.Domain
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public List<string> Type { get; set; } = new();
    }
}
