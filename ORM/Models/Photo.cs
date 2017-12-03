using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Photo
    {
        public Photo()
        {
            Tags = new HashSet<Tag>();
            Likes = new HashSet<Like>();
        }

        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public String Description { get; set; }

        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public string UserName { get; set; }
    }
}