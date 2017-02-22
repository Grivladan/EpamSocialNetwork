using SocialNetwork.DataAccess.Entities;
using System;

namespace SocialNetwork.WebHost.ViewModel
{
    public class LikeViewModel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int PostId { get; set; }
        public DateTime? Date { get; set; }
        public virtual Post Post { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}