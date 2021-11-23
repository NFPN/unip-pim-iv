using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BlackRiver.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost(x => x.UseUrls("https://localhost:4000", "http://localhost:4001"))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
