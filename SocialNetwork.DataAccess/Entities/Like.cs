using SocialNetwork.DataAccess.Interfaces;
using System;

namespace SocialNetwork.DataAccess.Entities
{
    public class Like : IEntity
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime Date { get; set; }

        public Like()
        {
            Date = DateTime.Now;
        }
    }
}
