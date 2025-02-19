using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DBML;
using AlumniWCF.DTO;
using AutoMapper;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FacultyServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FacultyServices.svc or FacultyServices.svc.cs at the Solution Explorer and start debugging.
    public class FacultyServices : IFacultyServices
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString {  get => connectionString; set => connectionString = value; }

        public FacultyServices()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        //---------------------------------------------------------------------------------
        public IEnumerable<FacultyDTO> GetFaculties()
        {
            //get the data
            var faculties = _context.Faculties
                        .OrderByDescending(f => f.ModifiedDate) // Sorting in descending order
                        .Select(f => Mapping.Mapper.Map<FacultyDTO>(f))
                        .ToList();

            return faculties;
        }

        public FacultyDTO GetFacultyByID(int facultyID)
        {
            var faculty = _context.Faculties.FirstOrDefault(f => f.FacultyID == facultyID);
            var result = Mapping.Mapper.Map<FacultyDTO>(faculty);
            return result;
        }
        public FacultyDTO GetFacultyIdByName(string facultyName)
        {
            var data = _context.Faculties.FirstOrDefault(f => f.FacultyName == facultyName);
            var result = Mapping.Mapper.Map<FacultyDTO>(data);
            return result;
        }

        public void AddFaculty(FacultyDTO faculty)
        {
            var newFaculty = Mapping.Mapper.Map<Faculty>(faculty);
            newFaculty.ModifiedDate = DateTime.Now;
            _context.Faculties.InsertOnSubmit(newFaculty);
            _context.SubmitChanges();
        }     

        public void UpdateFaculty(FacultyDTO faculty)
        {
            var existingFaculty = _context.Faculties.FirstOrDefault(f => f.FacultyID == faculty.FacultyID);
            var updateFacility = Mapping.Mapper.Map(faculty, existingFaculty);
            updateFacility.ModifiedDate = DateTime.Now;
            _context.SubmitChanges();
        }

        public void DeleteFaculty(int facultyID)
        {
            var result = _context.Faculties.FirstOrDefault(f => f.FacultyID == facultyID);
            _context.Faculties.DeleteOnSubmit(result);
            _context.SubmitChanges();
        }

    }
}
