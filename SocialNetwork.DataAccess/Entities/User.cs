using SocialNetwork.DataAccess.Interfaces;

namespace SocialNetwork.DataAccess.Entities
{
    public class User : IEntity
    {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    }
}
