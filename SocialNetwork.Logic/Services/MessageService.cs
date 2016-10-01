using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.DataAccess.EF;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser, int> _manager;


        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _manager = new UserManager<ApplicationUser, int>(new CustomUserStore(_unitOfWork.GetContext()));
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

            return null;
        }

        public void SendMessage(Message message, int fromId, int toId)
        {
            message.ApplicationUser = _manager.FindById(fromId);
            var userTo = _manager.FindById(toId);
            message.Receiver = userTo;

            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();
        }

        public IEnumerable<Message> GetUserMessages(int id)
        {
            var messages = _unitOfWork.Messages.Query.Where(x => x.ApplicationUserId == id || x.Receiver.Id == id);
            return messages;
        }

        public Message Update(int id, Message message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
