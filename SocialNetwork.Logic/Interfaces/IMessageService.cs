using SocialNetwork.Logic.DTO;
using System.Collections.Generic;

namespace SocialNetwork.Logic.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<MessageDTO> GetAll();
        IEnumerable<MessageDTO> GetUserMessages(int id);
        MessageDTO GetById(int id);
        MessageDTO Create(MessageDTO message);
        void Update(int id, MessageDTO message);
        void Remove(int id);
        void SendMessage(MessageDTO message, int fromId, int toId);
        int CountUnreadedMessages(int id);

        void Dispose();
    }
}
