
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.PictureDTO;

namespace ArpaBackend.Data.Interface
{
    public interface IPictureRepository
    {
        PictureResponse FindById(long? id);
        long Add(Picture picture);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        public Picture GetByAddress(string address);
    }
}
