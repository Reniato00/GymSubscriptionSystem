﻿namespace Persistence.Entities
{
    public class Customer : ControlMetrics
    {
        public string? Id { get; set; }
        public required string Name { get; set; }

        public bool? Gender { get; set; }

        public required DateTime SubscriptionExpiresAt { get; set; } // Datetime of when going to expires his subscription
    }
}
