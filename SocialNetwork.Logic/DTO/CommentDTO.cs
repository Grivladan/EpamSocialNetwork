using SocialNetwork.DataAccess.Entities;
using System;

namespace SocialNetwork.Logic.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public CommentDTO()
        {
            CommentDate = DateTime.Now;
        }
    }
}
