using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser, int> _manager;


        public MessageService(IUnitOfWork unitOfWork, UserManager<ApplicationUser, int> manager)
        {
            _unitOfWork = unitOfWork;
            _manager = manager;
        }

        public IEnumerable<Message> GetAll()
        {
            var messages = _unitOfWork.Messages.GetAll().ToList();
            return messages;
        }

        public Message GetById(int id)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if(message == null)
                return null;

            return message;
        }

        public Message Create(Message message)
        {
            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();

            return null
        }

        public void SendMessage(Message message, int id)
        {
            message.ApplicationUser = manager.FindById(User.Identity.GetUserId<int>());
            var userTo = manager.FindById(id);
            message.Receiver = userTo;

            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();
        }

        public DataAccess.Entities.Message Update(int id, DataAccess.Entities.Message message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
