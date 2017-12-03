using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepos
{
    public interface IProfileRepository : IRepository<DalProfile>
    {
        DalProfile GetProfileByName(string name);
    }
}