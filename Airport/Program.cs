using Airport.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("AeroportoAPI", c =>
{
    var serviceUri = builder.Configuration["ServiceUri:AeroportoAPI"];
    if (string.IsNullOrEmpty(serviceUri))
    {
        throw new InvalidOperationException("ServiceUri:AeroportoAPI configuration is missing or empty.");
    }
    c.BaseAddress = new Uri(serviceUri);
});

builder.Services.AddScoped<IVooService, VooService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();
builder.Services.AddScoped<ICiaAereaService, CiaAereaService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
