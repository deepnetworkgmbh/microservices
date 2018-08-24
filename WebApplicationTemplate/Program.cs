using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace WebApplicationTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
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
