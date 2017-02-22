using SocialNetwork.DataAccess.Entities;
using System;

namespace SocialNetwork.Logic.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PostId { get; set; }
        public DateTime? Date { get; set; }
        public virtual Post Post { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        
        public LikeDTO()
        {
            Date = DateTime.Now;    
        }
    }
}
