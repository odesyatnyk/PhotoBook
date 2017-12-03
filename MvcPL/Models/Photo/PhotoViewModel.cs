using System;
using System.Collections.Generic;

namespace MvcPL.Models.Photo
{
    public class PhotoViewModel
    {
        public PhotoViewModel()
        {
            CreatedOn = DateTime.Now;
            Likes = new List<LikeModel>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual List<LikeModel> Likes { get; set; }
    }
}