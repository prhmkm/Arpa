using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.StreamDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StreamController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public StreamController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("GetAllOnlineStreams")]
        public IActionResult GetAllOnlineStreams()
        {
            try
            {
                List<OnlineStream> res = _service.Stream.GetAllStreams();
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "استریم‌ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddStreamRequest addStreamRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(addStreamRequest.Url) && !string.IsNullOrEmpty(addStreamRequest.Title))
                {
                    OnlineStream StreamCreated = new OnlineStream()
                    {
                        Url = addStreamRequest.Url,
                        Title = addStreamRequest.Title
                    };
                    _service.Stream.AddStream(StreamCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "استریم با موفقیت ثبت شد", Value = new { }, Error = new { } });
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = " یک یا چند مورد فرستاده نشده است", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("EditStream")]
        public IActionResult EditStream([FromBody] UpdateStreamRequest stream)
        {
            try
            {
                if (!string.IsNullOrEmpty(stream.Url) && !string.IsNullOrEmpty(stream.Title))
                {
                    if (stream.Id == 0 || stream.Id == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه استریم اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                    }
                    else
                    {
                        OnlineStream StreamCreated = _service.Stream.GetStreamById(stream.Id);
                        if (StreamCreated == null)
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "استریمی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                        }
                        StreamCreated.Url = stream.Url;
                        StreamCreated.Title = stream.Title;
                        StreamCreated.IsActive = stream.IsActive;
                        StreamCreated.CreationDateTime = StreamCreated.CreationDateTime;
                        _service.Stream.UpdateStream(StreamCreated);
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "استریم با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                    }
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = " یک یا چند مورد فرستاده نشده است", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
