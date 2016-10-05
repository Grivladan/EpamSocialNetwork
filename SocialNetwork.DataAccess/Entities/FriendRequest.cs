﻿using SocialNetwork.DataAccess.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataAccess.Entities
{
    public class FriendRequest : IEntity
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }
        public int RequestedTo{ get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }

        public FriendRequest()
        {
            Date = DateTime.Now;
            IsAccepted = false;
        }
    }
}
