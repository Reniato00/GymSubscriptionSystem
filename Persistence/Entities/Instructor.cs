namespace Persistence.Entities
{
    public class Instructor : ControlMetrics
    {
        public string? Id { get; set; }
        public required string Name { get; set; }
    }
}
