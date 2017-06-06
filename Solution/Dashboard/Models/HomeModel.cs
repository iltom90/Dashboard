using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class HomeModel
    { 
        public List<long> lstUserID { get; set; }

        public HomeModel()
        {
            lstUserID = new List<long>();           
        }
    }
}