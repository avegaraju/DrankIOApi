using Amazon.CognitoIdentityProvider;
using Domain.Ports;
using DrankIO.Adapters.Cognito;
using DrankIO.Domain.Ports;
using DrankIO.Domain.Users;
using Google;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DrankIO.Auth
{
    internal static class ServiceProviderBuilder
    {
        public static IServiceProvider Build()
        {
            IServiceCollection serviceCollection
                = new ServiceCollection();

            ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IAmazonCognitoIdentityProvider client = new AmazonCognitoIdentityProviderClient();
            serviceCollection.AddTransient<ICognitoClient>(_=> new CognitoClient(client));
            serviceCollection.AddTransient<IGoogleApiClient, GoogleApiClient>();
            serviceCollection.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
