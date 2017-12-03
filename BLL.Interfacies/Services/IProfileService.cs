using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IProfileService : IService<ProfileEntity>
    {
        ProfileEntity GetProfileByName(string name);
    }
}