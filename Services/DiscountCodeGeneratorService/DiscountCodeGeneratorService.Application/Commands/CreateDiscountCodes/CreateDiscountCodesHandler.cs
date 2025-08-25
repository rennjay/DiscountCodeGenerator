namespace DiscountCodeGeneratorService.Application.Commands.CreateDiscountCodes;

public class CreateDiscountCodesHandler(IDiscountCodeService discountCodeGeneratorService,
    IDiscountCodeRepository discountCodeRepository) : IRequestHandler<CreateDiscountCodesCommand, CreateDiscountCodesResponse>
{
    public async Task<CreateDiscountCodesResponse> Handle(CreateDiscountCodesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await discountCodeGeneratorService.GenerateDiscountCodesAsync(request.Count, request.Length, cancellationToken);
            await discountCodeRepository.AddDiscountCodesAsync(result, cancellationToken);

            return new CreateDiscountCodesResponse(result.Count() > 0);
        }
        catch (Exception)
        {
            return new CreateDiscountCodesResponse(false);
        }
    }
}
