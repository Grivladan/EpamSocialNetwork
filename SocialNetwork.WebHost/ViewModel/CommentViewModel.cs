using System;

namespace SocialNetwork.WebHost.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}