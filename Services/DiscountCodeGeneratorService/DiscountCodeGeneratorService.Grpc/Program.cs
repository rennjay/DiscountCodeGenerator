using DiscountCodeGeneratorService.Application.Commands.CreateDiscountCodes;
using Services = DiscountCodeGeneratorService.Grpc.Services;
using DiscountCodeGeneratorService.Infrastructure;
using DiscountCodeGeneratorService.Domain;
using DiscountCodeGeneratorService.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var targetAssembly = typeof(CreateDiscountCodesCommand).Assembly;
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(targetAssembly);

});
builder.Services.AddGrpc();
var app = builder.Build();

app.MapGrpcService<Services.DiscountCodeGeneratorService >();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
