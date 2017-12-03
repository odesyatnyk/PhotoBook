using System;
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalPhoto : IEntity
    {
        public DalPhoto()
        {
            Tags = new HashSet<DalTag>();
            Likes = new HashSet<DalLike>();
        }

        public int ProfileId { get; set; }
        public String Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        public virtual ICollection<DalTag> Tags { get; set; }
        public virtual ICollection<DalLike> Likes { get; set; }

        public int Id { get; set; }
        public string UserName { get; set; }
    }
}