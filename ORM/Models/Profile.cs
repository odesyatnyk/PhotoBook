using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    public class Profile
    {
        public Profile()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60), MinLength(4)]
        public string UserName { get; set; }

        [MaxLength(50), MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(50), MinLength(4)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public byte[] Avatar { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}