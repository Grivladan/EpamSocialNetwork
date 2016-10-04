using SocialNetwork.DataAccess.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataAccess.Entities
{
    public class FriendRequest : IEntity
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser RequestedFrom{ get; set; }
        public virtual ApplicationUser RequestedTo { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }

        public FriendRequest()
        {
            Date = DateTime.Now;
            IsAccepted = false;
        }
    }
}
