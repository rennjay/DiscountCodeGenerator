namespace DiscountCodeGeneratorService.Application.Queries.GetDiscountCodes;

public record DiscountCodeDto(
    string Code,
    DateTime? ExpirationTime,
    DiscountUsageInfo UsageInfo
    );
