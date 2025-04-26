using Bussines.Extensions;

namespace Tests.Bussines.Extensions
{
    public class DatetimeExtensionTests
    {
        [Fact]
        public void GetStatus_Expired_ReturnsExpired()
        {
            DateTime? expired = DateTime.Now.AddDays(-1);
            var status = expired.GetStatus();
            Assert.Equal("Expired", status);

        }

        [Fact]
        public void GetStatus_ExpiringSoon_ReturnsExpiringSoon()
        {
            // Arrange
            DateTime? expired = DateTime.Now.AddDays(3);
            // Act
            string status = expired.GetStatus();
            // Assert
            Assert.Equal("ExpiringSoon", status);
        }

        [Fact]
        public void GetStatus_Active_ReturnsActive()
        {
            // Arrange
            DateTime? expired = DateTime.Now.AddDays(10);

            // Act
            string status = expired.GetStatus();

            // Assert
            Assert.Equal("Active", status);
        }
    }

}