using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class Work
    {
        public string submittedAnswerId { get; set; }
        public DateTime submitDateTime { get; set; }
        public Int32 correct { get; set; }
        public Int32 progress { get; set; }
        public long userId { get; set; }
        public string exerciseId { get; set; }
        public string difficulty { get; set; }
        public string subject { get; set; }
        public string domain { get; set; }
        public string learningObjective { get; set; }
    }
}