using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CitizenConnect.Models
{
    public class ReportView
    {
        [Key]
        public int ReportID { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        
        public int ReportTypeID { get; set; }
    }
}