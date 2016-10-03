using System;
using SocialNetwork.DataAccess.Interfaces;

namespace SocialNetwork.DataAccess.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public bool isReaded { get; set; }

        public Message()
        {
            Date = DateTime.Now;
            isReaded = false;
        }
    }
}
