using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataAccess.Entities
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }

        public virtual IList<User> Friends { get; set; }
        public virtual IList<Message> Messages { get; set; }
        public virtual IList<Post> Posts { get; set; }

        public User User { get; set; }
    }
}
