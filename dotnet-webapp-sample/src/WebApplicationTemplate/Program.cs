using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using WebApplicationTemplate.Common;

namespace WebApplicationTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                // Initialize other dependencies
                StateManager.SetHealthy();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Failed to initialize dependencies");
                StateManager.RequestRestart();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseSerilog((ctx, config) =>
                {
                    config
                        .MinimumLevel.Information()
                        .Enrich.FromLogContext();

                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        config.WriteTo.Console();
                    }
                    else
                    {
                        config.WriteTo.Console(new ElasticsearchJsonFormatter());
                    }
                })
                .UseStartup<Startup>();
    }
}
