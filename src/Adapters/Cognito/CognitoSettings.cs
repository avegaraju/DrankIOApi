namespace Cognito
{
    public class CognitoSettings
    {
        public string CognitoAppPoolClientId { get; set; }
        public string CognitoAppPoolClientSecret { get; set; }
        public string UserPoolId { get; set; }
        public string RedirectUri { get; set; }
    }
}
