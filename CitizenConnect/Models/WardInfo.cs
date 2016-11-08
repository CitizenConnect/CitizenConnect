using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;

namespace CitizenConnect.Models
{
    public class WardInfo
    {
        public Object getAddress()
        {
            string url = "https://maps.googleapis.com/maps/api/js?key=AIzaSyA-UEiyhrkeVKo2UfwX3WNKqToGvgL9yFc&libraries=places";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
    }
}