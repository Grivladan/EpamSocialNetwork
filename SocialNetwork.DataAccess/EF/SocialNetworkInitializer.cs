using System.Data.Entity;

namespace SocialNetwork.DataAccess.EF
{
    public class SocialNetworkInitializer : DropCreateDatabaseAlways<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            
        }
    }
}
