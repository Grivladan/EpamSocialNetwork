﻿using System;
using SocialNetwork.DataAccess.Interfaces;

namespace SocialNetwork.DataAccess.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User PostedOn { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
