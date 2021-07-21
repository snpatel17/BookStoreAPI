using BookStoreAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            //1. Get the IHost which will host this application.
            var host = CreateHostBuilder(args).Build();

            //2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of BookStoreContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BookStoreContext>();

                //4. Call the AddBookData to create sample data
                AddBookData.Initialize(services);
            }

            //Continue to run the application
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
