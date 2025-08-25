namespace DiscountCodeGeneratorService.Domain.Interfaces;

public interface IDiscountCodeRepository
{
    Task<bool> IsDiscountCodeExistsAsync(string code, CancellationToken cancellationToken);
    Task AddDiscountCodesAsync(IEnumerable<Entities.DiscountCode> discountCodes, CancellationToken cancellationToken);
    Task<IEnumerable<Entities.DiscountCode>> GetAllDiscountCodesAsync(CancellationToken cancellationToken);
    Task<Entities.DiscountCode?> GetDiscountCodeByCodeAsync(string code, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
