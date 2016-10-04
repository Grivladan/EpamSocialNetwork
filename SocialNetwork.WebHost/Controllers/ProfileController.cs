using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SocialNetwork.WebHost.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Profile
        public ActionResult Edit(int id = 0)
        {
            Profile profile = context.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "UserPhoto")]Profile profile)
        {
            if (ModelState.IsValid)
            {
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
                    profile.UserPhoto = imageData;
                }
                else
                {
                    int userId = User.Identity.GetUserId<int>();
                    var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    var userProfile = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault().Profile;
                    profile.UserPhoto = userProfile.UserPhoto;
                }

                context.Entry(profile).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("GetUserById", "Home");
            }
            return View(profile);
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
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}