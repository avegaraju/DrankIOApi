using Amazon.CognitoIdentityProvider;
using Domain.Ports;
using Cognito;
using Domain.Users;
using Google;
using Microsoft.Extensions.DependencyInjection;
using System;
using Api.Auth;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Auth
{
    internal static class ServiceProviderBuilder
    {
        public static IServiceProvider Build()
        {
            IServiceCollection serviceCollection
                = new ServiceCollection();

            var settings = BuildSettings();

            ConfigureServices(serviceCollection, settings);

            return serviceCollection.BuildServiceProvider();
        }

        private static LambdaSettings BuildSettings()
        {
            IConfigurationBuilder configurationBuilder =
               new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory());

            if (ApplicationEnvironment.IsDevelopment())
                configurationBuilder.AddUserSecrets<LambdaSettings>();

            configurationBuilder.AddEnvironmentVariables("DOTNET_")
                .AddEnvironmentVariables();

            IConfiguration configuration = configurationBuilder.Build();

            return configuration.Get<LambdaSettings>();
        }

        private static void ConfigureServices(
            IServiceCollection serviceCollection,
            LambdaSettings settings
            )
        {
            IAmazonCognitoIdentityProvider client = new AmazonCognitoIdentityProviderClient();
            serviceCollection.AddTransient<ICognitoClient>(_=> new CognitoClient(client, new CognitoSettings
            {
                CognitoAppPoolClientId = settings.CognitoPoolId
            }));
            serviceCollection.AddTransient<IGoogleApiClient, GoogleApiClient>();
            serviceCollection.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
        }
    }
}
