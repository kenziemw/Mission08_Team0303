using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Initialize SQLite Batteries
SQLitePCL.Batteries.Init();

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Use SQLite instead of SQL Server
var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// Apply pending migrations (ensures the database is up-to-date)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
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