using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepos
{
    public interface IPhotoRepository : IRepository<DalPhoto>
    {
        void RemoveLike(DalLike like);
        void AddLike(DalLike like);
    }
}