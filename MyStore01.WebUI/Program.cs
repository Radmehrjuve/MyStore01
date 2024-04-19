using Microsoft.EntityFrameworkCore;
using MyStore01.WebUI.Models;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionsstring = builder.Configuration.GetConnectionString("MyStoreConnectionString");
builder.Services.AddDbContext<MyStoreContext>(opt => opt.UseSqlServer(connectionsstring));
builder.Services.AddIdentity<Appuser, IdentityRole>(options => { options.Password.RequiredLength = 6; }).AddEntityFrameworkStores<MyStoreContext>();
builder.Services.AddScoped<IStoreRepository>();
builder.Services.AddScoped<EFStoreRepository>();
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
app.UseAuthentication();
app.UseAuthorization();
SeedData.EnsurePopulated(app);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
