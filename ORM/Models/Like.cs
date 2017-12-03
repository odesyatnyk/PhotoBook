using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public string UserName { get; set; }
        public int PhotoId { get; set; }
    }
}