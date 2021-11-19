using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.July2021.Web.Integration.Users.Requests
{
    public class UserRequest
    {
        public int age { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string street { get; set; }

        public int postal_code { get; set; }

        public string house_number { get; set; }

        public string email { get; set; }

        public List<AssetRequest> assets { get; set; }
    }
}
