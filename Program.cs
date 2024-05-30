using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Services;
//using SnackisApp.DAL;
using SnackisApp.Models;
using SnackisApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<AdminHelper>();

builder.Services.AddDefaultIdentity<SnackisUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()

    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers(); 

    builder.Services.AddHttpClient<DiscussionService>();
         builder.Services.AddHttpClient<DailyphilosopherService>();
 
 // Add HttpClient (API)
builder.Services.AddHttpClient();

  // Register DiscussionService
    builder.Services.AddScoped<DiscussionService>();

// Register Dailyphilosophers
     builder.Services.AddScoped<DailyphilosopherService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
     
    var userManager = services.GetRequiredService<UserManager<SnackisUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedData.Initialize(context, userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers(); // Ensure this is added to map API controllers



app.Run();
