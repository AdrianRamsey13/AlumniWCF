using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using AlumniWCF.DBML;
using AutoMapper;

namespace AlumniWCF.DTO
{
    public class MajorDTO
    {
        public int MajorID{get; set;}

        public string MajorName{get; set;}

        public System.Nullable<int> FacultyID { get; set; }

        public string Description{get; set;}

        public System.DateTime ModifiedDate{get; set;}

        [IgnoreMap]
        public string FacultyName { get; set; }
    }
}