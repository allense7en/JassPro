using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassPro.Web.Models
{
    public class VResult
    {
        public bool success { get; set; }

        public string msg { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public int error_code { get; set; }

        public string error_msg { get; set; }

        public object data { get; set; }
    }
}