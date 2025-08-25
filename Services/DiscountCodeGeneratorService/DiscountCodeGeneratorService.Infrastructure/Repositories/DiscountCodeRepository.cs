using DiscountCodeGeneratorService.Domain.Entities;
using DiscountCodeGeneratorService.Domain.Interfaces;
using DiscountCodeGeneratorService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiscountCodeGeneratorService.Infrastructure.Repositories;

public class DiscountCodeRepository(DiscountDbContext discountDbContext) : IDiscountCodeRepository
{
    public async Task AddDiscountCodesAsync(IEnumerable<DiscountCode> discountCodes, CancellationToken cancellationToken)
    {
        await discountDbContext.DiscountCodes.AddRangeAsync(discountCodes, cancellationToken);
        await SaveAsync(cancellationToken);
    }

    public async Task<IEnumerable<DiscountCode>> GetAllDiscountCodesAsync(CancellationToken cancellationToken)
    {
        return await discountDbContext.DiscountCodes.ToListAsync(cancellationToken);
    }

    public async Task<DiscountCode?> GetDiscountCodeByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await discountDbContext.DiscountCodes.FirstOrDefaultAsync(dc => dc.Code == code, cancellationToken);
    }

    public Task<bool> IsDiscountCodeExistsAsync(string code, CancellationToken cancellationToken)
    {
        return discountDbContext.DiscountCodes.AnyAsync(dc => dc.Code == code, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await discountDbContext.SaveChangesAsync(cancellationToken);
    }
}
