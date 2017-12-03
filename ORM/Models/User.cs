using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(60), MinLength(4)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; } //MD5 hash

        [Required]
        [MaxLength(60), MinLength(5)]
        public string Email { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [ForeignKey("UserProfileId")]
        public virtual Profile UserProfile { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}