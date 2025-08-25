namespace DiscountCodeGeneratorService.Domain.Exceptions;

public class InvalidCodeLengthWhenGeneratingDiscountCodes : Exception
{
    public InvalidCodeLengthWhenGeneratingDiscountCodes(string codeLength)
        : base($"Code length must be between 7 and 8: CodeLength = {codeLength}")
    {
    }
}
