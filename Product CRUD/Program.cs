// =============================================
// File: Program.cs
// Framework: ASP.NET Core 6+ (minimal hosting model)
// Purpose: App startup — registers services and middleware.
//
// Key differences from MVC 5:
//   - No Global.asax, no Startup.cs (merged into Program.cs in .NET 6+)
//   - Services registered via builder.Services
//   - Connection string read from appsettings.json via IConfiguration
//   - ProductRepository registered as a scoped service (DI container)
// =============================================

using ProductCRUD.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProductRepository>();
// -----------------------------------------------
// 1. Register MVC services
// -----------------------------------------------
builder.Services.AddControllersWithViews();

// -----------------------------------------------
// 2. Register ProductRepository for Dependency Injection
//    "Scoped" = one instance per HTTP request
//    The connection string is read from appsettings.json here
// -----------------------------------------------


var app = builder.Build();

// -----------------------------------------------
// 3. Configure the HTTP request pipeline (middleware)
// -----------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   // Serves wwwroot files (CSS, JS, images)
app.UseRouting();
app.UseAuthorization();

// -----------------------------------------------
// 4. Define the default route
//    Change "Home" to "Products" so the app opens on the product list
// -----------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
