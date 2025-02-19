using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;
using AlumniWCF.DBML;
using AutoMapper;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MajorServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MajorServices.svc or MajorServices.svc.cs at the Solution Explorer and start debugging.
    public class MajorServices : IMajorServices
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public MajorServices()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        //---------------------------------------------------------------------------------
        public IEnumerable<MajorDTO> GetMajors()
        {
            //var majors = _context.Majors.ToList().Select(m => Mapping.Mapper.Map<MajorDTO>(m));
            //return majors;

            var query = from m in _context.Majors
                        join f in _context.Faculties on m.FacultyID equals f.FacultyID
                        select new MajorDTO
                        {
                            MajorID = m.MajorID,
                            MajorName = m.MajorName,
                            FacultyName = f.FacultyName,
                            Description = m.Description,
                            FacultyID = f.FacultyID,
                            ModifiedDate = m.ModifiedDate
                        };
            var result = query.ToList().OrderByDescending(m => m.ModifiedDate).Take(20);
            return result;
        }

        public MajorDTO GetMajorByID(int majorID)
        {
            var major = _context.Majors.FirstOrDefault(m => m.MajorID == majorID);
            var result = Mapping.Mapper.Map<MajorDTO>(major);
            return result;
        }

        public MajorDTO GetMajorIdByName(string majorName)
        {
            var selectedData = _context.Majors.FirstOrDefault(m => m.MajorName == majorName);
            var result = Mapping.Mapper.Map<MajorDTO>(selectedData);
            return result;
        }

        public void AddMajor(MajorDTO majorDDTO)
        {
            var majorDTO = new Major
            {
                MajorName = majorDDTO.MajorName,
                FacultyID = majorDDTO.FacultyID,
                Description = majorDDTO.Description,
                ModifiedDate = DateTime.Now
            };
            _context.Majors.InsertOnSubmit(majorDTO);
            _context.SubmitChanges();
        }

        public void UpdateMajor(MajorDTO major)
        {
            var exisitingMajor = _context.Majors.FirstOrDefault(m => m.MajorID == major.MajorID);
            var updatedMajor = Mapping.Mapper.Map(major, exisitingMajor);
            updatedMajor.ModifiedDate = DateTime.Now;
            _context.SubmitChanges();
        }
        public void DeleteMajor(int majorID)
        {
           var result = _context.Majors.First(m => m.MajorID == majorID);
            _context.Majors.DeleteOnSubmit(result);
            _context.SubmitChanges();
        }        
    }
}
