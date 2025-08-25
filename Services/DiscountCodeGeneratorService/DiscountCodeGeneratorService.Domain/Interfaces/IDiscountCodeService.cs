using DiscountCodeGeneratorService.Domain.Entities;

namespace DiscountCodeGeneratorService.Domain.Interfaces;

public interface IDiscountCodeService
{
    Task<IEnumerable<DiscountCode>> GenerateDiscountCodesAsync(ushort count, byte codeLength, CancellationToken cancellationToken);
    Task<byte> UseCode(DiscountCode code, CancellationToken cancellationToken);
}
