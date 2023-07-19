using RestSharp;


namespace ArpaBackend.Service.Remote.Interfaces
{
    public interface IMagfaService
    {
        Task<RestResponse> SmsSend(string recipientNumber, string messageText);
    }
}
