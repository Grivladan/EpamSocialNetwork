using SocialNetwork.DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebHost.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You can't send empty message")]
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public bool isReaded { get; set; }

        public MessageViewModel()
        {
            Date = DateTime.Now;
            isReaded = false;
        }
    }
}