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
using static ArpaBackend.Domain.DTOs.MovieDTO;
using static ArpaBackend.Domain.DTOs.SliderDTO;

namespace ArpaBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public MovieController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddMovieRequest addMovieRequest)
        {
            try
            {

                if (!string.IsNullOrEmpty(addMovieRequest.Title) && !string.IsNullOrEmpty(addMovieRequest.Poster) && !string.IsNullOrEmpty(addMovieRequest.Cover) && !string.IsNullOrEmpty(addMovieRequest.URL) && addMovieRequest.CategoryId != null && !string.IsNullOrEmpty(addMovieRequest.Rating) && !string.IsNullOrEmpty(addMovieRequest.ReleaseDate) && addMovieRequest.GenreId.Count > 0 && !string.IsNullOrEmpty(addMovieRequest.ManufacturedBy) && !string.IsNullOrEmpty(addMovieRequest.Language) && addMovieRequest.LanguageId != null && !string.IsNullOrEmpty(addMovieRequest.Alias))
                {
                    addMovieRequest.Alias = addMovieRequest.Alias.ToLower().Replace(" ", "-");
                    string txt = "";
                    for (int i = 0; i < addMovieRequest.Alias.Length; i++)
                    {
                        txt += "-";
                    }
                    if (txt == addMovieRequest.Alias)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "اسم مستعار نامعتبر است", Value = new { }, Error = new { } });
                    }
                    if (_service.Movie.GetMovieByAlias(addMovieRequest.Alias, addMovieRequest.LanguageId).Count() != 0)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "برای این فیلم این زبان ثبت شده است", Value = new { }, Error = new { } });
                    }
                    if (!TextHelpers.IsDigitsOnly(addMovieRequest.Rating.Replace(".", "")))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "امتیاز نامعتبر است", Value = new { }, Error = new { } });
                    }
                    if (!TextHelpers.IsDigitsOnly(addMovieRequest.ReleaseDate.Replace("/", "")))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "تاریخ نامعتبر است", Value = new { }, Error = new { } });
                    }

                    if (addMovieRequest.HaveSubtitle == null)
                    {
                        addMovieRequest.HaveSubtitle = false;
                    }

                    if (addMovieRequest.IsMultiLanguage == null)
                    {
                        addMovieRequest.IsMultiLanguage = false;
                    }
                    var genres = string.Join(",", addMovieRequest.GenreId);
                    //------------------------------------------- saving cover
                    var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-Slide", addMovieRequest.Cover, 2);
                    //------------------------------------------- saving cover

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
                    //------------------------------------------- saving poster
                    var poster = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-Slide", addMovieRequest.Poster, 3);
                    //------------------------------------------- saving poster

                    if (poster.Id == 0)
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
                    Movie MovieCreated = new Movie()
                    {
                        Alias = addMovieRequest.Alias,
                        LanguageId = addMovieRequest.LanguageId,
                        Title = addMovieRequest.Title,
                        Description = addMovieRequest.Description,
                        Rating = addMovieRequest.Rating,
                        ReleaseDate = addMovieRequest.ReleaseDate,
                        URL = addMovieRequest.URL,
                        Poster = poster.Address,
                        Cover = cover.Address,
                        CategoryId = addMovieRequest.CategoryId,
                        GenreId = genres,
                        ManufacturedBy = addMovieRequest.ManufacturedBy,
                        Language = addMovieRequest.Language,
                        HaveSubtitle = addMovieRequest.HaveSubtitle,
                        IsMultiLanguage = addMovieRequest.IsMultiLanguage,

                    };
                    _service.Movie.AddMovie(MovieCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم با موفقیت ثبت شد", Value = new { }, Error = new { } });
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
        [HttpPost("GetAllMovies")]
        public IActionResult GetAllMovies([FromBody] FilterRequest filter, [FromHeader] int pagesize, [FromHeader] int pageNumber, [FromHeader] string? language)
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

                List<ShowMovies> res = _service.Movie.GetAllMovies(pagesize, pageNumber, filter, languageId);
                var genres = _service.Genre.GetAll();
                foreach (var item in res)
                {
                    var names = new List<string>();
                    foreach (var el in item.GenreName)
                    {
                        var gen = genres.FirstOrDefault(w => w.Id == Convert.ToInt32(el));
                        names.Add(gen == null ? "" : gen.Name);
                    }
                    item.GenreName = new List<string>();
                    item.GenreName.AddRange(names);
                }
                List<KeyValuePair<string, List<ShowMovies>>> keyValuePair = new List<KeyValuePair<string, List<ShowMovies>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowMovies>>(item.Code, res.Where(w => w.LanguageCode == item.Code).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { Max = _service.Movie.GetAllMovies(languageId).Count, response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("BOGetAllMovies")]
        public IActionResult BOGetAllMovies([FromBody] FilterRequest filter, [FromHeader] int pagesize, [FromHeader] int pageNumber, [FromHeader] string? language)
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
                List<BOShowMovies> res = _service.Movie.BOGetAllMovies(pagesize, pageNumber, filter, languageId);
                var genres = _service.Genre.GetAll();
                foreach (var item in res)
                {
                    var names = new List<string>();
                    var Ids = new List<int>();
                    foreach (var el in item.GenreName)
                    {
                        names.Add(genres.FirstOrDefault(w => w.Id == Convert.ToInt32(el))?.Name);
                        Ids.Add(Convert.ToInt32(el));
                    }
                    item.GenreId = new List<int>();
                    item.GenreName = new List<string>();
                    item.GenreId.AddRange(Ids);
                    item.GenreName.AddRange(names);
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { Max = _service.Movie.BOGetAllMovies(languageId).Count, response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] UpdateMovieRequest movie)
        {
            try
            {
                if (movie.Id == 0 || movie.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه فیلم اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Movie MovieCreated = _service.Movie.GetMovie(movie.Id);
                    if (MovieCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "فیلمی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    if (!TextHelpers.IsDigitsOnly(movie.Rating.Replace(".", "")))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "امتیاز نامعتبر است", Value = new { }, Error = new { } });
                    }
                    if (!TextHelpers.IsDigitsOnly(movie.ReleaseDate.Replace("/", "")))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.FailedDependency, Message = "تاریخ نامعتبر است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(movie.Cover))
                    {
                        movie.Cover = MovieCreated.Cover;
                    }
                    if (string.IsNullOrEmpty(movie.URL))
                    {
                        movie.URL = MovieCreated.URL;
                    }
                    if (string.IsNullOrEmpty(movie.Title))
                    {
                        movie.Title = MovieCreated.Title;
                    }
                    if (string.IsNullOrEmpty(movie.Poster))
                    {
                        movie.Poster = MovieCreated.Poster;
                    }
                    if (movie.IsActive == null)
                    {
                        movie.IsActive = MovieCreated.IsActive;
                    }
                    if (string.IsNullOrEmpty(movie.Description))
                    {
                        movie.Description = MovieCreated.Description;
                    }
                    if (string.IsNullOrEmpty(movie.ReleaseDate))
                    {
                        movie.ReleaseDate = MovieCreated.ReleaseDate;
                    }

                    if (movie.CategoryId == null)
                    {
                        movie.CategoryId = MovieCreated.CategoryId;
                    }
                    if (string.IsNullOrEmpty(movie.Rating))
                    {
                        movie.Rating = MovieCreated.Rating;
                    }
                    //if (movie.GenreId == null)
                    //{
                    //    movie.GenreId = MovieCreated.GenreId.Split(",");
                    //}
                    if (movie.HaveSubtitle == null)
                    {
                        movie.HaveSubtitle = MovieCreated.HaveSubtitle;
                    }
                    if (string.IsNullOrEmpty(movie.ManufacturedBy))
                    {
                        movie.ManufacturedBy = MovieCreated.ManufacturedBy;
                    }
                    if (movie.Language == null)
                    {
                        movie.Language = MovieCreated.Language;
                    }
                    if (movie.IsMultiLanguage == null)
                    {
                        movie.IsMultiLanguage = MovieCreated.IsMultiLanguage;
                    }
                    if (movie.Cover != MovieCreated.Cover)
                    {
                        var res = _service.Picture.GetByAddress(MovieCreated.Cover);
                        if (res != null)
                        {
                            var coverName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\MovieCover\\" + coverName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\MovieCover\\" + coverName);
                            _service.Picture.DeleteById(res.Id);
                        }

                        //------------------------------------------- saving cover
                        var cover = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + movie.Title, movie.Cover, 2);
                        //------------------------------------------- saving cover
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
                        MovieCreated.Cover = cover.Address;
                    }
                    if (movie.Poster != MovieCreated.Poster)
                    {
                        var res = _service.Picture.GetByAddress(MovieCreated.Poster);
                        if (res != null)
                        {
                            var coverName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\MoviePoster\\" + coverName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\MoviePoster\\" + coverName);
                            _service.Picture.DeleteById(res.Id);
                        }

                        //------------------------------------------- saving cover
                        var poster = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + movie.Title, movie.Poster, 3);
                        //------------------------------------------- saving cover
                        if (poster.Id == 0)
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
                        MovieCreated.Poster = poster.Address;
                    }
                    var genres = string.Join(",", movie.GenreId);
                    MovieCreated.Title = movie.Title;
                    MovieCreated.URL = movie.URL;
                    MovieCreated.Description = movie.Description;
                    MovieCreated.IsActive = movie.IsActive;
                    MovieCreated.Rating = movie.Rating;
                    MovieCreated.ReleaseDate = movie.ReleaseDate;
                    MovieCreated.CategoryId = movie.CategoryId;
                    MovieCreated.Language = movie.Language;
                    MovieCreated.GenreId = genres;
                    MovieCreated.HaveSubtitle = movie.HaveSubtitle;
                    MovieCreated.ManufacturedBy = movie.ManufacturedBy;
                    MovieCreated.IsMultiLanguage = movie.IsMultiLanguage;
                    MovieCreated.CreationDateTime = MovieCreated.CreationDateTime;
                    _service.Movie.UpdateMovie(MovieCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteMovieRequest movie)
        {
            try
            {
                if (movie.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات وارد شده نامعتبر است", Value = new { }, Error = new { } });
                }
                else
                {
                    Movie movieCreated = _service.Movie.GetMovie(movie.Id);
                    if (movieCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "فیلمی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }

                    if (movieCreated.LanguageId == _service.Language.GetByCode(_appSettings.DefaultLanguage).Id)
                    {
                        if (_service.Movie.BOGetMovieByAlias(movieCreated.Alias, 0).Where(w => w.LanguageCode != _appSettings.DefaultLanguage).Count() > 0)
                            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "امکان حذف زبان پیش فرض وجود ندارد", Value = new { }, Error = new { } });
                    }
                    movieCreated.IsDelete = true;
                    _service.Movie.UpdateMovie(movieCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم با موفقیت حذف شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("BOGetMovieImage")]
        public IActionResult BOGetMovieImage([FromHeader] int id)
        {
            try
            {
                BOShowMoviesImages res = _service.Movie.ShowMovieImages(id);
                if (res == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه فیلم اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "عکس ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetMovieByAlias")]
        public IActionResult GetMovieByAlias([FromHeader] string alias, [FromHeader] string? language)
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

                List<ShowMovies> res = _service.Movie.GetMovieByAlias(alias, languageId);
                foreach (var item in res)
                {
                    var names = new List<string>();
                    foreach (var el in item.GenreName)
                    {
                        var gen = _service.Genre.GetNameById(Convert.ToInt32(el));
                        if (!string.IsNullOrEmpty(gen)) names.Add(gen);
                    }
                    item.GenreName = new List<string>();
                    item.GenreName.AddRange(names);
                }
                List<KeyValuePair<string, ShowMovies>> keyValuePair = new List<KeyValuePair<string, ShowMovies>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, ShowMovies>(item.Code, res.Where(w => w.LanguageCode == item.Code)?.FirstOrDefault()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpGet("BOGetMovieByAlias")]
        public IActionResult BOGetMovieByAlias([FromHeader] string alias, [FromHeader] string? language)
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

                List<BOShowMovies> res = _service.Movie.BOGetMovieByAlias(alias, languageId);
                foreach (var item in res)
                {
                    var names = new List<string>();
                    var Ids = new List<int>();
                    foreach (var el in item.GenreName)
                    {
                        names.Add(_service.Genre.GetNameById(Convert.ToInt32(el)));
                        Ids.Add(Convert.ToInt32(el));
                    }
                    item.GenreId = new List<int>();
                    item.GenreName = new List<string>();
                    item.GenreId.AddRange(Ids);
                    item.GenreName.AddRange(names);
                }
                //List<KeyValuePair<string, ShowMovies>> keyValuePair = new List<KeyValuePair<string, ShowMovies>>();
                //var languages = _service.Language.GetLanguages();
                //foreach (var item in languages)
                //{
                //    keyValuePair.Add(new KeyValuePair<string, ShowMovies>(item.Code, res.Where(w => w.LanguageCode == item.Code).FirstOrDefault()));
                //}
                //return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetEventsByLang")]
        public IActionResult GetEventsByLang([FromHeader] string? language)
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
                int id = 0;
                if (languageId == 1)
                {
                    id = _appSettings.EventIR;
                }
                if (languageId == 2)
                {
                    id = _appSettings.EventGB;
                }
                if (languageId == 3)
                {
                    id = _appSettings.EventDE;
                }
                if (languageId == 4)
                {
                    id = _appSettings.EventSA;
                }
                if (languageId == 5)
                {
                    id = _appSettings.EventFR;
                }
                List<ShowMovies> res = _service.Movie.GetMovieByCategory(id);
                foreach (var item in res)
                {
                    var names = new List<string>();
                    foreach (var el in item.GenreName)
                    {
                        names.Add(_service.Genre.GetNameById(Convert.ToInt32(el)));
                    }
                    item.GenreName = new List<string>();
                    item.GenreName.AddRange(names);
                }
                List<KeyValuePair<string, List<ShowMovies>>> keyValuePair = new List<KeyValuePair<string, List<ShowMovies>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowMovies>>(item.Code, res.Where(w => w.LanguageCode == item.Code).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "رویداد ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetMoviesByTitle")]
        public IActionResult GetMoviesByTitle([FromHeader] string? language, [FromBody] MoviesByTitle? title)
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
                var res = _service.Movie.GetMoviesByTitle(title.Title, languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "فیلم ها با موفقیت ارسال شد", Value = new { Max = res.Count(), response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
