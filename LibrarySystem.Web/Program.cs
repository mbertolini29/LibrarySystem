using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Repositories.Data;
using LibrarySystem.Models;
using LibrarySystem.Utilities;
using LibrarySystem.Utilities.Seeding;
using LibrarySystem.Repositories.UnitOfWorkPattern;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LibrarySystemWebContextConnection") ?? throw new InvalidOperationException("Connection string 'LibrarySystemWebContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Data Source=MAURIX\\SQLEXPRESS;Initial Catalog=LibrarySystemDb;User Id=sa;Password=departamento;TrustServerCertificate=True;"));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("LibrarySystemWebContextConnection"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>(); //esto me hace ruido. no se si va "ApplicationDbContext --> ? "
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



//builder.Services.AddScoped<IUnitOfWork, UnitOfWork();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
DataSeeding();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initialize();
    }
}