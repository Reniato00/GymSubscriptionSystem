namespace Bussines.Extensions
{
    public static class DateTimeExtension
    {
        public static string GetStatus(this DateTime? expired)
        {
            if (expired == null || DateTime.Now > expired.Value)
            {
                return "Expired";
            }

            if (DateTime.Now > expired.Value.AddDays(-5) && DateTime.Now < expired.Value)
            {
                return "ExpiringSoon";
            }

            return "Active";
        }
    }
}