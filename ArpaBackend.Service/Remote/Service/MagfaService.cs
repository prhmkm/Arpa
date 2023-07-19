using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Service.Remote.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Text;

namespace ArpaBackend.Service.Remote.Service
{
    public class MagfaService : IMagfaService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSetting;
        public MagfaService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSetting = appSettings.Value;
            _repository = repository;
        }
        public async Task<RestResponse> SmsSend(string recipientNumber, string messageText)
        {
            string pass = _appSetting.MagfaSmsUsername + ":" + _appSetting.MagfaSmsPassword;
            string encodedStr = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(pass));        
            var url = _appSetting.MagfaSmsServiceUrl + "/api/ViraSms";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", encodedStr);
            request.AddParameter("application/json", "{\r\n\t\"RecipientNumber\":\"" + recipientNumber + "\",\r\n\t\"MessageText\":\"" + messageText + "\",\r\n    \"Authorization\":\"" + encodedStr + "\"\r\n}", ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }
    }
}
