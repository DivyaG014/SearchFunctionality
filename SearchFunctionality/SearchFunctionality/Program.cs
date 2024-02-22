
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using SearchFunctionality.BusinessLogic.Services;
using SearchFunctionality.DataContext.Models;
using SearchFunctionality.Middleware;
using System.Text;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var section = builder.Configuration.GetSection("NLog");
        LogManager.Configuration = new NLogLoggingConfiguration(section);
        var logger = LogManager.GetCurrentClassLogger();
        logger.Info(typeof(Program).Assembly.FullName + " starting");
        try
        {
            builder.Services.AddDbContext<airline_dbContext>(options => options.UseSqlServer(builder.Configuration["DatabaseConnectionString"]));
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IAirportService, AirportService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            ConfigureLogging(builder);
            ConfigureSwagger(builder);
            builder.Services.AddMvc();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            ConfigureMiddleware(app);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();         
            await app.RunAsync();
        }
        finally
        {
            LogManager.Flush();
            LogManager.Shutdown();
        }
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        app.UseMiddleware<RequestMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT token with 'Bearer' prefix.",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            swagger.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        openApiSecurityScheme,
                       Array.Empty<string>()
                    }
                });
        });
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        var options = new ApplicationInsightsServiceOptions();
        options.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
        builder.Services.AddApplicationInsightsTelemetry(options);
        builder.Host.UseNLog();
    }
}