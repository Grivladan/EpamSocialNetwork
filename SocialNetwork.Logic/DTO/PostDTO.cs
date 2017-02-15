using System;

namespace SocialNetwork.Logic.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? ApplicationUserId { get; set; }
    }
}
