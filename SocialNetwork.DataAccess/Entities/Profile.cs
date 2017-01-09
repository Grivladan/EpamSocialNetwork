﻿using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage="First name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Last name required")]
        public string LastName { get; set; } 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        [RegularExpression(@"([a-zA-Z]+|[a-zA-Z]+\\s[a-zA-Z]+)", ErrorMessage = "Entered country name is not valid.")]
        public string Country { get; set; }

        [RegularExpression(@"([a-zA-Z]+|[a-zA-Z]+\\s[a-zA-Z]+)", ErrorMessage = "Entered city name is not valid.")]
        public string City { get; set; }

        public Gender? Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        public byte[] UserPhoto { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
