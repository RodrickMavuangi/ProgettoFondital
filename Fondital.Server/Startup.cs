using Fondital.Data;
using Fondital.Server.Extensions;
using Fondital.Services;
using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Models.Settings;
using Fondital.Shared.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

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

            services.AddDefaultIdentity<Utente>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.SignIn.RequireConfirmedAccount = false;
                }).AddRoles<Ruolo>().AddEntityFrameworkStores<FonditalDbContext>();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddIdentityServer( opts => opts.Cors.CorsPolicyName = "CorsPolicy")
            .AddConfigurationStore(options =>
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
                }
            ).AddInMemoryCaching().AddClientStore<InMemoryClientStore>().AddResourceStore<InMemoryResourcesStore>()
            .AddApiAuthorization<Utente, FonditalDbContext>(
                //options =>
                //{
                //    // Clients
                //    var spaClient = ClientBuilder
                //        .SPA("Fondital.Client")
                //        .WithRedirectUri("https://localhost:5001/authentication/login-callback")
                //        .WithLogoutRedirectUri("https://localhost:5001/authentication/login")
                //        .WithScopes("openid", "profile")
                //        .Build();
                //    spaClient.AllowedCorsOrigins = new[]
                //    {
                //        "https://localhost:5003"
                //    };
                //
                //    options.Clients.Add(spaClient);
                //}
            );

            services.AddAuth(jwtSettings);

            services.AddControllers().AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.PropertyNamingPolicy = null; // prevent camel case
                });
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
            services.AddTransient<IServicePartnerService, ServicePartnerService>();
            services.AddTransient<IConfigurazioneService, ConfigurazioneService>();
            services.AddTransient<IDifettoService, DifettoService>();
            services.AddTransient<IVoceCostoService, VoceCostoService>();
            services.AddTransient<IListinoService, ListinoService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<ILavorazioneService, LavorazioneService>();

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
                };
                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fondital.Server v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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