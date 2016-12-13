using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        IEnumerable<Message> GetUserMessages(int id);
        Message GetById(int id);
        Message Create(Message message);
        Message Update(int id, Message message);
        void Remove(int id);
        void SendMessage(Message message, int fromId, int toId);
        int CountUnreadedMessages(int id);

        void Dispose();
    }
}
