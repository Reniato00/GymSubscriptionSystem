namespace GymSubscriptionSystem.Models
{
    public class CreateCustomerViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = "none"; // "male", "female", "none"

        public int SubscriptionPlanMonths { get; set; } // 1, 2, 3, 12
    }
}
