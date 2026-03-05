using Moq;
using TestApp.Models;
using TestApp.Repositories;
using Xunit;

public class ProductServiceTests
{
    [Fact]
    public void GetProductName_ShouldReturnProductName()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();

        mockRepo.Setup(repo => repo.GetProductById(1))
                .Returns(new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 80000
                });

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = service.GetProductName(1);

        // Assert
        Assert.Equal("Laptop", result);
    }
}