using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketVerkoop.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Repositories;
using TicketVerkoop.Repositories.Interfaces;
using TicketVerkoop.Services;
using TicketVerkoop.Util.Mail;
using TicketVerkoop.Util.Mail.Interfaces;
using TicketVerkoop.Util.PDF;
using TicketVerkoop.Util.PDF.Interfaces;
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

//----> Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API Employee",
        Version = "version 1",
        Description = "An API to perform Employee operations",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "CDW",
            Email = "christophe.dewaele@vives.be",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "Employee API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

builder.Services.AddTransient<IMatchService<Match>, MatchService>();
builder.Services.AddTransient<IMatchDAO<Match>, MatchDAO>();

builder.Services.AddTransient<IService<Stadium>, StadiumService>();
builder.Services.AddTransient<IDAO<Stadium>, StadiumDAO>();

builder.Services.AddTransient<IService<Ploeg>, PloegService>();
builder.Services.AddTransient<IDAO<Ploeg>, PloegDAO>();

builder.Services.AddTransient<IGetAllByService<Section>, SectionService>();
builder.Services.AddTransient<IGetAllByDAO<Section>, SectionDAO>();

builder.Services.AddTransient<IRingService<Ring>, RingService>();
builder.Services.AddTransient<IRingDAO<Ring>, RingDAO>();

builder.Services.AddTransient<IService<Bestelling>, BestellingService>();
builder.Services.AddTransient<IDAO<Bestelling>, BestellingDAO>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Ticket.Verkoop.Session";

    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//Mail
builder.Services.AddControllersWithViews();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
// Configuration.GetSection("EmailSettings")) zal de instellingen opvragen uit de AppSettings.json file en vervolgens wordt er een emailsettings - object aangemaakt en de waarden worden geïnjecteerd in het object
builder.Services.AddSingleton<IEmailSend, EmailSend>();
//Als in een Constructor een IEmailSender-parameter wordt gevonden, zal een emailSender - object worden aangemaakt. 

builder.Services.AddTransient<ICreatePDF, CreatePDF>();

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

var swaggerOptions = new TicketVerkoop.Options.OptionsSwagger();
builder.Configuration.GetSection(nameof(TicketVerkoop.Options.OptionsSwagger)).Bind(swaggerOptions);
// Enable middleware to serve generated Swagger as a JSON endpoint.
//RouteTemplate legt het path vast waar de JSON?file wordt aangemaakt
app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
//// By default, your Swagger UI loads up under / swagger /.If you want to change this, it's thankfully very straight?forward.
//Simply set the RoutePrefix option in your call to app.UseSwaggerUI in Program.cs:
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});
app.UseSwagger();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
