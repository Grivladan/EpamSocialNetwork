using System;

namespace SocialNetwork.Logic.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }
    }
}
