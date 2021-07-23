using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Assignment.Web.Filters;
using Assignment.Infrastructure.Data.Extensions;
using Assignment.Application.Extensions;
using Assignment.Infrastructure.Utility.Jwt.Extensions;
using Assignment.Web.Extensions;
using FluentValidation;
using Assignment.Application.Features.Accounts.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Assignment.Infrastructure.Utility.Notification.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Assignment.Infrastructure.Utility.Notification;
using Assignment.Application.Common.Behaviours;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace Assignment.Web
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
            services.AddJwtUtilities(new JwtUtilityOptions
            {
                Issuer = Configuration["JwtUtility:Issuer"],
                Secret = Configuration["JwtUtility:Secret"]
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtUtility:Issuer"],
                    ValidAudience = Configuration["JwtUtility:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtUtility:Secret"]))
                };

                o.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = (c =>
                    {
                        return System.Threading.Tasks.Task.CompletedTask;
                    }),
                    OnAuthenticationFailed = (c =>
                    {
                        return System.Threading.Tasks.Task.CompletedTask;
                    }),
                    OnMessageReceived = (c =>
                    {
                        return System.Threading.Tasks.Task.CompletedTask;
                    })
                };
            });

            services.AddAuthorization();

            services.AddDbServices(Configuration);
            services.AddApplication();

            services.AddSendGridNotifications(new SendGridEmailNotificationUtilityOptions {
                ApiKey = Configuration["SendGrid:ApiKey"],
                Sender = Configuration["SendGrid:Sender"]
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            services.AddControllersWithViews()
                .AddNewtonsoftJson(c => c.UseCamelCasing(true));

            services.AddSwaggerDocument(configure =>
            {
                configure.Title = "Assignment API";
            });
            
            services.AddScoped<AllowedContentTypesFilter>();
            services.AddValidatorsFromAssemblyContaining(typeof(RequestValidationBehavior<,>));

            services.Configure<ActivationMailOptions>(Configuration.GetSection("SendGrid:VerificationMail"));
            services.Configure<CreateTokenCommandOptions>(Configuration.GetSection("JwtUtility"));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseGlobalExceptionHandler();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });


        }
    }
}
