using DiscountCodeGeneratorService.Application.Commands.CreateDiscountCodes;
using DiscountCodeGeneratorService.Application.Commands.UseDiscountCode;
using DiscountCodeGeneratorService.Application.Queries.GetDiscountCodes;
using Grpc.Core;
using MediatR;

namespace DiscountCodeGeneratorService.Grpc.Services;

public class DiscountCodeGeneratorService(IMediator mediator) : DiscountCodeGeneratorProtoService.DiscountCodeGeneratorProtoServiceBase
{
    public override async Task<GenerateDiscountCodeResponse> GenerateDiscountCodes(
        GenerateDiscountCodeRequest request,
        ServerCallContext context)
    {
        var command = new CreateDiscountCodesCommand((ushort)request.Count, (byte)request.Length);
        var response = await mediator.Send(command, context.CancellationToken);

        return new GenerateDiscountCodeResponse
        {
            Result = response.Result
        };
    }

    public override async Task<PaginatedDiscountCodesResponse> GetDiscountCodes(
        GetDiscountCodesRequest request,
        ServerCallContext context)
    {
        var query = new GetDiscountCodesQuery((int)request.PageNumber, (int)request.PageSize);
        var response = await mediator.Send(query, context.CancellationToken);
        return new PaginatedDiscountCodesResponse
        {
            Items = { response.Items.Select(item => new DiscountCode
            {
                Code = item.Code,
                ExpirationTime = item.ExpirationTime.HasValue
                    ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.ExpirationTime.Value.ToUniversalTime())
                    : null,
                UsageInfo = new DiscountUsageInfo
                {
                    IsUsed = item.UsageInfo.IsUsed,
                    UsedTime = item.UsageInfo.UsedTime.HasValue
                        ? Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.UsageInfo.UsedTime.Value.ToUniversalTime())
                        : null
                }
            })},
            TotalCount = response.TotalCount,
            PageNumber = response.PageNumber,
            PageSize = response.PageSize,
            TotalPages = response.TotalPages
        };
    }

    public override async Task<UseDiscountCodeResponse> UseDiscountCode(UseDiscountCodeRequest request, ServerCallContext context)
    {
        var command = new UseDiscountCodeCommand(request.Code);
        var response = await mediator.Send(command, context.CancellationToken);
        return new UseDiscountCodeResponse
        {
            Result = response.Result
        };
    }
}