using System;
using System.Collections.Generic;

namespace SocialNetwork.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual IList<User> Friends { get; set; }
        public virtual IList<Message> Messages { get; set; }
        public virtual IList<Post> Posts { get; set; }
    }
}
