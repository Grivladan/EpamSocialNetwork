using SocialNetwork.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace SocialNetwork.WebHost.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int LikeCount
        {
            get { return Likes.Count; }
            private set { }
        }

        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}