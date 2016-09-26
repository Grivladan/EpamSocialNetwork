using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.Logic.Interfaces;
using SocialNetwork.WebHost.Hubs;
using System.Linq;
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
        // GET: Message
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
            _messageService.SendMessage(message, id);
            SendMessage("You get new message");
            return RedirectToAction("Index");
        }

        public ActionResult GetUserMessages(int id)
        {
            var messages = _messageService.GetUserMessages(id);
            return View("Index", messages);
        }

        private void SendMessage(string message)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayMessage(message);
        }
    }
}