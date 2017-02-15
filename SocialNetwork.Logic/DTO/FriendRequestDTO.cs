using SocialNetwork.DataAccess.Entities;
using System;

namespace SocialNetwork.Logic.DTO
{
    public class FriendRequestDTO
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int RequestedTo { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
    }
}
