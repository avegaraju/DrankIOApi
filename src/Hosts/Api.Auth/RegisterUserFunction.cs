using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.Lambda.Core;
using System;
using Microsoft.Extensions.DependencyInjection;
using DrankIO.Domain.Users;
using System.Net;

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
            var apiRequest = JsonSerializer.Deserialize<GoogleRegisterUserApiRequest>(request.Body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            var useCase = _serviceProvider.GetService<IRegisterUserUseCase>();
            await useCase.ExecuteAsync(apiRequest.Code);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}