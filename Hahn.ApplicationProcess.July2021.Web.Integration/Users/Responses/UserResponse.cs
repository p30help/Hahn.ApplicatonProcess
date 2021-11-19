using System;
using System.Collections.Generic;
using System.Text;
using Hahn.ApplicationProcess.July2021.Web.Integration.Users.Requests;

namespace Hahn.ApplicationProcess.July2021.Web.Integration.Users.Responses
{
    public class UserResponse
    {
        public  int id { get; set; }

        public int age { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public List<AssetResponse> assets { get; set; }
    }
}
