using System;
using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class PhotoEntity
    {
        public PhotoEntity()
        {
            Tags = new HashSet<TagEntity>();
            Likes = new HashSet<LikeEntity>();
        }

        public String Description { get; set; }
        public int Id { get; set; }
        public int ProfileId { get; set; }

        public DateTime CreatedOn { get; set; }
        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<LikeEntity> Likes { get; set; }
        public string UserName { get; set; }
    }
}