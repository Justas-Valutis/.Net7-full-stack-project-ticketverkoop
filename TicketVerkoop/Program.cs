using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services;
using TicketVerkoop.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//----> Dependency Injection
//syntax services.AddTransient<interface, implType>();
builder.Services.AddTransient<IService<Match>, MatchService>();
builder.Services.AddTransient<IDAO<Match>, MatchDAO>();

builder.Services.AddTransient<IService<Stadium>, StadiumService>();
builder.Services.AddTransient<IDAO<Stadium>, StadiumDAO>();

builder.Services.AddTransient<IService<Ploeg>, PloegService>();
builder.Services.AddTransient<IDAO<Ploeg>, PloegDAO>();
//Automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
