using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.DTO;
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

        public IEnumerable<MessageDTO> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Message, MessageDTO>());
            var messages = _unitOfWork.Messages.GetAll().ToList();
            return Mapper.Map<List<Message>, List<MessageDTO>>(messages);
        }

        public MessageDTO GetById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Message, MessageDTO>());
            return Mapper.Map<Message, MessageDTO>(_unitOfWork.Messages.GetById(id));
        }

        public MessageDTO Create(MessageDTO messageDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, Message>());
            var message = Mapper.Map<MessageDTO, Message>(messageDto);
            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();

            return null;
        }

        public void SendMessage(MessageDTO messageDto, int fromId, int toId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, Message>());
            var message = Mapper.Map<MessageDTO, Message>(messageDto);
            message.ApplicationUser = _unitOfWork.UserManager.FindById(fromId);
            var userTo = _unitOfWork.UserManager.FindById(toId);
            message.Receiver = userTo;

            _unitOfWork.Messages.Create(message);
            _unitOfWork.Save();
        }

        public IEnumerable<MessageDTO> GetUserMessages(int id)
        {
            var messages = _unitOfWork.Messages.Query.Where(x => x.ApplicationUserId == id || x.Receiver.Id == id)
                .OrderByDescending(m => m.Date).ToList();
            markMessagesAsReaded(id);

            Mapper.Initialize(cfg => cfg.CreateMap<Message, MessageDTO>());
            return Mapper.Map<List<Message>, List<MessageDTO>>(messages);
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

        public MessageDTO Update(int id, MessageDTO newMessage)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                return null;

            message.Text = newMessage.Text;
            _unitOfWork.Save();
            Mapper.Initialize(cfg => cfg.CreateMap<Message, MessageDTO>());
            return Mapper.Map<Message, MessageDTO>(message);
        }

        public void Remove(int id)
        {
            _unitOfWork.Messages.Delete(id);
            _unitOfWork.Save();
        }
    }
}
