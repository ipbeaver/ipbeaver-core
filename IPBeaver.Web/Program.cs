using Microsoft.Extensions.Caching.Memory;
using IPFox;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapGet("/", () => "This is a Nissl product");
app.MapGet("/search/{ip}", async (string ip, IMemoryCache memoryCache) => {
    if (memoryCache.TryGetValue<RegionDetail>(ip, out var regionDetail))
    {
        return regionDetail;
    }
    else
    {
        var region = await IPGeodataRepository.GetIPLocationAsync(ip);
        memoryCache.Set(ip, region);
        return region;
    }
    });
app.Run();
