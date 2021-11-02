using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Student
    {

        public int Id 
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate
        {
            get;
            set;
        }


        public string FullName
        {
            get { 
                return LastName + ", " + FirstName;
            } 
        }

        public ICollection<CommunityMembership> CommunityMembership { get; set; }
    }
}
