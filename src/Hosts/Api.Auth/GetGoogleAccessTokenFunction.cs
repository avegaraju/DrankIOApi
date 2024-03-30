using Amazon.Lambda.APIGatewayEvents;
using Auth;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Domain.Tokens;

namespace Api.Auth
{
    public class GetGoogleAccessTokenFunction
    {
        IServiceProvider _serviceProvider;
        public GetGoogleAccessTokenFunction() : this(ServiceProviderBuilder.Build())
        {

        }

        public GetGoogleAccessTokenFunction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<APIGatewayProxyResponse> ApiRequestHandler(APIGatewayProxyRequest request)
        {
            var apiRequest = JsonSerializer.Deserialize<GetGoogleAccessTokenApiRequest>(request.Body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            var useCase = _serviceProvider.GetRequiredService<IGetGoogleAccessTokenUseCase>();
            var accessToken = await useCase.ExecuteAsync(apiRequest.Credentials);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = accessToken
            };
        }
    }
}
