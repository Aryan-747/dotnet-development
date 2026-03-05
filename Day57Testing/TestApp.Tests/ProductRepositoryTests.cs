using ConsoleApp.Models;
using ConsoleApp.Repositories;
using Xunit;

namespace ConsoleApp.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void Add_Product_ShouldIncreaseCount()
        {
            // Arrange
            var repo = new ProductRepository();
            var product = new Product { Id = 1, Name = "Laptop", Price = 80000 };

            // Act
            repo.Add(product);
            var result = repo.GetAll();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectProduct()
        {
            // Arrange
            var repo = new ProductRepository();
            repo.Add(new Product { Id = 1, Name = "Laptop", Price = 80000 });

            // Act
            var product = repo.GetById(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Laptop", product.Name);
        }
    }
}