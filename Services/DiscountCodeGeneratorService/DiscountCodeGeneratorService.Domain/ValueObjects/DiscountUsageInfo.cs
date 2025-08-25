namespace DiscountCodeGeneratorService.Domain.ValueObjects;

public record DiscountUsageInfo(
    bool IsUsed,
    DateTime? UsedTime
);