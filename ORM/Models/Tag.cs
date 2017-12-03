using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    public class Tag
    {
        public Tag()
        {
            Photos = new List<Photo>();
        }

        [Key]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}