using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Person
    {

        [Required]
        [StringLength(255)]
        [BindProperty(Name = "firstName")]
        public string firstName
        {
            get;
            set;
        }

        [Required]
        [StringLength(255)]
        [BindProperty(Name = "lastName")]
        public string lastName
        {
            get;
            set;
        }

        [Required]
        [Range(0, int.MaxValue)]
        [BindProperty(Name = "age")]
        public int age
        {
            get;
            set;
        }

        [Required]
        [EmailAddress]
        [BindProperty(Name = "emailAddress")]
        public string emailAddress
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty(Name = "dateOfBirth")]
        public DateTime dob
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        [BindProperty(Name = "password")]
        public string password
        {
            get;
            set;
        }

        [Required]
        [StringLength(255)]
        [BindProperty(Name = "descriptionOfPerson")]
        public string dop
        {
            get;
            set;
        }

     
    }
}
