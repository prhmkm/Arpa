using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.PageDetailDTO;
using static ArpaBackend.Domain.DTOs.QuestionDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public QuestionController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion([FromBody] AddQuestion addQuestion)
        {
            try
            {
                if (addQuestion == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addQuestion.LanguageId == null || addQuestion.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addQuestion.QuestionTitle))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addQuestion.QuestionAnswer))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جواب سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                Question questionCreated = new Question()
                {
                    LanguageId = addQuestion.LanguageId,
                    QuestionTitle = addQuestion.QuestionTitle,
                    QuestionAnswer = addQuestion.QuestionAnswer
                };
                _service.Question.Add(questionCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "سوال با موفقیت ثبت شد",
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
        [HttpPost("EditQuestion")]
        public IActionResult EditQuestion([FromBody] EditQuestion editQuestion)
        {
            try
            {
                if (editQuestion == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editQuestion.Id == 0 || editQuestion.Id == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " شناسه سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var questionCreated = _service.Question.GetById(editQuestion.Id);
                if (questionCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " سوالی با این شناسه یافت نشد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editQuestion.LanguageId == null || editQuestion.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editQuestion.QuestionTitle))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editQuestion.QuestionAnswer))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جواب سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }


                questionCreated.LanguageId = editQuestion.LanguageId.GetValueOrDefault();
                questionCreated.QuestionTitle = editQuestion.QuestionTitle;
                questionCreated.QuestionAnswer = editQuestion.QuestionAnswer;
                questionCreated.IsActive = editQuestion.IsActive;

                _service.Question.Edit(questionCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "سوال با موفقیت به روز رسانی شد",
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
        [HttpPost("DeleteQuestion")]
        public IActionResult DeleteQuestion([FromBody] DeleteQuestion deleteQuestion)
        {
            try
            {
                if (deleteQuestion == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " اطلاعات فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (deleteQuestion.Id == 0 || deleteQuestion.Id == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " شناسه سوال فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var questionCreated = _service.Question.GetById(deleteQuestion.Id);
                if (questionCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = " سوالی با این شناسه یافت نشد",
                        Value = new { },
                        Error = new { }
                    });
                }

                questionCreated.IsDelete = true;

                _service.Question.Edit(questionCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "سوال با موفقیت حذف شد",
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
        [AllowAnonymous]
        [HttpGet("GetAllQuestions")]
        public IActionResult GetAllQuestions([FromHeader] string? language)
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

                List<ShowQuestions> res = _service.Question.GetAll(languageId);
                List<KeyValuePair<string, List<ShowQuestions>>> keyValuePair = new List<KeyValuePair<string, List<ShowQuestions>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowQuestions>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "سوالات با موفقیت ارسال شد",
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
        [HttpGet("BOGetAllQuestions")]
        public IActionResult BOGetAllQuestions([FromHeader] string? language)
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

                List<BOShowQuestions> res = _service.Question.BOGetAll(languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "سوالات با موفقیت ارسال شد",
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
