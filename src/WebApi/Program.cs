using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Filters;

namespace WebApi;

public sealed class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        try {
            Log.Information("Starting web application.");

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers(options => 
                options.Filters.Add<OperationCancelledExceptionFilter>());

            builder.Services.AddScoped<IRepository<Film>, FilmRepository>();
            builder.Services.AddScoped<IRepository<ListItem>, ListItemRepository>();
            builder.Services.AddScoped<IRepository<FilmList>, FilmListRepository>();

            builder.Services.AddScoped<IFilmService, FilmService>();
            builder.Services.AddScoped<IListItemService, ListItemService>();
            builder.Services.AddScoped<IFilmListService, FilmListService>();

            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "MyFilmList API",
                    Description = "An ASP.NET Core Web API for managing your personalized watch lists.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.UseInlineDefinitionsForEnums();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex) {
            Log.Fatal(ex, "Application terminated unexpectedly.");
            throw;
        }
        finally {
            Log.CloseAndFlush();
        }
    }
}
