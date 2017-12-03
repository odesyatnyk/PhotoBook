using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface ITagService : IService<TagEntity>
    {
        TagEntity GetTagByName(string name);
    }
}
