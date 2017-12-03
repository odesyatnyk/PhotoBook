using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IRoleService : IService<RoleEntity>
    {
        RoleEntity GetRoleByName(string name);
    }
}