using ArpaBackend.Core.Helpers;
using ArpaBackend.Core.Model.Base;
using ArpaBackend.Data.Base;
using ArpaBackend.Domain.Models;
using ArpaBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ArpaBackend.Domain.DTOs.PictureDTO;

namespace ArpaBackend.Service.Local.Service
{
    public class PictureService : IPictureService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public PictureService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public void DeleteById(long id)
        {
            _repository.Picture.DeleteById(id);
        }

        public List<Picture> FindByFolderId(long id)
        {
            return _repository.Picture.FindByFolderId(id);
        }
        public PictureResponse FindById(long? id)
        {
            return _repository.Picture.FindById(id);
        }

        public UploadPic Upload(string objectId, string picture, int? id)
        {
            List<string> _imagName = objectId.Split(".").ToList();
            string imgName = null;
            for (int i = 0; i < _imagName.Count - 1; i++)
            {
                imgName = imgName + _imagName[i] + ".";
            }
            string path = Path.Combine(_appSettings.SaveImagePath + "\\");
            if (id == 1)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\Slides\\");
            }
            if (id == 2)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\MovieCover\\");
            }
            if (id == 3)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\MoviePoster\\");
            }
            if (id == 4)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\FestivalVideo\\");
            }
            if (id == 5)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\FestivalCover\\");
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string address = null;
            string imageName = null;
            string imageNameThumb = null;

            imageName = Convertor.Base64ToImage(picture, path, objectId.Split(".")[0]);
            address = _appSettings.PublishImagePath + "//" + imageName;
            if (id == 1)
            {
                address = _appSettings.PublishImagePath + "//Slides//" + imageName;
            }
            if (id == 2)
            {
                address = _appSettings.PublishImagePath + "//MovieCover//" + imageName;
            }
            if (id == 3)
            {
                address = _appSettings.PublishImagePath + "//MoviePoster//" + imageName;
            }
            if (id == 4)
            {
                address = _appSettings.PublishImagePath + "//FestivalVideo//" + imageName;
            }
            if (id == 5)
            {
                address = _appSettings.PublishImagePath + "//FestivalCover//" + imageName;
            }
            
            if (imageName == "crash" || imageNameThumb == "crash" || imageName == "InvalidFormat")
            {
                return new UploadPic { Address = null, Id = 0 };
            }
            return new UploadPic { Address = address, Id = _repository.Picture.Add(new Picture { Address = address, ImageName = imageName }) };
        }

        public Picture GetByAddress(string address)
        {
            return _repository.Picture.GetByAddress(address);
        }
    }
}
