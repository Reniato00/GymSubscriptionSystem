namespace GymSubscriptionSystem.Persistence
{
    public class CustomerDto
    {
        public DateTime? Expired { get; set; }

        public required string Name { get; set; }
    }
}