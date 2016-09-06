using System.Data.Entity;
using SocialNetwork.DataAccess.Entities;

namespace SocialNetwork.DataAccess.EF
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Group> Groups { get; set; }

        static SocialNetworkContext()
        {
            Database.SetInitializer<SocialNetworkContext>(new SocialNetworkInitializer());
        }

        public SocialNetworkContext(string connectionString) : base(connectionString)
        {}
    }
}

