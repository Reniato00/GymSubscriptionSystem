using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class Instructor : ControlMetrics
    {
        [Column("id")]
        public string? Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("password")]
        public required string Password { get; set; }
    }
}
