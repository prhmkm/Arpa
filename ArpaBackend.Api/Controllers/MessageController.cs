using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.Net;
using static ArpaBackend.Domain.DTOs.MessageDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public MessageController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpGet("GetAllMessages")]
        [CustomerAccessDenyAttribute]
        public IActionResult GetAllMessages([FromHeader] int pagesize, [FromHeader] int pageNumber, [FromHeader] string? language, [FromHeader] bool? isSeen)
         {
            try
            {
                var languageId = 0;
                if (!string.IsNullOrEmpty(language))
                {
                    if (!TextHelpers.IsDigitsOnly(language))
                    {
                        Language _language = _service.Language.GetByCode(language);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(language))
                    {
                        languageId = Convert.ToInt32(language);
                    }
                }
                List<GetAllMessagesResponse> res = _service.Message.GetMessages(isSeen, languageId);
                int max  = res.Count();
                if (pagesize != 0 && pageNumber != 0)
                {
                    res = res.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
                }
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "پیام‌ها با موفقیت ارسال شد",
                    Value = new { response = res , Max = max },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddMessageRequest addMessageRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(addMessageRequest.FullName) && !string.IsNullOrEmpty(addMessageRequest.Email) && !string.IsNullOrEmpty(addMessageRequest.Title) && !string.IsNullOrEmpty(addMessageRequest.LanguageCode))
                {
                    if (!string.IsNullOrEmpty(addMessageRequest.Mobile) && addMessageRequest.Mobile.Length != 11)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(addMessageRequest.Mobile) && !TextHelpers.IsDigitsOnly(addMessageRequest.Mobile))
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
                        }
                        var lang = _service.Language.GetByCode(addMessageRequest.LanguageCode);
                        if (lang == null)
                        {
                            lang.Id = 0;
                        }
                        Message message = new Message()
                        {
                            LanguageId = lang.Id,  
                            FullName = addMessageRequest.FullName,
                            Mobile = addMessageRequest.Mobile,
                            Email = addMessageRequest.Email,
                            Title = addMessageRequest.Title,
                            Description = addMessageRequest.Description,
                        };
                        _service.Message.AddMessage(message);
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "پیام با موفقیت ثبت شد", Value = new { }, Error = new { } });
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

        [HttpGet("GetById")]
        [CustomerAccessDenyAttribute]
        public IActionResult GetById([FromHeader] string? language, [FromHeader] int MessageId)
        {
            try
            {
                var languageId = 0;
                if (!string.IsNullOrEmpty(language))
                {
                    if (!TextHelpers.IsDigitsOnly(language))
                    {
                        Language _language = _service.Language.GetByCode(language);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(language))
                    {
                        languageId = Convert.ToInt32(language);
                    }
                }
                GetAllMessagesResponse res = _service.Message.GetMessageById(MessageId, languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "اطلاعات با موفقیت ارسال شد",
                    Value = new { response = res },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
    }
}
