namespace DrankIO.Domain
{
    public interface IGetGoogleAccessToken
    {
        string GetAccessToken(string authenticationCode);
    }

    public class GetGoogleAccessToken : IGetGoogleAccessToken
    {
        public string GetAccessToken(string authenticationCode)
        {
            throw new NotImplementedException();
        }
    }
}