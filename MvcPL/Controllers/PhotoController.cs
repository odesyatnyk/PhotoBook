using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;

namespace MvcPL.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoService _service;

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        public ActionResult Show(string photoId)
        {
            var photo = _service.GetById(Convert.ToInt32(photoId));
            return PartialView("ImageContent", photo.ToMvcPhoto());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePhoto(string name, int photoId)
        {
            var photo = _service.GetById(photoId);
            _service.Delete(photo);
            return RedirectToAction("Index", "Photo", new { name });
        }

        [HttpPost]
        public ActionResult LinksView(int page, string pageName)
        {
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                CurrentPage = page,
                PageName = pageName,
            };
            if (pageName.StartsWith("Profile"))
            {
                string username = pageName.Substring(7);
                photos.Content =
                    _service.GetAllByPredicate(p => p.UserName == username).Select(photo => photo.ToMvcPhoto()).ToList();
                //return RedirectToAction("UserPageLinks", "Profile", new { username, page });
            }
            else if (pageName.StartsWith("Tag"))
            {
                string tag = pageName.Substring(3);
                photos.Content =
                    _service.GetAllEntities().Where(p => p.Tags.Select(t => t.Name).Contains(tag))
                        .Select(photo => photo.ToMvcPhoto()).ToList();
                //return RedirectToAction("TagSearchLinks", "Tag", new {name = tag, page});
            }
            else
            {
                //(pageName.StartsWith("Index"))
                string filter = pageName.Substring(5);
                photos.Content = _service.GetAllByPredicate(x => x.Description.Contains(filter))
                    .Select(photo => photo.ToMvcPhoto())
                    .ToList();
            }
            photos.Count = photos.Content.Count;
            photos.Content = photos.Content
                .Skip((photos.CurrentPage - 1) * GalleryHelper.PageSize)
                .Take(GalleryHelper.PageSize)
                .ToList();
            return PartialView("Links", photos);
        }

        public ActionResult Index(string filter = null)
        {
            ViewBag.filter = filter;
            var content = _service.GetAllByPredicate(x => filter == null || x.Description.Contains(filter)).OrderByDescending(x => x.CreatedOn).ToList();
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                Count = content.Count,
                CurrentPage = 1,
                Content = content.Take(GalleryHelper.PageSize).Select(photo => photo.ToMvcPhoto()).ToList(),
                PageName = "Index" + filter
            };
            return View(photos);
        }

        public ActionResult Like(int photoId, string name)
        {
            var photo = _service.GetById(photoId);
            if (photo.Likes.FirstOrDefault(l => l.UserName == User.Identity.Name) != null)
            {
                _service.RemoveLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            else
            {
                _service.AddLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            photo = _service.GetById(photoId);
            return PartialView("Like", photo.ToMvcPhoto());
        }
    }
}