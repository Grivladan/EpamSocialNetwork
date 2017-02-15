using SocialNetwork.DataAccess.Entities;
using System;

namespace SocialNetwork.Logic.DTO
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Gender? Gender { get; set; }
        public string Phone { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
