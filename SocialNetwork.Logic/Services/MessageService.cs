using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            message.ApplicationUser = _unitOfWork.UserManager.FindById(fromId);
            var userTo = _unitOfWork.UserManager.FindById(toId);
            message.Receiver = userTo;

            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();
        }

        public IEnumerable<Message> GetUserMessages(int id)
        {
            var messages = _unitOfWork.Messages.Query.Where(x => x.ApplicationUserId == id || x.Receiver.Id == id)
                .OrderByDescending(m => m.Date).ToList();
            markMessagesAsReaded(id);

            return messages;
        }

        private void markMessagesAsReaded(int id)
        {
            var messages = _unitOfWork.Messages.Query.Where(x => x.Receiver.Id == id && x.isReaded == false).ToList();
            foreach (var message in messages)
            {
                message.isReaded = true;
                _unitOfWork.Messages.Update(message);
            }
            _unitOfWork.Save();
        }

        public int CountUnreadedMessages(int id)
        {
            var countMessages = _unitOfWork.Messages.Query.Where(x => x.Receiver.Id == id && x.isReaded == false).ToList().Count;
            
            return countMessages;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Message Update(int id, Message newMessage)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                return null;

            message.Text = newMessage.Text;
            _unitOfWork.Save();
            return message;
        }

        public void Remove(int id)
        {
            _unitOfWork.Messages.Delete(id);
            _unitOfWork.Save();
        }
    }
}
