using IntraApi.App.Data;
using IntraApi.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<EmployeeShiftService>();
builder.Services.AddScoped<RoleService>();

// Add HttpClient
builder.Services.AddHttpClient("IntraApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7077/"); // Replace with your API base URL
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
