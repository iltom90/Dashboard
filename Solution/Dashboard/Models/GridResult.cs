using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class GridResult
    {
        public virtual ICollection<Work> Works { get; set; }
        public long NumTotWorks { get; set; }
        public Int32 NumTotPages { get; set; }

        public GridResult()
        {
            Works = new Collection<Work>();
        }
    }
}