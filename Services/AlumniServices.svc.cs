using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DBML;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlumniServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlumniServices.svc or AlumniServices.svc.cs at the Solution Explorer and start debugging.
    public class AlumniServices : IAlumniServices
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public AlumniServices()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        //---------------------------------------------------------------------------------

        public IEnumerable<AlumniDTO> GetAlumnis()
        {
            var query = from a in _context.Alumnis
                        join m in _context.Majors on a.MajorID equals m.MajorID
                        join f in _context.Faculties on m.FacultyID equals f.FacultyID
                        join d in _context.Districts on a.DistrictID equals d.DistrictID
                        join s in _context.States on d.StateID equals s.StateID
                        select new AlumniDTO
                        {
                            AlumniID = a.AlumniID,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            FullName = a.FirstName + " " + (a.MiddleName ?? "") + " " + a.LastName,
                            Email = a.Email,
                            MobileNumber = a.MobileNumber,
                            Address = a.Address,
                            MajorName = m.MajorName,
                            FacultyName = f.FacultyName,
                            LinkedInProfile = a.LinkedInProfile,
                            ModifiedDate = a.ModifiedDate,
                            FullAddress = a.Address + ", " + d.DistrictName + ", " + s.StateName,
                            FacultyID = f.FacultyID,
                            MajorID = m.MajorID,
                            StateID = s.StateID,
                            DistrictID = d.DistrictID,
                            DateOfBirth = (DateTime)a.DateOfBirth,
                            Degree = a.Degree,
                            StateName = s.StateName,
                            DistrictName = d.DistrictName,
                            GraduationYear = a.GraduationYear,
                        };
            return query.ToList().OrderByDescending(a => a.ModifiedDate).Take(20);
        }
        public AlumniDTO GetAlumniByID(int alumniID)
        {
            //var alumni = _context.Alumnis.FirstOrDefault(a => a.AlumniID == alumniID);
            //var result = Mapping.Mapper.Map<AlumniDTO>(alumni);
            //return result;
            var query = from a in _context.Alumnis
                        join m in _context.Majors on a.MajorID equals m.MajorID
                        join f in _context.Faculties on m.FacultyID equals f.FacultyID
                        join d in _context.Districts on a.DistrictID equals d.DistrictID
                        join s in _context.States on d.StateID equals s.StateID
                        where a.AlumniID == alumniID
                        select new AlumniDTO
                        {
                            AlumniID = alumniID,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            FullName = $"{a.FirstName} {(a.MiddleName ?? "").Trim()} {a.LastName}".Trim(),
                            Email = a.Email,
                            MobileNumber = a.MobileNumber,
                            Address = a.Address,
                            MajorName = m.MajorName,
                            FacultyName = f.FacultyName,
                            LinkedInProfile = a.LinkedInProfile,
                            ModifiedDate = a.ModifiedDate,
                            FullAddress = $"{a.Address}, {d.DistrictName}, {s.StateName}",
                            FacultyID = f.FacultyID,
                            MajorID = m.MajorID,
                            StateID = s.StateID,
                            DistrictID = d.DistrictID,
                            DateOfBirth = (DateTime)a.DateOfBirth,
                            Degree = a.Degree,
                            StateName = s.StateName,
                            DistrictName = d.DistrictName,
                            GraduationYear = a.GraduationYear,
                        };
            return query.FirstOrDefault();
        }

        public IEnumerable<AlumniDTO> GetAlumniFullName(string fullName)
        {
            var data = from a in _context.Alumnis
                       select a;
            var result = data.ToList().Select(a => new AlumniDTO
            {
                AlumniID = a.AlumniID,
                FullName = a.FirstName + " " + (a.MiddleName ?? "") + " " + a.LastName,
            });
            return result;
        }

        public void AddAlumni(AlumniDTO alumni)
        {
            var newAlumni = Mapping.Mapper.Map<Alumni>(alumni);
            newAlumni.ModifiedDate = DateTime.Now;
            _context.Alumnis.InsertOnSubmit(newAlumni);
            _context.SubmitChanges();
        }
        public void UpdateAlumni(AlumniDTO alumni)
        {
            var existingAlumni = _context.Alumnis.First(a => a.AlumniID == alumni.AlumniID);
            var updatedAlumni = Mapping.Mapper.Map(alumni, existingAlumni);
            updatedAlumni.ModifiedDate = DateTime.Now;
            _context.SubmitChanges();
        }

        public void DeleteAlumni(int alumniID)
        {
            var selectData = _context.Alumnis.First(a => a.AlumniID == alumniID);
            _context.Alumnis.DeleteOnSubmit(selectData);
            _context.SubmitChanges();
        }
        public int GetDistrictIdByName(string districtName)
        {
            var district = _context.Districts.FirstOrDefault(d => d.DistrictName == districtName);
            int districtId = district.DistrictID;
            return districtId;
        }

        public int GetStateIdByName(string stateName)
        {
            var state = _context.States.FirstOrDefault(s => s.StateName == stateName);
            int stateId = state.StateID;
            return stateId;
        }           
       

        //-------------------------Import Excel File--------------------------------

        public void ImportFromExcel(AlumniDTO alumniDTO)
        {
            throw new NotImplementedException();
        }
    }
}
