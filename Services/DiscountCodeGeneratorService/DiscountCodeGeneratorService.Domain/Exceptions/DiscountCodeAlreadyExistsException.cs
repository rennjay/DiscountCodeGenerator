namespace DiscountCodeGeneratorService.Domain.Exceptions;

public class DiscountCodeAlreadyExistsException : Exception
{
    public DiscountCodeAlreadyExistsException(string code)
        : base($"{code} had already been used")
    {

    }
}
