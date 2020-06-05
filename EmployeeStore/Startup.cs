using EmployeeStore.Models;
using EmployeeStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace EmployeeStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:DbConnect:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:IdentityDbConnect:ConnectionString"]));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            
            //Email confirm services
            services.AddSingleton<IEmailSender, EmailService>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
                options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
                options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));

                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("ManagerRolePolicy", policy => policy.RequireRole("manager"));
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "242360318944-4lie4m28u92kp7blq8un1aamd4f0uj77.apps.googleusercontent.com";
                    options.ClientSecret = "FqaqPZe7v8ONq6sUsYCKJ-Bj";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "187371895819224";
                    options.AppSecret = "328f5c9e3d0d598790031bfc1cc2da9b";
                });
            services.AddTransient<IEmployeeRepository, EFEmployeeRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: null,
                   pattern: "{category}/Page{productPage:int}",
                   defaults: new { controller = "Home", action = "Index" }
                   );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 }
                    );
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
