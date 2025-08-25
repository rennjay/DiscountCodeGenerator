using DiscountCodeGeneratorService.Domain.Exceptions;

namespace DiscountCodeGeneratorService.Application.Commands.UseDiscountCode;

public record UseDiscountCodeCommandHandler(IDiscountCodeRepository discountCodeRepository,
    IDiscountCodeService discountCodeService) 
    : IRequestHandler<UseDiscountCodeCommand, UseDiscountCodeResponse>
{
    public async Task<UseDiscountCodeResponse> Handle(UseDiscountCodeCommand request, CancellationToken cancellationToken)
    {
        var discountCode = await discountCodeRepository.GetDiscountCodeByCodeAsync(request.Code, cancellationToken);

        if (discountCode == null)
        {
            return new UseDiscountCodeResponse(0);
        }

        try
        {
            var result = await discountCodeService.UseCode(discountCode, cancellationToken);

            return new UseDiscountCodeResponse(result);
        }
        catch (Exception)
        {
            return new UseDiscountCodeResponse(0);
        }

    }
}
