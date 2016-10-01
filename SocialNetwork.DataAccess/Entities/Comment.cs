using SocialNetwork.DataAccess.Interfaces;
using System;

namespace SocialNetwork.DataAccess.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Comment()
        {
            CommentDate = DateTime.Now;
        }
        
    }
}
