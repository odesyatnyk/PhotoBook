using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.User
{
    public class UserViewModel
    {
        [Display(Name = "User's e-mail")]
        public string Email { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}