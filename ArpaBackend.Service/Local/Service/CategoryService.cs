using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ArpaBackend.Service.Local.Service
{
    public class CategoryService : ICategoryService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public CategoryService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public List<Category> GetCategories(int id)
        {
            return _repository.Category.GetCategories(id);
        }
    }
}
