using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace something.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; } //Users who apply for the job
    }
}