namespace Domain.Ports
{
    public interface IGoogleApiClient
    {
        Task Validate(string accessCode);
    }
}
