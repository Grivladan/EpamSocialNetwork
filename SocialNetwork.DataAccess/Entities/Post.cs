using System;
using SocialNetwork.DataAccess.Interfaces;
using System.Collections.Generic;

namespace SocialNetwork.DataAccess.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Comment> Comments{ get; set;}

        public Post()
        {
            Date = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}
