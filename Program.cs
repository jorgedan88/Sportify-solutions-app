using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models; // Ajusta esto al namespace correcto de tu DbContext
using Microsoft.Extensions.DependencyInjection;
using Sportify_back.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión para SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Usa SQLite en lugar de SQL Server
builder.Services.AddDbContext<SportifyDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuración de Identity y Claims personalizada
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SportifyDbContext>()
    .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>();

builder.Services.AddControllersWithViews();

// Configuración de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministradorOnly", policy =>
        policy.RequireClaim("Profile", "Administrador"));
});

var app = builder.Build();

// Configura el pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
