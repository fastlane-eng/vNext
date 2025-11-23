using FastLane.Core.Models;
using FastLane.Core.Interfaces;
using FastLane.Data.Repositories;
using FastLane.Services;
using FastLane.Api.Middleware;
using FastLane.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Register repositories and services
builder.Services.AddSingleton<InventoryRepository>();
builder.Services.AddSingleton<ForecastRepository>();
builder.Services.AddSingleton<PurchaseOrderRepository>();

builder.Services.AddSingleton<IInventoryService, InventoryService>();
builder.Services.AddSingleton<IForecastService, ForecastService>();
builder.Services.AddSingleton<IPurchaseOrderEngine, PurchaseOrderEngine>();

// API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Authentication and authorization (stub)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Placeholder authority and audience; real values to be configured
        options.Authority = "https://example.com";
        options.Audience = "fastlane_api";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use global error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/inventory", async (IInventoryService svc) =>
{
    var skus = await svc.GetAllSkusAsync();
    return Results.Ok(skus);
});

app.MapGet("/inventory/{id:int}", async (int id, IInventoryService svc) =>
{
    var sku = await svc.GetSkuByIdAsync(id);
    return sku != null ? Results.Ok(sku) : Results.NotFound();
});

app.MapPut("/inventory/{id:int}", async (int id, InventoryUpdateRequest request, IInventoryService svc) =>
{
    await svc.UpdateInventoryAsync(id, request.QuantityDelta);
    return Results.NoContent();
});

app.MapGet("/forecast/{skuId:int}", async (int skuId, IForecastService svc) =>
{
    var forecasts = await svc.GetForecastsForSkuAsync(skuId);
    return Results.Ok(forecasts);
});

app.MapPost("/forecast", async (Forecast forecast, IForecastService svc) =>
{
    await svc.CreateForecastAsync(forecast);
    return Results.Created($"/forecast/{forecast.SkuId}", forecast);
});

app.MapGet("/purchase-orders", async (IPurchaseOrderEngine engine) =>
{
    var orders = await engine.GetPurchaseOrdersAsync();
    return Results.Ok(orders);
});

app.MapGet("/purchase-orders/{id:int}", async (int id, IPurchaseOrderEngine engine) =>
{
    var po = await engine.GetPurchaseOrderByIdAsync(id);
    return po != null ? Results.Ok(po) : Results.NotFound();
});

app.MapPost("/purchase-orders", async (PurchaseOrder purchaseOrder, IPurchaseOrderEngine engine) =>
{
    var result = await engine.CreatePurchaseOrderAsync(purchaseOrder);
    return Results.Created($"/purchase-orders/{result.Id}", result);
});

app.Run();

// Request type for updating inventory
public record InventoryUpdateRequest(int QuantityDelta);
