using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public enum Question
    {
        earth, computer
    }
    
    public class AnswerImage
    {
        public int AnswerImageId
        {
            get;
            set;
        }
        
        [Required]
        [DisplayName("File Name")]
        public string FileName
        {
            get;
            set;
        }

        [Required]
        [Url]
        [DisplayName("Image")]
        public string Url
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Question")]
        public Question Question { get; set; }
    }

}
