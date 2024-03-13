using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DrankIO.Auth
{

    public class RegisterUserFunction
    {
        IServiceProvider _serviceProvider;
        public RegisterUserFunction(): this(ServiceProviderBuilder.Build())
        {
            
        }

        public RegisterUserFunction(IServiceProvider serviceProvider )
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<APIGatewayProxyResponse> ApiRequestHandler(APIGatewayProxyRequest request)
        {
            var code = JsonSerializer.Deserialize<GoogleRegisterUserApiRequest>(request.Body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return new APIGatewayProxyResponse();
        }
    }
}