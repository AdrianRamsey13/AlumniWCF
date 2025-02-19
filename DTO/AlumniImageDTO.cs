using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniWCF.DTO
{
    public class AlumniImageDTO
    {
        public int ImageID{get; set;}

        public int AlumniID{get; set;}

        public string ImagePath{get; set;}

        public string FileName{get; set;}

        public System.DateTime UploadDate{get; set;}
    }
}