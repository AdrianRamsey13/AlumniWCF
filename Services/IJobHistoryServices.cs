using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJobHistoryServices" in both code and config file together.
    [ServiceContract]
    public interface IJobHistoryServices
    {
        [OperationContract]
        IEnumerable<JobHistoryDTO> GetJobHistories(int alumniID);

        [OperationContract]
        JobHistoryDTO GetJobHistoryByID(int alumniID, int jobHistoryID);

        [OperationContract]
        void AddJobHistory(JobHistoryDTO jobHistory, int alumniID);
        [OperationContract]
        void UpdateJobHistory(JobHistoryDTO jobHistory, int alumniID);
        [OperationContract]
        void DeleteJobHistory(int jobHistoryID, int alumniID);

    }
}
