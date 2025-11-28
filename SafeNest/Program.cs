using SafeNest.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Register repository as a Singleton
builder.Services.AddSingleton(new Repository(
    builder.Configuration.GetConnectionString("Default")
));

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();
