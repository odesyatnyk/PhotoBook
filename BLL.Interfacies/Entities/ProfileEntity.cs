using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class ProfileEntity
    {
        public ProfileEntity()
        {
            Photos = new HashSet<PhotoEntity>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public byte[] Avatar { get; set; }

        public virtual ICollection<PhotoEntity> Photos { get; set; }
    }
}