using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace ArpaBackend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IpconverterController : Controller
    {
        IServiceWrapper _service;
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;
        public IpconverterController(IServiceWrapper service, IOptions<AppSettings> appSettings, IRepositoryWrapper repository)
        {
            _appSettings = appSettings.Value;
            _service = service;
            _repository = repository;
        }
        [HttpPost("GetCountryCode")]
        public IActionResult GetCountryCode([FromHeader] string ip)
        {
            try
            {
                var res = _service.IpconverterService.IpToCountryCode(ip);
                if(res == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد کشور با موفقیت ارسال شد", Value = new { response = _appSettings.DefaultLanguage }, Error = new { } }); ;
                }
                var code = _repository.IpConverter.CountryNameToCountryCode(res.Country,_appSettings.DefaultLanguage);

                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد کشور با موفقیت ارسال شد", Value = new { response = code }, Error = new { } }); ;
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
