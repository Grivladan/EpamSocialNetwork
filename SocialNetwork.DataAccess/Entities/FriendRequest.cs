using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataAccess.Entities
{
    public class FriendRequest
    {
        [Key]
        public int RequestId { get; set; }
        public int RequestedFrom{ get; set; }
        public int RequestedTo { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
    }
}
