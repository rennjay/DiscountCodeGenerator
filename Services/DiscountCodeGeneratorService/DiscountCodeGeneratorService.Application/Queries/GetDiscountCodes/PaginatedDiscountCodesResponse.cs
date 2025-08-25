namespace DiscountCodeGeneratorService.Application.Queries.GetDiscountCodes;

public record PaginatedDiscountCodesResponse(
    IEnumerable<DiscountCodeDto> Items,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages
);