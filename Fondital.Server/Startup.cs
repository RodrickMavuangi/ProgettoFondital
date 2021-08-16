using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Fondital.Data;
using Fondital.Shared;
using Fondital.Shared.Services;
using Fondital.Services;
using Fondital.Shared.Models.Auth;
using Fondital.Server.Extensions;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using System.Reflection;

namespace Fondital.Server
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
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddDbContext<FonditalDbContext>(options => options.UseSqlServer(Configuration["Database:ConnectionString"]));
            //services.AddIdentity<Utente, Ruolo>(options =>
            //   {
            //       options.Password.RequiredLength = 8;
            //       options.Password.RequireNonAlphanumeric = true;
            //       options.Password.RequireUppercase = true;
            //       options.SignIn.RequireConfirmedAccount = false;
            //   }).AddEntityFrameworkStores<FonditalDbContext>().AddDefaultTokenProviders();

            services.AddDefaultIdentity<Utente>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.SignIn.RequireConfirmedAccount = false;
                }).AddRoles<Ruolo>().AddEntityFrameworkStores<FonditalDbContext>();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddIdentityServer().AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(Configuration["Database:ConnectionString"], sql =>
                        sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            }).AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration["Database:ConnectionString"],
                            sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                }).AddInMemoryCaching().AddClientStore<InMemoryClientStore>().AddResourceStore<InMemoryResourcesStore>(); //AddApiAuthorization<Utente, FonditalDbContext>();
    
            services.AddAuth(jwtSettings);

            services.AddControllers();
            services.AddRazorPages();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        //.AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUtenteService, UtenteService>();
            services.AddTransient<ITraceService, TraceService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fondital.Server", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security = new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    },
                                    UnresolvedReference = true
                            },
                            new List<string>()
                    }
                }; c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fondital.Server v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/.well-known/openid-configuration")
            //        context.Request.Path = "/.well-known/openid-configuration.json";
            //
            //    await next();
            //});
            
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}