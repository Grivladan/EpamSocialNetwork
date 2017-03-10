using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetwork.DataAccess.Interfaces;
using System;

namespace SocialNetwork.DataAccess.Entities
{
    public class Profile : IEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Key]
        [ForeignKey("ApplicationUser")]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
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
