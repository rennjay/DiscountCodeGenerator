namespace DiscountCodeGeneratorService.Application.Commands.UseDiscountCode;

public record UseDiscountCodeCommand(string Code) : IRequest<UseDiscountCodeResponse>;