
using System.Text;
using GameShop.Infrastructure;
using GameShop.Application.Interfaces;
using GameShop.Application.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Identity;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using GameShop.Application.Mappings;
using Microsoft.AspNetCore.Mvc.Formatters;
using GameShop.Infrastructure.Extensions;
using GameShop.UI.Extensions;
using GameShop.Infrastructure.Interfaces;
using GameShop.Infrastructure.Identity;
using System;
using GameShop.Application.Basket;
using GameShop.Application.Photos;
using Stripe;
using GameShop.Domain.Logic;
using GameShop.Application.StockManipulations;
using System.Linq;
using Newtonsoft.Json;

namespace GameShop.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;              
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            services.ConfigureDbContext(Configuration);

            ////
            // REFERENCE LOOP HANDLING WAS HERE //
            ////

            services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();

            services.AddScoped<IAddStockToBasket, AddStockToBasket>();
            services.AddScoped<IAddPhotoToCloud, AddPhotoToCloud>();
            services.AddScoped<ICountOrderPrice, CountOrderPrice>();
            services.AddScoped<ICreateCharge, CreateCharge>();
            services.AddScoped<ITransferStockToStockOnHold, TransferStockToStockOnHold>();
            services.AddScoped<ITransferStockOnHoldWhenExpire, TransferStockOnHoldWhenExpire>();
            services.AddScoped<IDeleteStockFromBasket, DeleteStockFromBasket>();
            services.AddScoped<ISynchronizeBasket, SynchronizeBasket>();

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<BasketSettings>(Configuration.GetSection("BasketSettings"));
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.ConfigureUnitOfWork();
            services.ConfigureAuthentication(Configuration);

            services.ConfigureAuthorization();

            services.ConfigureControllers(); 

            services.AddSession(options => 
            {
                var cookieExpireDays = int.Parse(Configuration["BasketSettings:CookieExpireDays"]);
                options.Cookie.MaxAge = TimeSpan.FromDays(cookieExpireDays);
                options.Cookie.Name = "Basket";

            });

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["PrivateKey"];

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //"/Error"
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null) 
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        } 
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            // app.UseRewriter(new RewriteOptions()
            //     .AddRedirect("index.html", "/"));

            

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
