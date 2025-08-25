namespace DiscountCodeGeneratorService.Application.Queries.GetDiscountCodes;

public record GetDiscountCodesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedDiscountCodesResponse>;