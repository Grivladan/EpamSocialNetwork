using System;

namespace SocialNetwork.WebHost.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? ApplicationUserId { get; set; }
    }
}