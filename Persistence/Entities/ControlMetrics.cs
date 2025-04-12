using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class ControlMetrics
    {
        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("lastupdatedat")]
        public DateTime LastUpdatedAt { get; set;}
    }
}
