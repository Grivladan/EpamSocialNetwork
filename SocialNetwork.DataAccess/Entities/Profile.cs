using System.Collections.Generic;
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
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        public string City { get; set; }
        public Gender? Gender { get; set; }
        public string Phone { get; set; }
        public byte[] UserPhoto { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
