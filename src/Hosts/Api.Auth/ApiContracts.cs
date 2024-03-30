namespace Auth
{
    public record class GoogleRegisterUserApiRequest(string Code);
    public record class GetGoogleAccessTokenApiRequest(string credentials);
}

