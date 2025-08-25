using DiscountCodeGeneratorService.Domain.Exceptions;
using DiscountCodeGeneratorService.Domain.ValueObjects;

namespace DiscountCodeGeneratorService.Domain.Entities;

public class DiscountCode
{
    public Guid Id { get; }
    public string Code { get; }
    public DateTime? ExpirationTime { get; }
    public DiscountUsageInfo UsageInfo { get; private set; }

    private DiscountCode() { }
    public DiscountCode(string code, DateTime? expirationTime = null, bool isUsed = false, DateTime? usedTime = null)
    {
        Id = Guid.NewGuid();
        Code = code;
        ExpirationTime = expirationTime;
        UsageInfo = new DiscountUsageInfo(isUsed, usedTime);
    }

    internal void MarkAsUsed(DateTime usedTime)
    {
        if (UsageInfo.IsUsed)
        {
            throw new DiscountCodeAlreadyExistsException(Code);
        }

        UsageInfo = UsageInfo with { IsUsed = true, UsedTime = usedTime };
    }
}
