using System;
using SocialNetwork.DataAccess.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataAccess.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "You can't write empty post")]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Comment> Comments{ get; set;}
        public virtual ICollection<Like> Likes { get; set; }
        public int LikeCount
        {
            get { return Likes.Count; }
            private set { }
        }

        public Post()
        {
            Date = DateTime.Now;
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}
