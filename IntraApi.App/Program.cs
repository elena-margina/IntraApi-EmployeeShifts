using IntraApi.App.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<EmployeeShiftService>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddHttpClient("IntraApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7077/"); 
});

var app = builder.Build();

app.Map("/", context => Task.Run(() => context.Response.Redirect("/employeeshifts")));

app.UseExceptionHandler("/Error"); 
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred.");
        context.Response.Redirect("/Error");
    }
});

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
