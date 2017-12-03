using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepos
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByEmail(string email);
        DalUser GetByName(string name);
    }
}