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
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PlaceID { get; set; }
        public string AddressString { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("ReportType")]
        public int ReportTypeID { get; set; }
        public virtual ReportType ReportType { get; set; }
    }
}