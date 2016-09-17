using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetwork.DataAccess.Interfaces;

namespace SocialNetwork.DataAccess.Entities
{
    public class Profile : IEntity
    {
       // [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
