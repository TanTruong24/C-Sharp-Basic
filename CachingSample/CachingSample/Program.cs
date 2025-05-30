using CachingSample.Data;
using CachingSample.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL + EF
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dotnetSeries")));

// DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
