using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class Customer : ControlMetrics
    {
        [Column("id")]
        public string? Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("gender")]
        public bool? Gender { get; set; }

        [Column("subscriptionexpiresat")]
        public required DateTime SubscriptionExpiresAt { get; set; } // Datetime of when going to expires his subscription

        [Column("nameuser")]
        public string? NameUser { get; set; }
    }
}
