using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CitizenConnect.Models
{
    public class ProjectView
    {
        [Key]
        public int ProjectID { get; set; }
        [Display(Name = "project name")]
        public string ProjectName { get; set; }
        [Display(Name ="description")]
        public string ProjectDescription { get; set; }
        public DateTime CreationDate { get; set; }

        //member suggesting project
        [Display(Name = "Organizer")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //Volunteers
        [ForeignKey("InterestedVolunteers")]
        [Display(Name = "Volunteer")]
        public int InterestedUserID { get; set; }
        public virtual ICollection<InterestedVolunteers> InterestedVolunteers { get; set; }
        

       

    }
}