using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Models.User;

namespace MvcPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _service;

        public HomeController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index(string filter = null)
        {
            return RedirectToAction("Index", "Photo", filter);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UsersEdit()
        {
            var model = _service.GetAllEntities().Select(u => new UserViewModel
            {
                Email = u.Email,
                Name = u.UserName
            });

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string name)
        {
            var user = _service.GetUserByName(name);
            _service.Delete(user);
            return RedirectToAction("UsersEdit");
        }

    }
}