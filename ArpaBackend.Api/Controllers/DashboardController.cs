using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public DashboardController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpGet("DashBoardInfo")]
        public IActionResult DashBoardInfo()
        {
            try
            {
                int[] res0 = new int[] { 0, 0, 13, 0, 0 };


                string[] res1 = new string[] { "در انتظار تعمیر", "تعمیر شده", "مرجوعی", "بدون گارانتی", "تحویل داده شده" };


                string[] res2 = new string[] { "1400/10", "1400/11", "1400/12", "1401/01", "1401/02", "1401/03", "1401/04", "1401/05", "1401/06", "1401/07", "1401/08", "1401/09", "1401/10", };


                int[] res3 = new int[] { 10, 5, 15, 2, 9, 30, 0, 20, 10, 50, 2, 16, 3 };


                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات‌ با موفقیت ارسال شد", Value = new { response0 = res0, response1 = res1, response2 = res2, response3 = res3 }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new
                    {
                        Response = ex.ToString()
                    }
                });
            }
        }

        [HttpGet("VisitorsInfo")]
        public IActionResult VisitorsInfo()
        {
            try
            {

                List<DashboardDTO.VisitorsInfo> visitors = new List<DashboardDTO.VisitorsInfo>();
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 924,
                    Country = "ایران",
                    Percentage = 49.79
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 334,
                    Country = "افغانستان",
                    Percentage = 17.99
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 139,
                    Country = "عراق",
                    Percentage = 7.49
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 12,
                    Country = "آلمان",
                    Percentage = 0.64
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 2,
                    Country = "آمریکا",
                    Percentage = 0.1
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 68,
                    Country = "تاجیکستان",
                    Percentage = 3.67
                });
                visitors.Add(new DashboardDTO.VisitorsInfo
                {
                    Count = 377,
                    Country = "سوریه",
                    Percentage = 20.32
                });

                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات‌ با موفقیت ارسال شد", Value = new { Response = visitors }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new
                    {
                        Response = ex.ToString()
                    }
                });
            }
        }
        [HttpGet("VisitByDate")]
        public IActionResult VisitByDate()
        {
            try
            {
                Random rnd = new Random();
                List<DashboardDTO.VisitByDate> visits = new List<DashboardDTO.VisitByDate>();
                for (var i = 0; i < 30; i++)
                {
                    visits.Add(new DashboardDTO.VisitByDate
                    {
                        Count = rnd.Next(1001),
                        Date = DateHelpers.ToPersianDate(DateTime.Now.AddDays(i), false, "/")
                    });
                }

                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اطلاعات‌ با موفقیت ارسال شد", Value = new { Response = visits }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new
                    {
                        Response = ex.ToString()
                    }
                });
            }
        }
    }
}
