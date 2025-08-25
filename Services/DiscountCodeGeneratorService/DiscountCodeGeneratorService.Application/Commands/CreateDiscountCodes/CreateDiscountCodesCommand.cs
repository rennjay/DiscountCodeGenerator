namespace DiscountCodeGeneratorService.Application.Commands.CreateDiscountCodes;

public record CreateDiscountCodesCommand(ushort Count, byte Length) : IRequest<CreateDiscountCodesResponse>;
