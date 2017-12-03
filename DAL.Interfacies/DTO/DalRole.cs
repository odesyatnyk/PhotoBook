using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalRole : IEntity
    {
        public DalRole()
        {
            Users = new HashSet<DalUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<DalUser> Users { get; set; }
        public int Id { get; set; }
    }
}