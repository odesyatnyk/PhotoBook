using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IPhotoService : IService<PhotoEntity>
    {
        void RemoveLike(LikeEntity likeEntity);
        void AddLike(LikeEntity likeEntity);
    }
}