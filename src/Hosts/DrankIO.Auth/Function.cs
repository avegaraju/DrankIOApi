using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;
using System.Text.Json;

namespace DrankIO.Auth
{
    public class Function
    {
        public async Task<APIGatewayProxyResponse> ApiRequestHandler(APIGatewayProxyRequest request)
        {
            var code = JsonSerializer.Deserialize<GoogleAuthApiRequest>(request.Body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return new APIGatewayProxyResponse();
        }
    }
}