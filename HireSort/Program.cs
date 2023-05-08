using HireSort.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HireSort.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DbCo>(optiopns)
builder.Services.AddDbContext<HRContext>(options =>
           options.UseSqlServer(
               builder.Configuration.GetConnectionString("HRdb")));
// Add services to the container.
builder.Services.AddControllersWithViews();
//Dependencies List
builder.Services.AddServiceDependency();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}");

app.MapControllerRoute(
      name: "areaRoute",
      pattern: "{area:exists}/{controller}/{action}"
  );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeCandidate}/{action=IndexCandidate}/{id?}");

app.Run();
