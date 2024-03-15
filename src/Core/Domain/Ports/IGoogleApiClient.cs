namespace Domain.Ports
{
    public interface IGoogleApiClient
    {
        Task GetUser(string email);
    }
}
