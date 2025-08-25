
namespace DiscountCodeGeneratorService.Application.Queries.GetDiscountCodes;

public class GetDiscountCodesQueryHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<GetDiscountCodesQuery, PaginatedDiscountCodesResponse>
{
    public async Task<PaginatedDiscountCodesResponse> Handle(GetDiscountCodesQuery request, CancellationToken cancellationToken)
    {
        var discountCodes = await discountCodeRepository
            .GetAllDiscountCodesAsync(cancellationToken);

        var totalCount = discountCodes.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var items = discountCodes
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(dc => new DiscountCodeDto(
                dc.Code,
                dc.ExpirationTime,
                dc.UsageInfo
            ));

        return new PaginatedDiscountCodesResponse(
            Items: items,
            TotalCount: totalCount,
            PageNumber: request.PageNumber,
            PageSize: request.PageSize,
            TotalPages: totalPages
        );
    }
}
