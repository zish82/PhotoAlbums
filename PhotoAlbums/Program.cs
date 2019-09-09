using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Lamar.Microsoft.DependencyInjection;

namespace PhotoAlbums
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLamar()
                .UseStartup<Startup>();
    }
}
