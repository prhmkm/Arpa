using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using static ArpaBackend.Domain.DTOs.FestivalVideoDTO;
using static ArpaBackend.Domain.DTOs.MovieDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FestivalVideoController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public FestivalVideoController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddFestivalVideo addFestivalVideo)
        {
            try
            {
                if (addFestivalVideo == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "اطلاعات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.FirstName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.LastName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام خانوادگی وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.LanguageCode))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "زبان وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.NationalCode))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد ملی وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (!SecurityHelpers.IsValidNationalCode(addFestivalVideo.NationalCode))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد ملی نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addFestivalVideo.ProvinceId == null || addFestivalVideo.ProvinceId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه استان وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addFestivalVideo.FestivalId == null || addFestivalVideo.FestivalId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه جشنواره وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addFestivalVideo.CityId == null || addFestivalVideo.CityId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه شهر وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.Video))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ویدیو وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestivalVideo.VideoTitle))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام ویدیو وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                //------------------------------------------- saving video
                var video = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addFestivalVideo.VideoTitle, addFestivalVideo.Video, 4);
                //------------------------------------------- saving video

                if (video.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "خطا در ثبت ویدیو ",
                        Value = new { },
                        Error = new { }
                    });
                }

                var languageId = 0;
                if (!string.IsNullOrEmpty(addFestivalVideo.LanguageCode))
                {
                    if (!TextHelpers.IsDigitsOnly(addFestivalVideo.LanguageCode))
                    {
                        Language _language = _service.Language.GetByCode(addFestivalVideo.LanguageCode);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(addFestivalVideo.LanguageCode))
                    {
                        languageId = Convert.ToInt32(addFestivalVideo.LanguageCode);
                    }
                }
                FestivalVideo festivalVideoCreated = new FestivalVideo()
                {
                    LanguageId = languageId,
                    FirstName = addFestivalVideo.FirstName.Trim(),
                    LastName = addFestivalVideo.LastName.Trim(),
                    NationalCode = addFestivalVideo.NationalCode.Trim(),
                    ProvinceId = addFestivalVideo.ProvinceId,
                    CityId = addFestivalVideo.CityId,
                    VideoURL = video.Address,
                    FestivalId = addFestivalVideo.FestivalId,
                    UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value)
            };
                _service.FestivalVideo.Add(festivalVideoCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "ویدیو جشواره با موفقیت ثبت شد",
                    Value = new { },
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
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteFestivalVideo deleteFestivalVideo)
        {
            try
            {
                if (deleteFestivalVideo.Id == 0 || deleteFestivalVideo.Id == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه جشنواره نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                else
                {
                    FestivalVideo festivalVideoCreated = _service.FestivalVideo.GetById(deleteFestivalVideo.Id);
                    if (festivalVideoCreated == null)
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = "شناسه جشنواره نامعتبر است",
                            Value = new { },
                            Error = new { }
                        });
                    }

                    festivalVideoCreated.IsDelete = true;
                    _service.FestivalVideo.Edit(festivalVideoCreated);

                    //var res = _service.Picture.GetByAddress(festivalVideoCreated.VideoURL);
                    //if (res != null)
                    //{
                    //    var videoName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                    //    if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FestivalVideo\\" + videoName))
                    //        System.IO.File.Delete(_appSettings.SaveImagePath + "\\FestivalVideo\\" + videoName);
                    //    _service.Picture.DeleteById(res.Id);
                    //}

                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "جشنواره با موفقیت حذف شد",
                        Value = new { },
                        Error = new { }
                    });
                }
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
        [CustomerAccessDenyAttribute]
        [HttpGet("BOGetAllByFestivalId")]
        public IActionResult BOGetAllByFestivalId([FromHeader] string? language, [FromHeader] int festivalId)
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
                List<BOShowFestivalVideos> res = _service.FestivalVideo.BOGetAll(languageId,festivalId);
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
