using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
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
            var messages = _messageService.GetAll();
            return View(messages);
        }

        public ActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Message message, int id)
        {
            var ownerId = User.Identity.GetUserId<int>();
            _messageService.SendMessage(message, ownerId, id);
            return RedirectToAction("GetUserMessages", new { id = ownerId});
        }

        public ActionResult Remove(int id)
        {
            _messageService.Remove(id);
            return RedirectToAction("Index", "Message");
        }

        public ActionResult Edit(Message message)
        {
            _messageService.Update(message.Id, message);
            return RedirectToAction("Index", "Message");
        }

        public ActionResult HasUnreadedMessages(int id)
        {
            var countMessages = _messageService.CountUnreadedMessages(id);

            return Json(new { countMessages },
                 JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserMessages(int id)
        {
            var messages = _messageService.GetUserMessages(id);
            return View(messages);
        }

    }
}