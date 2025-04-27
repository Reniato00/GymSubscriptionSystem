using AutoFixture;
using Bussines.Decorators;
using Bussines.Services;
using Moq;
using Persistence.Entities;
using Persistence.Repositories;

namespace Tests.Bussines.Decorators
{
    public class CustomerServiceTests
    {
        private readonly CustomerService customerService;
        private readonly Mock<IMiAppRepository> repositoryMock = new();
        private readonly Mock<ILoggerDecorator<CustomerService>> loggerMock  = new();
        private readonly Fixture fixture = new();

        public CustomerServiceTests()
        {
            customerService = new CustomerService(repositoryMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task GetCustomersAsync_MaleCase()
        {
            //Arrange
            string name = "Juan";
            string gender = "male";
            int months = 12;
            //Act
            await customerService.CreateCustomer(name,gender, months);
            //Assert
            repositoryMock.Verify(x => x.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomersAsync_FemaleCase()
        {
            //Arrange
            string name = "Juan";
            string gender = "female";
            int months = 12;
            //Act
            await customerService.CreateCustomer(name,gender, months);
            //Assert
            repositoryMock.Verify(x => x.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomersAsync_EmptyCase()
        {
            //Arrange
            string name = "Juan";
            string gender = "";
            int months = 12;
            //Act
            await customerService.CreateCustomer(name,gender, months);
            //Assert
            repositoryMock.Verify(x => x.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_Test()
        {
            // Arrange
            var customers = fixture.CreateMany<Customer>();
            repositoryMock.Setup(x => x.GetAllCustomersAsync()).ReturnsAsync(customers.ToList());

            // Act
            var result = await customerService.GetAll();

            // Assert
            Assert.Equal(customers, result);
        }
    }

}