using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.MovieDTO;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;
using static ArpaBackend.Domain.DTOs.TVCoverDTO;

namespace ArpaBackend.Api.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PageDetailController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public PageDetailController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("GetAllPageDetails")]
        public IActionResult GetAllPageDetails([FromHeader] string? language, [FromHeader] string? URL)
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
                if (string.IsNullOrEmpty(URL))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "آدرس صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                List<ShowPageDetails> res = _service.PageDetail.GetAll(languageId, URL);
                List<KeyValuePair<string, List<ShowPageDetails>>> keyValuePair = new List<KeyValuePair<string, List<ShowPageDetails>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowPageDetails>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحات با موفقیت ارسال شد",
                    Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) },
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
        [HttpGet("BOGetAllPageDetails")]
        public IActionResult BOGetAllPageDetails([FromHeader] int? languageId, [FromHeader] int? pageId, [FromHeader] bool? isActive)
        {
            try
            {
                List<BOShowPageDetails> res = _service.PageDetail.BOGetAll(languageId.GetValueOrDefault(), pageId.GetValueOrDefault()).Where(w => isActive != null ? w.IsActive == isActive : true).ToList();
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحات با موفقیت ارسال شد",
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
        [HttpGet("GetAllPages")]
        public IActionResult GetAllPages()
        {
            try
            {
                List<Page> res = _service.PageDetail.GetAllPages();
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحات با موفقیت ارسال شد",
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
        [HttpPost("AddPageDetails")]
        public IActionResult AddPageDetails([FromBody] AddPageDetail addPageDetail)
        {
            try
            {
                if (addPageDetail == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addPageDetail.LanguageId == null || addPageDetail.LanguageId == 0)
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
                if (addPageDetail.PageId == null || addPageDetail.PageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPageDetail.PageTitle))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPageDetail.HTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                //var page = _service.PageDetail.GetPageByURL(addPageDetail.PageURL);
                //if(page == null)
                //{
                //    return Ok(new
                //    {
                //        TimeStamp = DateTime.Now,
                //        ResponseCode = HttpStatusCode.BadRequest,
                //        Message = "آدرس صفحه نادرستی وارد شده است",
                //        Value = new { },
                //        Error = new { }
                //    });
                //}
                if (addPageDetail.IsActive && _service.PageDetail.ExistActivePageDetail(addPageDetail.PageId, addPageDetail.LanguageId))
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "برای زبان انتخاب شده، صفحه فعال وجود دارد", Value = new { }, Error = new { } });
                PageDetail pageDetailCreated = new PageDetail()
                {
                    LanguageId = addPageDetail.LanguageId,
                    PageId = addPageDetail.PageId,
                    PageTitle = addPageDetail.PageTitle,
                    HTML = addPageDetail.HTML,
                    IsActive = addPageDetail.IsActive,
                };
                _service.PageDetail.Add(pageDetailCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحه با موفقیت ثبت شد",
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
        [HttpPost("DeletePageDetails")]
        public IActionResult DeletePageDetails([FromBody] DeletePageDetail deletePageDetail)
        {
            try
            {
                if (deletePageDetail == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (deletePageDetail.Id == null || deletePageDetail.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                var page = _service.PageDetail.GetById(deletePageDetail.Id);
                if (page == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه وارد شده نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                page.IsDelete = true;
                _service.PageDetail.Edit(page);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحه با موفقیت حذف شد",
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
        [HttpPost("EditPageDetails")]
        public IActionResult EditPageDetails([FromBody] EditPageDetail editPageDetail)
        {
            try
            {
                if (editPageDetail == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPageDetail.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPageDetail.LanguageId == 0)
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
                if (editPageDetail.PageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editPageDetail.PageTitle))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editPageDetail.HTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات صفحه وارد نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var page = _service.PageDetail.GetById(editPageDetail.Id);
                if (page == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه وارد شده نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if ((!page.IsActive.GetValueOrDefault()) && (editPageDetail.IsActive))
                {
                    if (_service.PageDetail.ExistActivePageDetail(page.PageId, page.LanguageId))
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = "برای این زبان، صفحه فعال وجود دارد.",
                            Value = new { },
                            Error = new { }
                        });
                    }
                }
                page.LanguageId = editPageDetail.LanguageId;
                page.PageId = editPageDetail.PageId;
                page.PageTitle = editPageDetail.PageTitle;
                page.HTML = editPageDetail.HTML;
                page.IsActive = editPageDetail.IsActive;
                _service.PageDetail.Edit(page);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحه با موفقیت ویرایش شد",
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
    }
}
