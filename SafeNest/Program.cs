using SafeNest.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Register Repository (SQLite)
builder.Services.AddSingleton<Repository>(provider =>
{
    string dbPath = Path.Combine(AppContext.BaseDirectory, "safenest.db");
    string conn = $"Data Source={dbPath}";
    return new Repository(conn);
});

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();
