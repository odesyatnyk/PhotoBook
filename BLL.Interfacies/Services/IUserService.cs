using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByName(string name);
    }
}