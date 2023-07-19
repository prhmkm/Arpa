using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.TVCoverDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TVCoverController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public TVCoverController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [CustomerAccessDeny]
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddTVCoverRequest addTVCoverRequest)
        {
            try
            {

                if (addTVCoverRequest.LanguageId==0||string.IsNullOrEmpty(addTVCoverRequest.HTMLText))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات وارد شده نامعتبر است", Value = new { }, Error = new { } });
                }
                else
                {
                    if(addTVCoverRequest.IsActive && _service.TVCover.ExistActiveCover(addTVCoverRequest.LanguageId))
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "برای زبان انتخاب شده، کاور فعال وجود دارد", Value = new { }, Error = new { } });
                    TVCover TVCoverCreated = new TVCover()
                    {
                        LanguageId = addTVCoverRequest.LanguageId,
                        HTMLText = addTVCoverRequest.HTMLText,
                        IsActive = addTVCoverRequest.IsActive,
                        Title = addTVCoverRequest.Title
                    };
                    _service.TVCover.Add(TVCoverCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات با موفقیت ثبت شد", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetTVCover")]
        public IActionResult GetTVCover([FromHeader] string? language)
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

                ShowAllTVCovers res = _service.TVCover.GetActiveCover(languageId);
                
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات با موفقیت ارسال شد", Value = new { Response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [CustomerAccessDeny]
        [HttpPost("BOGetAllTVCovers")]
        public IActionResult BOGetAllTVCovers([FromHeader] int pagesize, [FromHeader] int pageNumber, [FromHeader] string? language)
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
                List<BOShowAllTVCovers> res = _service.TVCover.BOGetAll(pagesize, pageNumber, languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات ها با موفقیت ارسال شد", Value = new { Max = _service.TVCover.BOGetAll(languageId).Count, response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [CustomerAccessDeny]
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditTVCoverRequest tVCover)
        {
            try
            {
                if (tVCover.Id == 0 || tVCover.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اطلاعات اشتباهی وارد شده است", Value = new { }, Error = new { } });
                }
                else if (tVCover.LanguageId == 0 || string.IsNullOrEmpty(tVCover.HTMLText))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات وارد شده نامعتبر است", Value = new { }, Error = new { } });
                }
                else
                {
                    TVCover TVCoverCreated = _service.TVCover.GetById(tVCover.Id);
                    if (TVCoverCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعاتی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                   
                    TVCoverCreated.HTMLText = tVCover.HTMLText;
                    TVCoverCreated.LanguageId = tVCover.LanguageId.GetValueOrDefault();
                    TVCoverCreated.IsActive = tVCover.IsActive;
                    TVCoverCreated.Title = tVCover.Title;
                    _service.TVCover.Edit(TVCoverCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [CustomerAccessDeny]
        [HttpGet("Delete")]
        public IActionResult Delete([FromHeader] int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اطلاعات اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    TVCover TVCoverCreated = _service.TVCover.GetById(id);
                    if (TVCoverCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعاتی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }

                    TVCoverCreated.IsDelete = true;
                    _service.TVCover.Edit(TVCoverCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات با موفقیت حذف شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }

    }
}
