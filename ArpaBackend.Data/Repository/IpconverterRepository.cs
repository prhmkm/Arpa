using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Data.Repository
{
    public class IpconverterRepository : BaseRepository<Country>, IIpconverterRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        private readonly AppSettings _appSettings;
        public IpconverterRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public string CountryNameToCountryCode(string countryname, string defaultLang)
        {
            var country = _repositoryContext.Countries.FirstOrDefault(w => w.CountryName == countryname);
            if (country == null)
            {
                return defaultLang;
            }
            return _repositoryContext.Languages.FirstOrDefault(w => w.Id == country.LanguageId).Code;
        }
    }
}
