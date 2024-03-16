
using Domain.Ports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Google.Tests
{
    public class GoogleApiClientTests
    {
        private string? _profileFields;
        public GoogleApiClientTests()
        {
           // RunTestHost();
        }

        private void RunTestHost()
        {
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(x =>
            {
                x.Listen(System.Net.IPAddress.Any, 5445);
            });

            var app =  builder.Build();

            app.UseRouting();

            app.MapGet("personFields={fields}&sources=READ_SOURCE_TYPE_PROFILE",
                async context =>
                {
                    _profileFields = context.Request?.RouteValues["fields"]?.ToString();
                    await context.Response.WriteAsync("baba");
                });

            app.RunAsync();
        }

        [Fact(Skip ="Skip for now")]
        public void GetUser_SendsExpectedRequestToGoogle()
        {
            var sut = CreateSut();
        }

        private IGoogleApiClient CreateSut()
        {

            Func<HttpClient> client = () => new HttpClient()
            {
                BaseAddress = new Uri("http://localhost")
            };
            

            return new GoogleApiClient(client);
        }
    }
}