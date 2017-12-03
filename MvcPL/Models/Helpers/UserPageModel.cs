using MvcPL.Models.Photo;
using MvcPL.Models.Profile;

namespace MvcPL.Models.Helpers
{
    public class UserPageModel
    {
        public ProfileViewModel Profile { get; set; }
        public PagedList<PhotoViewModel> Photos { get; set; }
    }
}