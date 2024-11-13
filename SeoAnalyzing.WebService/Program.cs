using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Infrastructure.Core.Clients;
using SeoAnalyzing.Infrastructure.Core.Services;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.WebService.Extensions.HttpClient;
using SeoAnalyzing.WebService.Extensions.Swagger;
using SeoAnalyzing.WebService.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // Add the filter globally
    options.Filters.Add<ValidateModelAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerDoc();
builder.Services.RegisterHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddCors(o => o.AddPolicy(CommonConstants.SeoAnalyzingCors, builder =>
{
    builder.SetIsOriginAllowed(origin => true)
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Register service
builder.Services.AddScoped<IGoogleSearchClient, GoogleSearchClient>();
builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();
builder.Services.AddScoped<IBingSearchClient, BingSearchClient>();
builder.Services.AddScoped<IBingSearchService, BingSearchService>();
builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerDoc();

app.UseCors(CommonConstants.SeoAnalyzingCors);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
