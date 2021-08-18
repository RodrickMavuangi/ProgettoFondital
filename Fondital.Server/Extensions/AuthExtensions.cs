using Microsoft.Extensions.DependencyInjection;
using Fondital.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;

namespace Fondital.Server.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            JwtSettings jwtSettings)
        {
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("Direzione", policy => policy.RequireUserName("direzione@fondital.com"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddIdentityServerJwt()
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = jwtSettings.Audience;
                    options.Authority = jwtSettings.Issuer;
                    options.IncludeErrorDetails = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Configuration = new OpenIdConnectConfiguration();
                });

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}