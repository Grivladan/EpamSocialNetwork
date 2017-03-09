using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNetwork.DataAccess.Entities;
using SocialNetwork.DataAccess.Interfaces;
using SocialNetwork.Logic.DTO;
using SocialNetwork.Logic.Infrastructure;
using SocialNetwork.Logic.Interfaces;
using System;
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
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                throw new ValidationException("Message doesn't exist", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Message, MessageDTO>());
            return Mapper.Map<Message, MessageDTO>(message);
        }

        public MessageDTO Create(MessageDTO messageDto)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, Message>());
                var message = Mapper.Map<MessageDTO, Message>(messageDto);
                _unitOfWork.Messages.Create(message);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public void SendMessage(MessageDTO messageDto, int fromId, int toId)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, Message>());
                var message = Mapper.Map<MessageDTO, Message>(messageDto);
                message.ApplicationUser = _unitOfWork.UserManager.FindById(fromId);
                var userTo = _unitOfWork.UserManager.FindById(toId);
                message.Receiver = userTo;
                _unitOfWork.Messages.Create(message);
                _unitOfWork.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        public void Update(int id, MessageDTO newMessageDto)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                throw new ValidationException("Message doesn't exist", "");

            Mapper.Initialize(cfg => cfg.CreateMap<MessageDTO, Message>());
            var newMessage = Mapper.Map<MessageDTO, Message>(newMessageDto);
            _unitOfWork.Messages.Update(newMessage);
        }

        public void Remove(int id)
        {
            try
            {
                _unitOfWork.Messages.Delete(id);
                _unitOfWork.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
