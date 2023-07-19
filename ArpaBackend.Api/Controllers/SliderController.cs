using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.FestivalDTO;
using static ArpaBackend.Domain.DTOs.SliderDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SliderController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public SliderController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddSliderRequest addSliderRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(addSliderRequest.SlideImage) && addSliderRequest.LanguageId != null)
                {
                    //addSliderRequest.Alias = addSliderRequest.Alias.ToLower().Replace(" ", "-");
                    //string txt = "";
                    //for (int i = 0; i < addSliderRequest.Alias.Length; i++)
                    //{
                    //    txt += "-";
                    //}
                    //if (txt == addSliderRequest.Alias)
                    //{
                    //    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "اسم مستعار نامعتبر است", Value = new { }, Error = new { } });
                    //}
                    //if (_service.Slider.BOGetSliderByAlias(addSliderRequest.Alias, addSliderRequest.LanguageId).Count() != 0)
                    //{
                    //    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "برای این اسلایدر این زبان ثبت شده است", Value = new { }, Error = new { } });
                    //}
                    //------------------------------------------- saving video
                    var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-Slide", addSliderRequest.SlideImage, 1);
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
                    Slider SliderCreated = new Slider()
                    {
                        LanguageId = addSliderRequest.LanguageId,
                        SlideImage = cover.Address,
                        URL = addSliderRequest.URL,
                        IsDefault = addSliderRequest.IsDefault.GetValueOrDefault()
                    };
                    _service.Slider.AddSlider(SliderCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلایدر با موفقیت ثبت شد", Value = new { }, Error = new { } });
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = " عکس فرستاده نشده است", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetAllSliders")]
        public IActionResult GetAllSliders([FromHeader] string? language)
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
                List<Slider> res = _service.Slider.GetAllSliders(languageId);
                List<Slider> def = _service.Slider.GetAllDefaultSliders();
                List<KeyValuePair<string, List<Slider>>> keyValuePair = new List<KeyValuePair<string, List<Slider>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<Slider>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                keyValuePair.Add(new KeyValuePair<string, List<Slider>>("Default", def));
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلاید ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpGet("BOGetAllSliders")]
        public IActionResult BOGetAllSliders([FromHeader] int languageId, [FromHeader] bool? isActive)
        {
            try
            {
                List<ShowSliders> res = _service.Slider.BOGetAllSliders(languageId).Where(w => isActive != null ? w.IsActive == isActive.GetValueOrDefault() : true).ToList();
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلاید ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] UpdateSliderRequest slider)
        {
            try
            {
                if (slider.Id == 0 || slider.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلاید اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Slider SliderCreated = _service.Slider.GetSliderById(slider.Id);
                    if (SliderCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اسلایدی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(slider.SlideImage))
                    {
                        slider.SlideImage = SliderCreated.SlideImage;
                    }
                    if (slider.IsActive == null)
                    {
                        slider.IsActive = SliderCreated.IsActive;
                    }
                    if (string.IsNullOrEmpty(slider.URL))
                    {
                        slider.URL = SliderCreated.URL;
                    }
                    if (slider.LanguageId == null)
                    {
                        slider.LanguageId = SliderCreated.LanguageId;
                    }
                    if (slider.IsDefault == null)
                    {
                        slider.IsDefault = SliderCreated.IsDefault;
                    }
                    if (slider.SlideImage != SliderCreated.SlideImage)
                    {
                        var res = _service.Picture.GetByAddress(SliderCreated.SlideImage);
                        if (res != null)
                        {
                            var coverName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Slides\\" + coverName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\Slides\\" + coverName);
                            _service.Picture.DeleteById(res.Id);
                        }

                        //------------------------------------------- saving cover
                        var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-Slide", slider.SlideImage, 1);
                        //------------------------------------------- saving cover

                        SliderCreated.SlideImage = cover.Address;
                    }
                    SliderCreated.URL = slider.URL;
                    SliderCreated.LanguageId = slider.LanguageId;
                    SliderCreated.IsDefault = slider.IsDefault.GetValueOrDefault();
                    SliderCreated.IsActive = slider.IsActive;
                    SliderCreated.CreationDateTime = SliderCreated.CreationDateTime;
                    _service.Slider.UpdateSlider(SliderCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلاید با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteSliderRequest slider)
        {
            try
            {
                if (slider.Id == 0 || slider.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلاید اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Slider SliderCreated = _service.Slider.GetSliderById(slider.Id);
                    if (SliderCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اسلایدری با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    //if (SliderCreated.LanguageId == _service.Language.GetByCode(_appSettings.DefaultLanguage).Id)
                    //{
                    //    List<ShowSliders> res = _service.Slider.BOGetSliderByAlias(SliderCreated.Alias, 0);
                    //    //foreach (var item in res)
                    //    //{
                    //    //    item.IsDelete = true;
                    //    //    _service.Movie.UpdateMovie(item);
                    //    //}
                    //    if (res.Count > 1)
                    //    {
                    //        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "برای این اسلاید زبان های دیگری وچود دارد", Value = new { }, Error = new { } });
                    //    }
                    //}
                    SliderCreated.IsDelete = true;
                    _service.Slider.UpdateSlider(SliderCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلایدر با موفقیت حذف شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("GetSlideImage")]
        public IActionResult GetSlideImage([FromHeader] int id)
        {
            try
            {
                string res = _service.Slider.ShowSlideImage(id);
                if (string.IsNullOrEmpty(res))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلایدر اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "عکس اسلایدر با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
