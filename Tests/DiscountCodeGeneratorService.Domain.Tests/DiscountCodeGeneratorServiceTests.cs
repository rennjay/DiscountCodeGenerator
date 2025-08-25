using DiscountCodeGeneratorService.Domain.Exceptions;
using DiscountCodeGeneratorService.Domain.Interfaces;
using Moq;
namespace DiscountCodeGeneratorService.Domain.Tests;
public class DiscountCodeGeneratorServiceTests
{
    private Services.DiscountCodeService GetServiceUnderTest()
    {
        var mockRepo = new Mock<IDiscountCodeRepository>();
         mockRepo.Setup(r => r.IsDiscountCodeExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        return new Services.DiscountCodeService(mockRepo.Object);
    }

    [Fact]
    public async Task GenerateCodesAsync_WhenCountIsInvalid_ThrowsArgumentException()
    {
        var discountCodeGeneratorService = GetServiceUnderTest();

        await Assert.ThrowsAsync<InvalidCountWhenGeneratingDiscountCodes>(() => discountCodeGeneratorService.GenerateDiscountCodesAsync(0, 7, CancellationToken.None));
    }

    [Fact]
    public async Task GenerateCodesAsync_WhenCodeLengthIsInvalid_ThrowsArgumentException()
    {
        var discountCodeGeneratorService = GetServiceUnderTest();

        await Assert.ThrowsAsync<InvalidCodeLengthWhenGeneratingDiscountCodes>(() => discountCodeGeneratorService.GenerateDiscountCodesAsync(100, 0, CancellationToken.None));
    }

    [Theory]
    [InlineData(1_000, 7)]
    [InlineData(2_000, 8)]
    public async Task GenerateCodesAsync_WhenCalledWithValidParameters_ReturnsCorrectNumberOfCodes(ushort count, byte codeLength)
    {
        var discountCodeGeneratorService = GetServiceUnderTest();
        var result = await discountCodeGeneratorService.GenerateDiscountCodesAsync(count, codeLength, new CancellationToken());
        Assert.Equal(count, result.Count());
        Assert.Equal(result.Count(), result.Select(dc => dc.Code).Distinct().Count());
    }
}