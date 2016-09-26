using SocialNetwork.DataAccess.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        Message GetById(int id);
        Message Create(Message message);
        Message Update(int id, Message message);

        void Dispose();
    }
}
