using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
public class EmployeeServiceTests
{
    [Test]
    public void GetEmployeeCount_ReturnsCorrectCount()
    {
        var mockRepo = new Mock();
        mockRepo.Setup(r => r.GetAll()).Returns(new List
        {
            new Employee{Id=1,Name="Ravi"},
            new Employee{Id=2,Name="Anu"}
        });

        var service = new EmployeeService(mockRepo.Object);

        int count = service.GetEmployeeCount();

        Assert.AreEqual(2, count);
    }
}