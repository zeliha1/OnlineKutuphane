using Microsoft.EntityFrameworkCore;
using OnlineKutuphane.Models;
using OnlineKutuphane.Utility;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<KutuphaneDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servisini ekliyoruz
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Rolleri eklemek için
    .AddEntityFrameworkStores<KutuphaneDbContext>();

builder.Services.AddRazorPages();

// _kitapTuruRepository nesnesi => Dependency Injection
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

var app = builder.Build();

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rolleri ve kullanýcýlarý seed etmek için metodu çaðýrýyoruz
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Rolleri ve kullanýcýlarý seed et
        await SeedData.SeedRolesAsync(roleManager);
        await SeedData.SeedUsersAsync(userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding roles and users: {ex.Message}");
    }
}

app.Run();

