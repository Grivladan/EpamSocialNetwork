using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Infrastructure;
using SocialNetwork.Logic.Interfaces;
using SocialNetwork.WebHost.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult GetPostsByUser(int id)
        {
            var postsDto = _postService.GetPostsByUser(id);
            Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>());
            var postsViewModel = Mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostViewModel>>(postsDto);
            return PartialView(postsViewModel);
        }

        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PostDTO postDto = new PostDTO()
                    {
                        Text = formCollection["Post"],
                        ApplicationUserId = User.Identity.GetUserId<int>()
                    };
                    _postService.Create(postDto);
                }
                return RedirectToAction("GetUserById", "Home");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult LikePost(int postId)
        {
            try
            {
                var postDto = _postService.GetById(postId);
                LikeDTO likeDto = new LikeDTO
                {
                    OwnerId = User.Identity.GetUserId<int>(),
                    PostId = postId
                };
                _postService.LikePost(likeDto);

                var postDtoUpdate = _postService.GetById(postId);
                Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>());
                var postViewModel = Mapper.Map<PostDTO, PostViewModel>(postDtoUpdate);
                return PartialView("_LikeButton", postViewModel);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult GetFriendsPosts(int id)
        {
            try
            {
                var postsDto = _postService.GetFriendsPosts(id);
                Mapper.Initialize(cfg => cfg.CreateMap<PostDTO, PostViewModel>());
                var postsViewModel = Mapper.Map<IEnumerable<PostDTO>, IEnumerable<PostViewModel>>(postsDto);
                return PartialView("GetPostsByUser", postsViewModel);
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}