using ArpaBackend.Core.Model.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ArpaBackend.Domain.DTOs.CustomerDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        private static Random random = new Random();

        public CustomerController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] CLoginRequest _singIn)
        {
            try
            {
                //----------------------------------------------------------------------------------Check parameters
                if (string.IsNullOrEmpty(_singIn.Mobile))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام کاربری یا ایمیل الزامی است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_singIn.Password))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کلمه عبور الزامی است",
                        Value = new { },
                        Error = new { }
                    });
                }
                //----------------------------------------------------------------------------------Check parameters

                //----------------------------------------------------------------------------------Find User                


                Customer customer = _service.Customer.GetCustomerLogin(_singIn.Mobile, _singIn.Password);
                if (customer == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = "نام کاربری یا کلمه عبور نادرست است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (customer.IsActive == false)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.MethodNotAllowed,
                        Message = "کاربر مورد نظر غیرفعال است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                var token = _service.Customer.GenToken(customer);
                var refreshToken = "";
                if (_singIn.RememberMe)
                {
                    Random random = new Random();
                    refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                }
                var lang = _appSettings.DefaultLanguage;
                if (customer.LanguageId != 0)
                {
                    var language = _service.Language.GetById(customer.LanguageId);
                    if (language != null)
                        lang = language.Code;
                }
                customer.RememberMe = _singIn.RememberMe;
                customer.RefreshToken = refreshToken;
                _service.Customer.EditCustomer(customer);

                CLoginResponse login = new CLoginResponse
                {

                    DisplayName = customer.FullName,
                    Mobile = customer.Mobile,
                    Token = token.AccessToken,
                    Language = lang,
                    RefreshToken = refreshToken,
                    CreationDateTime = customer.CreationDateTime
                };
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "ورود با موفقیت انجام شد.",
                    Value = new { Response = login },
                    Error = new { }
                });
                //----------------------------------------------------------------------------------Find User
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }

        }
        [AllowAnonymous]
        [HttpPost("InitialRegister")]
        public IActionResult InitialRegister([FromBody] SendSMSRequest sms)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "خطای ناشناخته", Value = new { }, Error = new { ErrorMsg = ModelState } });
                }
                if (!string.IsNullOrEmpty(sms.Mobile) && sms.Mobile.Count() == 11 && sms.Mobile.Substring(0, 2) == "09")
                {
                    Customer cus = _service.Customer.GetCustomerByMobile(sms.Mobile);
                    if (cus == null)
                    {
                        const string chars = "0123456789";
                        string token = new string(Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)]).ToArray());
                        _service.MagfaService.SmsSend(sms.Mobile, "کد ورود شما :" + token);
                        Customer customerCreated = new Customer()
                        {
                            Mobile = sms.Mobile,
                            VerificationCode = token
                        };
                        _service.Customer.AddCustomer(customerCreated);
                    }
                    if (cus != null && cus.Verify == false)
                    {
                        const string chars = "0123456789";
                        string token = new string(Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)]).ToArray());
                        _service.MagfaService.SmsSend(sms.Mobile, "کد ورود شما: " + token);
                        //Customer customerCreated = _service.Customer.GetCustomerByMobile(sms.Mobile);
                        cus.VerificationCode = token;
                        _service.Customer.EditCustomer(cus);
                    }
                    if (cus != null && cus.Verify == true)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام کرده اید", Value = new { }, Error = new { } });
                    }
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد تایید با موفقیت ارسال شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }

        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] CRegisterRequest register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "خطای ناشناخته", Value = new { }, Error = new { ErrorMsg = ModelState } });
                }
                if (!string.IsNullOrEmpty(register.Mobile) && !string.IsNullOrEmpty(register.Password) && !string.IsNullOrEmpty(register.Code) && !string.IsNullOrEmpty(register.FullName))
                {
                    if (register.Mobile.Count() == 11 && register.Mobile.Substring(0, 2) == "09")
                    {
                        Customer customer = _service.Customer.GetCustomerByMobile(register.Mobile);
                        if (customer == null)
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات وارد شده نامعتبر است", Value = new { }, Error = new { } });
                        }
                        if (customer.Verify)
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام کرده اید", Value = new { }, Error = new { } });
                        }
                        if (customer.VerificationCode != register.Code)
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کد نامعتبر است", Value = new { }, Error = new { } });
                        }
                        customer.Verify = true;
                        customer.FullName = register.FullName;
                        customer.Password = register.Password;
                        if (!string.IsNullOrEmpty(register.Language))
                        {
                            var lang = _service.Language.GetByCode(register.Language);
                            if (lang == null)
                            {
                                customer.LanguageId = 0;
                            }
                            else
                            {
                                customer.LanguageId = lang.Id;
                            }
                        }
                        var token = _service.Customer.GenToken(customer);
                        var refreshToken = "";
                        Random random = new Random();
                        refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                        customer.RememberMe = true;
                        customer.RefreshToken = refreshToken;
                        _service.Customer.EditCustomer(customer);

                        CLoginResponse login = new CLoginResponse
                        {
                            DisplayName = customer.FullName,
                            Mobile = customer.Mobile,
                            Token = token.AccessToken,
                            RefreshToken = refreshToken,
                            CreationDateTime = customer.CreationDateTime
                        };
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.OK,
                            Message = "ثبت نام با موفقیت انجام شد",
                            Value = new { Response = login },
                            Error = new { }
                        });
                    }
                    else
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
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
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] CRefreshTokenRequest _refreshTokenRequest)
        {
            try
            {
                //----------------------------------------------------------------------------------Check parameters
                if (_refreshTokenRequest is null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "داده های دریافتی معتبر نمی باشد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_refreshTokenRequest.RefreshToken))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "مقدار رفرش توکن الزامی است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_refreshTokenRequest.Mobile))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام کاربری الزامی است",
                        Value = new { },
                        Error = new { }
                    });
                }
                //----------------------------------------------------------------------------------Check parameters
                //----------------------------------------------------------------------------------Check Customer Exist
                Customer customer = _service.Customer.GetCustomerByMobile(_refreshTokenRequest.Mobile);
                if (customer == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = "نام کاربری یا کلمه عبور نادرست است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (customer.IsActive == false)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.MethodNotAllowed,
                        Message = "کاربر مورد نظر غیرفعال است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (customer.RefreshToken != _refreshTokenRequest.RefreshToken || !customer.RememberMe)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.MethodNotAllowed,
                        Message = "رفرش توکن نامعتبر است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                var token = _service.Customer.GenToken(customer);
                var refreshToken = "";
                Random random = new Random();
                refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                customer.RefreshToken = refreshToken;
                _service.Customer.EditCustomer(customer);

                CLoginResponse login = new CLoginResponse
                {

                    DisplayName = customer.FullName,
                    Mobile = customer.Mobile,
                    Token = token.AccessToken,
                    RefreshToken = refreshToken,
                    CreationDateTime = customer.CreationDateTime
                };
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "توکن با موفقیت بازیابی شد.",
                    Value = new { Response = login },
                    Error = new { }
                });
                //----------------------------------------------------------------------------------Check Customer Exist
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطا داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { message = ex.Message }
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetPasswordRecoverySMS")]
        public IActionResult GetPasswordRecoverySMS([FromBody] SendSMSRequest sms)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "خطای ناشناخته", Value = new { }, Error = new { ErrorMsg = ModelState } });
                }
                if (!string.IsNullOrEmpty(sms.Mobile) && sms.Mobile.Count() == 11 && sms.Mobile.Substring(0, 2) == "09")
                {
                    Customer cus = _service.Customer.GetCustomerByMobile(sms.Mobile);
                    if (cus == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام نکرده اید", Value = new { }, Error = new { } });
                    }
                    if (cus != null && cus.Verify == false)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام تکمیل نکرده اید", Value = new { }, Error = new { } });
                    }
                    if (cus != null && cus.Verify == true)
                    {
                        const string chars = "0123456789";
                        string token = new string(Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)]).ToArray());
                        _service.MagfaService.SmsSend(sms.Mobile, "کد ورود شما: " + token);
                        //Customer customerCreated = _service.Customer.GetCustomerByMobile(sms.Mobile);
                        cus.VerificationCode = token;
                        _service.Customer.EditCustomer(cus);
                    }
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد تایید با موفقیت ارسال شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("PasswordRecovery")]
        public IActionResult PasswordRecovery([FromBody] PasswprdRecovey recover)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "خطای ناشناخته", Value = new { }, Error = new { ErrorMsg = ModelState } });
                }
                if (!string.IsNullOrEmpty(recover.Mobile) && recover.Mobile.Count() == 11 && recover.Mobile.Substring(0, 2) == "09")
                {
                    Customer cus = _service.Customer.GetCustomerByMobile(recover.Mobile);
                    if (cus == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام نکرده اید", Value = new { }, Error = new { } });
                    }
                    if (cus != null && cus.Verify == false)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.NotAcceptable, Message = "شما قبلا با این شماره ثبت نام تکمیل نکرده اید", Value = new { }, Error = new { } });
                    }
                    if (cus != null && cus.Verify == true)
                    {
                        if (cus.VerificationCode != recover.Code)
                        {
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کد نامعتبر است", Value = new { }, Error = new { } });
                        }
                        cus.Password = recover.NewPassword;
                        _service.Customer.EditCustomer(cus);
                    }
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شماره موبایل نامعتبر است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کلمه عبور با موفقیت تغییر کرد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }

        }

    }
}
