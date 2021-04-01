using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestExecuter.Services
{
    public class RequestService
    {
        private readonly IConfiguration configuration;

        public RequestService(IConfiguration configuration)
        {
            this.configuration = configuration 
                    ?? throw new ArgumentNullException(nameof(configuration));
        }


        public async Task ExecuteAsync()
        {
            var name = configuration["Name"];
            var url = configuration["Endpoint"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{url}");

            HttpResponseMessage response = await client.GetAsync("?name={name}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Ok!");
            }
            else
            {
                Console.WriteLine("Error!: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }
    }
}
