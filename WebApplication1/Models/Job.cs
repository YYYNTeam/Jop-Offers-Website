using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Jop_Offers_Website.Models
{
    public class Job
    {
        public int Id { get; set; } 

        [Display(Name ="Job name" )]
        public string JobTitle { get; set; }

        [Display(Name = "Job description")]
        [AllowHtml]
        public string JobContent { get; set; }

        [Display(Name = "Job image")]
        public string JobImg { get; set; }

        [Display(Name ="Job category")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}