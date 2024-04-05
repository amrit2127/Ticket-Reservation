using Microsoft.EntityFrameworkCore;
using NationalPark_WebApp_116.Data;
using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Repository;
using NationalPark_WebApp_116.Repository.IRepository;
using Stripe;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string cs = builder.Configuration.GetConnectionString("conStr");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(cs, sqlServerOptionsAction: sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5, // Adjust as needed
        maxRetryDelay: TimeSpan.FromSeconds(30), // Adjust as needed
        errorNumbersToAdd: null
    );
}));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cs));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<StripeSettings, StripeSettings>();


builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));   


builder.Services.AddHttpClient();



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

app.UseRouting();
//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
