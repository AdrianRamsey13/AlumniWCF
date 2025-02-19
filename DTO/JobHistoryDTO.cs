using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using AlumniWCF.DBML;

namespace AlumniWCF.DTO
{
    public class JobHistoryDTO
    {
        public int JobHistoryID{get; set;}

        public int AlumniID{get; set;}

        public string JobTitle{get; set;}

        public string Company{get; set;}

        public DateTime StartDate{get; set;}

        public DateTime EndDate{get; set;}

        public string Description{get; set;}

        public System.DateTime ModifiedDate{get; set;}

        public string Alumni { get; set; }
    }
}