using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using music_manager_starter.Data;
using System.Security.AccessControl;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure Serilog globally for logging to the console.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("The global logger has been configured.");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{

    var path = "App_Data";

    if (!Path.Exists(path)) Directory.CreateDirectory(path);
    var dbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();


    dbContext.Database.Migrate();

}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
