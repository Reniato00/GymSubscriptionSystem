namespace Persistence.Entities
{
    public class Plans : ControlMetrics
    {
        public string? Id { get; set; }
        public required string Name { get; set; } // lIke Anual, Monthly,
        public required int Duration { get; set; } // Duration in Days
    }
}
