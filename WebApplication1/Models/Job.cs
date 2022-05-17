using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

// class for jobs in DB -it has one to many relationship between it and Category-
//Category have more than job & one job belong to one category
namespace Jop_Offers_Website.Models
{
    public class Job
    {
        public int Id { get; set; } //primary key for DB

        [Display(Name ="اسم الوظيفة" )]
        public string JobTitle { get; set; }

        [Display(Name ="وصف الوظيفة")]
        public string JobContent { get; set; }


        [Display(Name = "صورة الوظيفة")]
        public string JobImg { get; set; }

        //Forign key for one to many relationship
        [Display(Name ="نوع الوظيفة")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}