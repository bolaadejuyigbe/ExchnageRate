using ExchangeRate_API.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate_API
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        public static async Task Main(string[] args)
        {
           
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                var host = CreateWebHostBuilder(args).Build();
                using (var serviceScope = host.Services.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                    await dbContext.Database.MigrateAsync();
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        var adminRole = new IdentityRole("Admin");
                        await roleManager.CreateAsync(adminRole);
                    }

                    if (!await roleManager.RoleExistsAsync("User"))
                    {
                        var userRole = new IdentityRole("User");
                        await roleManager.CreateAsync(userRole);
                    }

                }
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                
                .UseStartup<Startup>();
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .UseSerilog()
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
