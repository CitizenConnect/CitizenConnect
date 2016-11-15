using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitizenConnect.Models
{
    public class ProjectView
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime CreationDate { get; set; }

        //member suggesting project
        public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}