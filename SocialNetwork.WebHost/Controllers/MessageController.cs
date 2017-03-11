using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Interfaces;
using SocialNetwork.WebHost.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SocialNetwork.WebHost.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
       
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        public ActionResult Index()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>());
            var messageDtos = Mapper.Map<IEnumerable<MessageDTO>, List<MessageViewModel>>(_messageService.GetAll());
            return View(messageDtos);
        }

        public ActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MessageViewModel messageViewModel, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MessageViewModel, MessageDTO>());
                    var messageDto = Mapper.Map<MessageViewModel, MessageDTO>(messageViewModel);
                    var ownerId = User.Identity.GetUserId<int>();
                    _messageService.SendMessage(messageDto, ownerId, id);
                    return RedirectToAction("GetUserMessages", new { id = ownerId });
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View("Create", messageViewModel);
        }

        public ActionResult Remove(int id)
        {
            try
            {
                _messageService.Remove(id);
                return RedirectToAction("Index", "Message");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Edit(MessageViewModel messageViewModel)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<MessageViewModel, MessageDTO>());
                var messageDto = Mapper.Map<MessageViewModel, MessageDTO>(messageViewModel);
                _messageService.Update(messageDto.Id, messageDto);
                return RedirectToAction("Index", "Message");
            }
            catch (Exception)
            {
                return View("Create", messageViewModel);
            }
        }

        public ActionResult HasUnreadedMessages(int id)
        {
            var countMessages = _messageService.CountUnreadedMessages(id);

            return Json(new { countMessages },
                 JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserMessages(int id)
        {
            var messagesDto = _messageService.GetUserMessages(id);
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, MessageViewModel>());
            var messagesViewModel = Mapper.Map<IEnumerable<MessageDTO>, IEnumerable<MessageViewModel>>(messagesDto);
            return View(messagesViewModel);
        }

    }
}