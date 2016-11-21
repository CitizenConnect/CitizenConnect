using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CitizenConnect.Models
{
    public class InterestedVolunteers
    {
        [Key]
        public int InterestedUserID { get; set; }
        public bool IfInterested { get; set; }

        //ApplicationUser and ProjectID are Primary Key
        [Display(Name = "Volunteers")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey("ProjectView")]
        [Display(Name = "Community Projects")]
        public int ProjectID { get; set; }
        public virtual ProjectView ProjectView  { get; set; }
        


    }
}