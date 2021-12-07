using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class Community
    {
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Registration Number")]
        public string Id
        {
            get;
            set;
        }

        [Required]
        [StringLength(maximumLength:50, MinimumLength = 3)]
        public string Title
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget
        {
            get;
            set;
        }

        public ICollection<CommunityMembership> CommunityMembership { get; set; }

        public static implicit operator Community(List<Community> v)
        {
            throw new NotImplementedException();
        }
    }
}
