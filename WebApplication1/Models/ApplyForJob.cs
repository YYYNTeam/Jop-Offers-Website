using Jop_Offers_Website.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication2.Models;

// this is the third table 
namespace something.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public String Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public String UserId { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserName { get; set; }

        // how to deal with the ralation ship in the entity relation ship
        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }

        public virtual ApplicationUser AspNetUsers { get; set; }
    }
}

