using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class RoleEntity
    {
        public RoleEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}