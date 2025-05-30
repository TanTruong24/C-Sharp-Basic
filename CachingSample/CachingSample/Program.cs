using CachingSample.Data;
using CachingSample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL + EF
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dotnetSeries")));

// DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();

var useInMemoryCache = builder.Configuration.GetValue<bool>("UseInMemoryCache");

if (useInMemoryCache)
{
    builder.Services.AddMemoryCache();
    builder.Services.AddSingleton<ICacheService, InMemoryCacheService>();
}
else
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });
    builder.Services.AddSingleton<ICacheService, RedisCacheService>();
}


var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
