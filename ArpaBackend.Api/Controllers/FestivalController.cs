using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.FestivalDTO;
using static ArpaBackend.Domain.DTOs.MovieDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FestivalController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public FestivalController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        [CustomerAccessDenyAttribute]
        public IActionResult Add([FromBody] AddFestival addFestival)
        {
            try
            {
                if (addFestival == null)
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
                if (addFestival.LanguageId == null || addFestival.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestival.Title))
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
                if (string.IsNullOrEmpty(addFestival.Description))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "توضیحات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addFestival.Cover))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تصویر جشنواره وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                //------------------------------------------- saving video
                var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addFestival.CoverTitle, addFestival.Cover, 5);
                //------------------------------------------- saving video

                if (cover.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "خطا در ثبت تصویر ",
                        Value = new { },
                        Error = new { }
                    });
                }

                Festival festivalCreated = new Festival()
                {
                    LanguageId = addFestival.LanguageId,
                    Description = addFestival.Description.Trim(),
                    Title = addFestival.Title.Trim(),
                    VideoLength = addFestival.VideoLength,
                    VideoSize = addFestival.VideoSize,
                    CoverURL = cover.Address,
                    IsActive = addFestival.IsActive
                };
                _service.Festival.Add(festivalCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "جشواره با موفقیت ثبت شد",
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
        [HttpPost("Edit")]
        [CustomerAccessDenyAttribute]
        public IActionResult Edit([FromBody] EditFestival editFestival)
        {
            try
            {
                if (editFestival == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "اطلاعات وارد شده نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editFestival.Id == null || editFestival.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه جشواره نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }

                Festival festivalCreated = _service.Festival.GetById(editFestival.Id);
                if (festivalCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه جشواره نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editFestival.LanguageId == null || editFestival.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editFestival.Title))
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
                if (string.IsNullOrEmpty(editFestival.Description))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "توضیحات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editFestival.Cover))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تصویر وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editFestival.Cover == festivalCreated.CoverURL)
                {
                    festivalCreated.CoverURL = editFestival.Cover;
                }
                else if (editFestival.Cover != festivalCreated.CoverURL)
                {
                    var res = _service.Picture.GetByAddress(festivalCreated.CoverURL);
                    if (res != null)
                    {
                        var coverName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FestivalCover\\" + coverName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\FestivalCover\\" + coverName);
                        _service.Picture.DeleteById(res.Id);
                    }

                    //------------------------------------------- saving cover
                    var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editFestival.CoverTitle, editFestival.Cover, 5);
                    //------------------------------------------- saving cover

                    festivalCreated.CoverURL = cover.Address;
                }

                festivalCreated.LanguageId = editFestival.LanguageId.GetValueOrDefault();
                festivalCreated.Description = editFestival.Description.Trim();
                festivalCreated.Title = editFestival.Title.Trim();
                festivalCreated.VideoLength = editFestival.VideoLength;
                festivalCreated.VideoSize = editFestival.VideoSize;
                festivalCreated.IsActive = editFestival.IsActive;

                _service.Festival.Edit(festivalCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "ویرایش با موفقیت انجام شد",
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
        [CustomerAccessDenyAttribute]
        public IActionResult Delete([FromBody] DeleteFestival deleteFestival)
        {
            try
            {
                if (deleteFestival.Id == 0 || deleteFestival.Id == null)
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
                    Festival festivalCreated = _service.Festival.GetById(deleteFestival.Id);
                    if (festivalCreated == null)
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

                    festivalCreated.IsDelete = true;
                    _service.Festival.Edit(festivalCreated);

                    //var res = _service.Picture.GetByAddress(festivalCreated.VideoURL);
                    //if (res != null)
                    //{
                    //    var videoName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                    //    if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Festival\\" + videoName))
                    //        System.IO.File.Delete(_appSettings.SaveImagePath + "\\Festival\\" + videoName);
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
        [HttpGet("BOGetAll")]
        [CustomerAccessDenyAttribute]
        public IActionResult BOGetAll([FromHeader] int pagesize, [FromHeader] int pageNumber, [FromHeader] int languageId)
        {
            try
            {
                List<BOShowFestivals> res = _service.Festival.BOGetAll(pagesize, pageNumber, languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "اطلاعات با موفقیت ارسال شد",
                    Value = new { Max = _service.Festival.BOGetAll(languageId).Count, response = res },
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
        [HttpGet("GetById")]
        [CustomerAccessDenyAttribute]
        public IActionResult GetById([FromHeader] string? language, [FromHeader] int festivalId)
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
                BOShowFestivals res = _service.Festival.BOGetById(languageId, festivalId);
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
