using GestionNotificaciones.Data;
using GestionNotificaciones.Services.Repositories.NotificacionRepository;
using GestionNotificaciones.Services.Repositories.TagRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GestionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GestionDbConnectionString")));


builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();

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
