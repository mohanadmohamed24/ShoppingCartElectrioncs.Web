using Microsoft.EntityFrameworkCore;
using ShoppingCartElectrioncs.DataAccess;
using ShoppingCartElectrioncs.DataAccess.Implementation;
using ShoppingCartElectrioncs.Entities.Rebositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ShoppingCartElectrioncs.Utilities;
using Microsoft.Extensions.Options;
using Stripe;

namespace ShoppingCartElectrioncs.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //register in rin time
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            //register in Db
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            builder.Services.Configure<StripeData>(builder.Configuration.GetSection("stripe"));
            //register identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
            options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4)
         ).AddDefaultTokenProviders().AddDefaultUI()
          .AddEntityFrameworkStores<ApplicationDbContext>();
            //register Email in identiy
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            //register unit of work 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

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

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();


            app.MapRazorPages();
            app.MapControllerRoute(
             name: "default",
             pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
             app.MapControllerRoute(
             name: "Customer",
             pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
