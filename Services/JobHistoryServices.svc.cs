using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;
using AlumniWCF.DBML;
using AutoMapper;
using System.Configuration;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JobHistoryServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select JobHistoryServices.svc or JobHistoryServices.svc.cs at the Solution Explorer and start debugging.
    public class JobHistoryServices : IJobHistoryServices
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public JobHistoryServices()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        public IEnumerable<JobHistoryDTO> GetJobHistories(int alumniID)
        {
            //get the data based on alumniID
            var jobHistories = _context.JobHistories.Where(j => j.AlumniID == alumniID).ToList().Select(j => Mapping.Mapper.Map<JobHistoryDTO>(j));
            return jobHistories;
        }

        public JobHistoryDTO GetJobHistoryByID(int jobHistoryID, int alumniID)
        {
            var jobHistory = _context.JobHistories.FirstOrDefault(j => j.JobHistoryID == jobHistoryID);
            var result = Mapping.Mapper.Map<JobHistoryDTO>(jobHistory);
            return result;
        }
        public void AddJobHistory(JobHistoryDTO jobHistory, int alumniID)
        {
            //var exisitngAlumni = _context.Alumnis.FirstOrDefault(a => a.AlumniID == alumniID);
            //var newJob = Mapping.Mapper.Map<JobHistory>(jobHistory);
            //newJob.ModifiedDate = DateTime.Now;

            var newJob = new JobHistory
            {
                AlumniID = alumniID, // Mengisi AlumniID secara langsung
                JobTitle = jobHistory.JobTitle, // Mengisi JobTitle
                Company = jobHistory.Company, // Mengisi Company
                StartDate = jobHistory.StartDate, // Mengisi StartDate
                EndDate = jobHistory.EndDate, // Mengisi EndDate
                Description = jobHistory.Description, // Mengisi Description
                ModifiedDate = DateTime.Now // Mengisi ModifiedDate
            };
            _context.JobHistories.InsertOnSubmit(newJob);
            _context.SubmitChanges();
        }

        public void UpdateJobHistory(JobHistoryDTO jobHistoryDTO, int alumniID)
        {
            // Mencari JobHistory yang sudah ada berdasarkan JobHistoryID
            var existingJob = _context.JobHistories.FirstOrDefault(j => j.JobHistoryID == jobHistoryDTO.JobHistoryID);

            if (existingJob != null)
            {
                // Mengupdate properti secara manual
                existingJob.AlumniID = alumniID; // Memastikan AlumniID tetap sama
                existingJob.JobTitle = jobHistoryDTO.JobTitle; // Mengupdate JobTitle
                existingJob.Company = jobHistoryDTO.Company; // Mengupdate Company
                existingJob.StartDate = jobHistoryDTO.StartDate; // Mengupdate StartDate
                existingJob.EndDate = jobHistoryDTO.EndDate; // Mengupdate EndDate
                existingJob.Description = jobHistoryDTO.Description; // Mengupdate Description
                existingJob.ModifiedDate = DateTime.Now; // Mengupdate ModifiedDate

                // Menyimpan perubahan ke database
                _context.SubmitChanges();
            }
            else
            {
                // Jika tidak ada job history yang ditemukan, bisa menambahkan logika penanganan error atau exception
                throw new Exception("JobHistory tidak ditemukan.");
            }
        }


        public void DeleteJobHistory(int jobHistoryID, int alumniID)
        {
            var result = _context.JobHistories.FirstOrDefault(j => j.JobHistoryID == jobHistoryID);
            _context.JobHistories.DeleteOnSubmit(result);
            _context.SubmitChanges();
        }
    }
}
