using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepos
{
    public interface ITagRepository : IRepository<DalTag>
    {
        DalTag GetTagByName(string name);
    }
}
