using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;

namespace MvcPL.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [Route("tag/{name}")]
        public ActionResult TagSearch(string name)
        {
            var tag = _service.GetTagByName(name);
            if (tag == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                Content = new List<PhotoViewModel>(tag.Photos.Take(GalleryHelper.PageSize).Select(p => p.ToMvcPhoto())),
                CurrentPage = 1,
                Count = tag.Photos.Count,
                PageName = "Tag" + name
            };

            return View(photos);
        }

        //public ActionResult TagSearchLinks(string name, int page)
        //{
        //    var tag = _service.GetTagByName(name);
        //    if (tag == null)
        //    {
        //        return RedirectToAction("NotFound", "Error");
        //    }
        //    PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
        //    {
        //        CurrentPage = page,
        //        PageName = "Tag" + name,
        //        Content = new List<PhotoViewModel>(tag.Photos.Select(p => p.ToMvcPhoto()))
        //    };
        //    return PartialView("Links", photos);
        //}
	}
}