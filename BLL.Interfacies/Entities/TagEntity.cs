using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class TagEntity
    {
        public TagEntity()
        {
            Photos = new List<PhotoEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PhotoEntity> Photos { get; set; }
    }
}