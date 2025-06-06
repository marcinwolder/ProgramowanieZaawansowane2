using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Airly.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AirlyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AirlyContext") ?? throw new InvalidOperationException("Connection string 'AirlyContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.Use(async (ctx, next) =>
{
    await next();
    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        string originalPath = ctx.Request.Path.Value!;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Auth/Login";
        await next();
    }
});

app.UseSession();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
