using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Extentions;
using CodeAcademyEventManagementSystem.Profile;
using CodeAcademyEventManagementSystem.Repository.Implementation;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Implementation;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<EventSystemDB>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<EventSystemDB>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

            builder.Services.AddCustomRepositories();
            builder.Services.AddCustomServices();
            builder.Services.AddAutoMapper(typeof(CustomProfile).Assembly);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await SeedData.Initialize(services); // await ilə çağırılır
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Verilenler bazasinda melumatlar doldurularken xeta bas verdi");
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())   
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
