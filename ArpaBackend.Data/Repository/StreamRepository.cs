using ArpaBackend.Data.Base;
using ArpaBackend.Data.Interface;
using ArpaBackend.Domain.Models;

namespace ArpaBackend.Data.Repository
{
    public class StreamRepository : BaseRepository<OnlineStream>, IStreamRepository
    {
        ArpaWebsite_DBContext _repositoryContext;
        public StreamRepository(ArpaWebsite_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddStream(OnlineStream stream)
        {
            Create(stream);
            Save();
        }

        public List<OnlineStream> BOGetAllStreams()
        {
            return FindAll().Where(s => s.IsDelete == false).ToList();
        }

        public List<OnlineStream> GetAllStreams()
        {
            return FindAll().Where(s => s.IsActive == true && s.IsDelete == false).ToList();
        }

        public OnlineStream GetStreamById(int Id)
        {
            return FindByCondition(w => w.Id == Id).FirstOrDefault();
        }

        public void UpdateStream(OnlineStream stream)
        {
            Update(stream);
            Save();
        }
    }
}
