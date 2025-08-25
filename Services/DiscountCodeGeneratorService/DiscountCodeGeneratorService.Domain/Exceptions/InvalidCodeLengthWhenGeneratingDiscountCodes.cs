namespace DiscountCodeGeneratorService.Domain.Exceptions;

public class InvalidCountWhenGeneratingDiscountCodes : Exception
{
    public InvalidCountWhenGeneratingDiscountCodes(string count)
        : base($"Count must be between 1 and 2000: Count = {count}")
    {
    }
}
