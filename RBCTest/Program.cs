using Microsoft.EntityFrameworkCore;
using RBCTest.Models;

namespace RBC_Test
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<RBCTestContext>(options => options.UseSqlServer(@"Server=DESKTOP-0I2CLND;Database=RBCTest;Trusted_Connection=True;"));

            var app = builder.Build();


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            app.Run();
        }
    }
}