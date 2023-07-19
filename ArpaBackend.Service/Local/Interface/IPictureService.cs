

using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.PictureDTO;

namespace ArpaBackend.Service.Local.Interface
{
    public interface IPictureService
    {
        PictureResponse FindById(long? id);
        UploadPic Upload(string objectId, string picture, int? id);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        public Picture GetByAddress(string address);
    }
}
