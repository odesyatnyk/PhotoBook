using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalTag : IEntity
    {
        public DalTag()
        {
            Photos = new List<DalPhoto>();
        }

        public string Name { get; set; }
        public virtual ICollection<DalPhoto> Photos { get; set; }
        public int Id { get; set; }
    }
}