using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mission08_Team0303.Data;
using Mission08_Team0303.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Mission08_Team0303.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ Initialize SQLite
SQLitePCL.Batteries.Init();

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Use SQLite
var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// ✅ Register Repository Pattern
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// ✅ Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();