﻿using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Companies.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddPolicy("AllowAll", p =>
                p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

        });
    }
    /*
    public static void ConfigureOpenApi(this IServiceCollection services) =>
        services.AddEndpointsApiExplorer()
                .AddSwaggerGen(setup =>
                {
                    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Place to add JWT with Bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });

                    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                });


    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped(provider => new Lazy<IAuthService>(() => provider.GetRequiredService<IAuthService>()));
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //ToDo: Set in "Manage User Secrets" (right click project and you find it!). Example "secretkey" : "ReallyLongKeyItDosentWorkIfItsNotAtleat32Caracters!!!!!!!!!!!!!!!!!!!!!!!!"
        var secretkey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(secretkey, nameof(secretkey));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            ArgumentNullException.ThrowIfNull(jwtSettings, nameof(jwtSettings));

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
            };

        });
    }

    */
}
