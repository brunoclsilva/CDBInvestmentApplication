using Investiment.Domain.Entities;
using Investiment.Domain.Interfaces.Services;
using Investment.Service.Services;
using Moq;

namespace Investment.Tests
{
    public class CdbServiceTests
    {
        private readonly Mock<IIndexService> _indexServiceMock;
        public CdbServiceTests()
        {
            _indexServiceMock = new Mock<IIndexService>();
            _indexServiceMock.Setup(s => s.GetTb()).ReturnsAsync(1.08m); // TB
            _indexServiceMock.Setup(s => s.GetCdi()).ReturnsAsync(0.009m); // CDI
        }

        [Fact]
        public async Task CalculateInvestment_InvalidInitialValue_ThrowsArgumentException()
        {
            // Arrange
            var request = new InvestmentRequest
            {
                InitialValue = 0m,
                Months = 12
            };

            var indexServiceMock = new Mock<IIndexService>();
            var cdbService = new CdbService(indexServiceMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await cdbService.CalculateInvestment(request);
            });
        }

        [Fact]
        public async Task CalculateInvestment_InvalidMonths_ThrowsArgumentException()
        {
            // Arrange
            var request = new InvestmentRequest
            {
                InitialValue = 1000m,
                Months = 0
            };

            var indexServiceMock = new Mock<IIndexService>();
            var cdbService = new CdbService(indexServiceMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await cdbService.CalculateInvestment(request);
            });
        }

        [Theory]
        [InlineData(6, 61.68, 47.8)]
        [InlineData(12, 145.56, 116.45)]
        [InlineData(24, 519.54, 428.62)]
        [InlineData(36, 1708.87, 1452.54)]
        public async Task CalculateInvestment_ShouldCalculateGrossAndNetResult(int months, decimal expectedGrossResult, decimal expectedNetResult)
        {
            // Arrange
            var cdbService = new CdbService(_indexServiceMock.Object);
            var request = new InvestmentRequest
            {
                InitialValue = 1000,
                Months = months,
            };

            // Act
            var result = await cdbService.CalculateInvestment(request);

            // Assert
            Assert.Equal(expectedGrossResult, result.GrossResult);
            Assert.Equal(expectedNetResult, result.NetResult);
        }
    }
}