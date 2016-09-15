using System;
using SocialNetwork.DataAccess.Interfaces;

namespace SocialNetwork.DataAccess.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
       // public ApplicationUser Author { get; set; }
        public DateTime Date { get; set; }

        /*public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }*/
    }
}
