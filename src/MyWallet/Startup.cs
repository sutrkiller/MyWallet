using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWallet.Configuration;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Repositories;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Middlewares;
using MyWallet.Services.Configuration;
using MyWallet.Services.Services;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("googleauth.json", optional: false)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionOptions>(
                    options => options.ConnectionString = Configuration.GetConnectionString("MyWalletConnection"))

                .AddScoped<IBudgetService, BudgetService>().
                AddScoped<ICategoryService, CategoryService>().
                AddScoped<IGroupService, GroupService>().
                AddScoped<IEntryService, EntryService>().
                AddScoped<IUserService, UserService>();
            // Add framework services.
            services.AddMvc();

            services.AddTransient<IBudgetRepository, BudgetRepository>();
            services.AddTransient<IEntryRepository, EntryRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IConversionRatioRepository, ConversionRatioRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            var mapper = new MapperConfiguration(cfg =>
            {
                ViewModelsMapperConfiguration.InitializeMappings(cfg);
                ServicesMapperConfiguration.InitializeMappings(cfg);
            }).CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton(mapper)
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //This should call CNB as soon as possible
            app.UseCurrencyUpdaterMiddleware();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookie",
                AccessDeniedPath = new PathString("/Accounts/AccessDenied"),
                LoginPath = new PathString("/Accounts/AccessDenied"),
                AutomaticChallenge = true,
                AutomaticAuthenticate = true,
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = Configuration["web:client_id"],
                ClientSecret = Configuration["web:client_secret"],
                AuthenticationScheme = "Google",
                CallbackPath = "/Home/Index",
                SignInScheme = "Cookie",
                AutomaticAuthenticate = true,
                Events = new OAuthEvents
                {
                    OnTicketReceived = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetService<IUserService>();
                        userService.EnsureUserExists(context.Principal.Identity as ClaimsIdentity);
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("GetConversionRatiosByCurrencyId",
                                "entries/GetConversionRatiosByCurrencyId/",
                                new { controller = "Entries", action = "GetConversionRatiosByCurrencyId" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");                
            });
        }
    }
}
