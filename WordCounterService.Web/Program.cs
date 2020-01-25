using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WordCounterService.Web
{
    /// <summary>
    /// Defines the entry point to the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
