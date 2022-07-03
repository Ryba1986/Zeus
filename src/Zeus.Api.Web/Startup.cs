using System;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Zeus.Enums.Tokens;
using Zeus.Utilities.Extensions;
using Zeus.Validators.Users;

namespace Zeus.Api.Web
{
   internal sealed class Startup
   {
      public IConfiguration Configuration { get; }

      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public void ConfigureServices(IServiceCollection services)
      {
         services
            .AddControllers()
            .AddJsonOptions(x =>
            {
               x.JsonSerializerOptions.WriteIndented = false;
               x.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .AddFluentValidation(config =>
            {
               config.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
               config.DisableDataAnnotationsValidation = true;
            });

         services
            .AddAuthentication(x =>
            {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
               x.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateLifetime = true,
                  ValidateAudience = false,
                  ValidateIssuer = true,
                  ValidIssuer = JwtIssuer.Web.GetDescription(),
                  ClockSkew = TimeSpan.Zero,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"] ?? string.Empty))
               };
            });
      }

      public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseRouting()
            .UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto })
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(x => { x.MapDefaultControllerRoute(); });
      }
   }
}
