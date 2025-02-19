using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Web;
using AlumniWCF.DBML;

namespace AlumniWCF.DTO
{
    public class AlumniDTO
    {
        public int AlumniID{get; set;}

        public string FirstName{get; set;}

        public string MiddleName{get; set;}

        public string LastName{get; set;}

        public string Email{get; set;}

        public string MobileNumber{get; set;}

        public string Address{get; set;}

        public System.Nullable<int> DistrictID { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public System.Nullable<int> GraduationYear { get; set; }

        public string Degree{get; set;}

        public System.Nullable<int> MajorID { get; set; }

        public string LinkedInProfile{get; set;}

        public System.DateTime ModifiedDate{get; set;}
        public string StateName { get; set; }
        public string DistrictName { get; set; }

        public string MajorName{get; set;}

        public string FacultyName { get; set; }

        public string FullName { get; set;}

        public string FullAddress{get; set;}

        public int FacultyID { get; set; }

        public int StateID { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<SelectListItem> Districts { get; set; }

        public IEnumerable<SelectListItem> Faculties { get; set; }

        public IEnumerable<SelectListItem> Majors { get; set; }

        public IEnumerable<SelectListItem> Degrees { get; set; }
    }
}