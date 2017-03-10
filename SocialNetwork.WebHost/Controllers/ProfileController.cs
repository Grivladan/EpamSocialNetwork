using SocialNetwork.DataAccess.EF;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Logic.Interfaces;
using SocialNetwork.Logic.DTO;
using AutoMapper;
using SocialNetwork.WebHost.ViewModel;
using System;

namespace SocialNetwork.WebHost.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        // GET: Profile
        public ActionResult Edit(int id= 0)
        {
            try
            {
                ProfileDTO profileDto = _profileService.GetById(id);
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>());
                var profileViewModel = Mapper.Map<ProfileDTO, ProfileViewModel>(profileDto);
                return View(profileViewModel);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "UserPhoto")]ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProfileViewModel, ProfileDTO>());
                var profileDto = Mapper.Map<ProfileViewModel, ProfileDTO>(profileViewModel);

                byte[] imageData = null;

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"] ;
                    if (poImgFile != null && poImgFile.ContentLength > 0)
                    {
                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            imageData = binary.ReadBytes(poImgFile.ContentLength);
                        }
                    }
                }

                if (imageData != null)
                {
                    profileDto.UserPhoto = imageData;
                }
                else
                {
                    int userId = User.Identity.GetUserId<int>();
                    var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    var userProfile = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault().Profile;
                    profileDto.UserPhoto = userProfile.UserPhoto;
                }
                try
                {
                    _profileService.Update(profileDto);
                    return RedirectToAction("GetUserById", "Home");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return View(profileViewModel);
        }

        public FileContentResult UserPhotos(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = id ?? User.Identity.GetUserId<int>();

                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userProfile = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault().Profile;

                return new FileContentResult(userProfile.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        imageData = br.ReadBytes((int)imageFileLength);
                    }
                }
                return File(imageData, "image/png");
            }
        }
    }
}