using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;

using Shop.Database;
using Shop.Domain.Infrastructure;
using Shop.UI.Infrastructure;

namespace Shop.UI
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation()
                .AddRazorPagesOptions(options => {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(_config["DefaultConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                // options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                options.AddPolicy("Manager", policy => policy.RequireAssertion(context => 
                    context.User.HasClaim("Role", "Manager")
                    || context.User.HasClaim("Role", "Admin")
                ));
            });

            services.AddAntiforgery(options => {
                options.Cookie.Name = "X-CSRF-TOKEN-COOKIENAME";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.Name = "Identity";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddSession(options => {
                options.Cookie.Name = "cart";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
                // options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            // services.AddTransient<IStockManager, StockManager>();
            // services.AddTransient<IProductManager, ProductManager>();
            // services.AddTransient<IOrderManager, OrderManager>();
            // services.AddTransient<IUserManager, UserManager>();
            // services.AddScoped<ISessionManager, SessionManager>();

            // services.AddTransient<CreateUser>();
            services.AddApplicationServices();

            StripeConfiguration.ApiKey = _config.GetSection("Stripe")["SecretKey"];
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
