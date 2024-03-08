using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccess.Data;
using ShoppingCart.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Utility.DbInitializer;
using ShoppingCart.DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using ShoppingCart.Utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
  
// Add services to the container.
builder.Services.AddControllersWithViews();

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//STRIPE
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("PaymentSettings"));

builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer,DbInitializerRepo>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    opt.LoginPath = $"/Identity/Account/Login" ;
    opt.LogoutPath = $"/Identity/Account/Logout";
});

builder.Services.AddSession(opt => {
    opt.IdleTimeout = TimeSpan.FromMinutes(100);
    opt.Cookie.HttpOnly = true ;
    opt.Cookie.IsEssential = true ;
});

builder.Services.AddRazorPages();

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
app.UseSession();
app.UseRouting();
dataSedding();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("PaymentSettings:SecretKey").Get<string>();
app.UseAuthentication();;
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
void dataSedding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initializer();
    }
}