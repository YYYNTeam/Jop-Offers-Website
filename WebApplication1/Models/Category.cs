using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


// class for Category in DB -it has one to many relationship between it and Job-
//one Category have more than job & one job belong to one category

//this class for jops_Category and control it
namespace Jop_Offers_Website.Models
{
    public class Category
    {
        public int Id { get; set; } //primary key in DB

        [Required]
        [Display(Name ="نوع الوظيفة")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "وصف النوع")]
        public string CategoryDescription { get; set; }


        //one category have more than one job 

        public virtual ICollection<Job> Jobs { get; set; }
    }
}