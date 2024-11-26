using Application;
using Application.Extensions;
using Domain;
using GrpcServices;
using Infrastructure;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDomainServices();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices(builder.Configuration);


builder.Services.AddGrpc();
var app = builder.Build();

app.MapGet("/", () => "Server Start!");

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<UserGrpcService>();
});
app.Run();
