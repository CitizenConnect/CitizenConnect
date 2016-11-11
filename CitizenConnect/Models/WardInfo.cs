using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CitizenConnect.Models
{
    public class WardInfo
    {
        public Object getWardInfo()
        {
            var API_KEY = "AIzaSyA-UEiyhrkeVKo2UfwX3WNKqToGvgL9yFc";
            var INFO_API = "https://www.googleapis.com/civicinfo/v2/representatives";
            var address = $('#address').val();
            var params = 
                {
                "key": API_KEY;
                "address": address
                    }
            string url = "https://www.googleapis.com/civicinfo/v2/representatives?key=AIzaSyA-UEiyhrkeVKo2UfwX3WNKqToGvgL9yFc";
            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
    }
}