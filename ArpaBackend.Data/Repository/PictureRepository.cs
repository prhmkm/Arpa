using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.DTOs;
using ArpaBackend.Domain.Models;
using static ArpaBackend.Domain.DTOs.PictureDTO;

namespace ArpaBackend.Data.Repository
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public PictureRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }


        public long Add(Picture picture)
        {
            Create(picture);
            Save();
            return picture.Id;
        }

        public void DeleteById(long id)
        {
            _repositoryContext.Remove(_repositoryContext.Pictures.FirstOrDefault(a => a.Id == id));
            _repositoryContext.SaveChanges();
        }
        public List<Picture> FindByFolderId(long id)
        {
            return null;
        }

        public PictureResponse FindById(long? id)
        {
            //PictureResponse picture = new PictureResponse
            //{
            //    Address = "",
            //    FolderId = 0,
            //    Id = 0,
            //    ImageName = "",
            //    Thumbnail = ""
            //};
            //var q = (from a in _repositoryContext.Pictures
            //         join b in _repositoryContext.Folders on a.FolderId equals b.Id
            //         where a.Id == id
            //         select new PictureResponse
            //         {
            //             Id = a.Id,
            //             Address = a.Address,
            //             FolderId = a.FolderId,
            //             ImageName = a.ImageName,
            //             Thumbnail = a.Thumbnail
            //         }).FirstOrDefault();

                //if (q != null)
                //{
                //    return q;
                //}
                //else
                //{
                //    return picture;
                //}
            return null;
        }

        public Picture GetByAddress(string address)
        {
            return FindByCondition(w => w.Address == address).FirstOrDefault();
        }
    }
}
