using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;
using MvcPL.Models.Profile;

namespace MvcPL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [Route("user/{name}")]
        public ActionResult UserPage(string name)
        {
            var profile = _service.GetProfileByName(name);
            if (profile == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var records = new PagedList<PhotoViewModel>
            {
                Content = new List<PhotoViewModel>(profile.Photos.Take(GalleryHelper.PageSize).Select(photo => photo.ToMvcPhoto())).OrderByDescending(x => x.CreatedOn).ToList(),
                CurrentPage = 1,
                Count = profile.Photos.Count,
                PageName = "Profile" + name
            };
            var model = new UserPageModel
            {
                Photos = records,
                Profile = profile.ToMvcProfile()
            };
            return View(model);
        }

        //public ActionResult UserPageLinks(int page, string username)
        //{
        //    var profile = _service.GetProfileByName(username);
        //    if (profile == null)
        //    {
        //        return RedirectToAction("NotFound", "Error");
        //    }
        //    var photos = new PagedList<PhotoViewModel>
        //    {
        //        Content = new List<PhotoViewModel>(profile.Photos.Take(GalleryHelper.PageSize).Select(photo => photo.ToMvcPhoto())),
        //        CurrentPage = page,
        //        PageName = "Profile" + username
        //    };
        //    return PartialView("Links", photos);
        //}

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfileEdit(ProfileEditModel viewModel, HttpPostedFileBase file)
        {
            var profile = _service.GetProfileByName(User.Identity.Name);
            if (file != null && file.ContentLength > 0)
            {
                var target = new MemoryStream();
                file.InputStream.CopyTo(target);
                var byteArrayIn = target.ToArray();
                Image image = null;
                using (var ms = new MemoryStream(byteArrayIn))
                {
                    image = Image.FromStream(ms);
                }
                profile.Avatar = GalleryHelper.ImageToByteArray(image);
            }

            profile.FirstName = viewModel.FirstName ?? profile.FirstName;
            profile.LastName = viewModel.LastName ?? profile.LastName;
            profile.Age = viewModel.Age == 0 ? profile.Age : viewModel.Age;

            _service.Update(profile);
            return RedirectToAction("UserPage", new { name = profile.UserName });
        }

        [HttpGet]
        public ActionResult Create()
        {
            var photo = new CreatePhotoModel();
            return View(photo);
        }

        [HttpPost]
        public ActionResult Create(CreatePhotoModel photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(photo);

            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!httpPostedFileBases.Any() || httpPostedFileBases.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            if (photo.Tags != null)
                photo.Tags = photo.Tags.Trim();

            var model = new PhotoViewModel();
            var profile = _service.GetProfileByName(User.Identity.Name);

            foreach (var file in httpPostedFileBases)
            {
                if (file.ContentLength == 0) continue;

                using (var img = Image.FromStream(file.InputStream))
                {
                    var cutImage = GalleryHelper.CutImage(img, 300, 300);
                    model.Picture = GalleryHelper.ImageToByteArray(cutImage);
                    model.FullSize = GalleryHelper.ImageToByteArray(img);
                }
                // Save record to database
                model.CreatedOn = DateTime.Now;
                model.Description = photo.Description;
                model.UserName = User.Identity.Name;

                var tags = photo.Tags != null ? photo.Tags.Split(' ') : new string[0];

                model.Tags = new List<TagModel>();
                foreach (var tag in tags)
                {
                    model.Tags.Add(new TagModel
                    {
                        Name = tag
                    });
                }

                profile.Photos.Add(model.ToPhotoEntity());
            }
            _service.Update(profile);
            return RedirectToAction("UserPage", new { name = profile.UserName });
        }


    }
}