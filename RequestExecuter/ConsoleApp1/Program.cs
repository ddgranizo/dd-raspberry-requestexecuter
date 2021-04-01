using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RequestExecuter.Services;

namespace RequestExecuter
{
    class Program
    {

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await RunAsync(host.Services, args);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("AppSettings.json", optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<RequestService>();
                });


        private static async Task RunAsync(IServiceProvider services, string[] args)
        {
            await services.GetService<RequestService>().ExecuteAsync();
        }


    }
}
