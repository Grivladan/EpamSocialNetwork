using System;
using System.Collections.Generic;

namespace SocialNetwork.DataAccess.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}
