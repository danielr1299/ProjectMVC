using Microsoft.EntityFrameworkCore;
using PetShop.Data.Contexts;
using PetShop.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetShopDbContext>(options =>
options.UseLazyLoadingProxies().UseSqlServer(
builder.Configuration.GetConnectionString("PetShopDataConnection")));

builder.Services.AddScoped<IRepository, CatgoryRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopDbContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
