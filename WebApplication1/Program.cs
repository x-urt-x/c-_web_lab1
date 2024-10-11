var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute("Home", "{controller=Home}/{action=EntryView}/{Id?}");

app.Run();