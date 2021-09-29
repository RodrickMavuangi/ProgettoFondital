using Fondital.Data;
using Fondital.Repository;
using Fondital.Server.Automapper;
using Fondital.Services;
using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Models.Settings;
using Fondital.Shared.Services;
using Fondital.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using Fondital.Server.Automapper;
using Fondital.Repository;
using Fondital.Shared.Settings;
using Serilog;
using Fondital.Server.Controllers;

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
            services.AddDbContext<FonditalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddDefaultIdentity<Utente>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.SignIn.RequireConfirmedAccount = false;
                }).AddRoles<Ruolo>().AddEntityFrameworkStores<FonditalDbContext>();

            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                    options.Configuration = new OpenIdConnectConfiguration();
                });

            services.AddControllers().AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.PropertyNamingPolicy = null; // prevent camel case
                });
            services.AddRazorPages();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<RestClientSettings>(Configuration.GetSection("RestClientSettings"));
            services.AddHttpClient<RestExternalServiceController>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            var logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            services.AddSingleton<ILogger>(logger);
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
            services.AddTransient<IRapportoService, RapportoService>();

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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (Configuration.GetValue<bool>("IsSwaggerEnabled"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fondital.Server v1"));
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}