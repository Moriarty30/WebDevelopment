using EmployeesWeb.Application.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
ConfigurationManager Configuration = builder.Configuration; // allows both to access and to set up the config
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiConfiguration>(Configuration.GetSection("ApiConfiguration"));

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
    // Users
    endpoints.MapControllerRoute(
      name: "Employees",
      pattern: "{controller=Employees}/{action=Index}/{id?}");
});

app.Run();
