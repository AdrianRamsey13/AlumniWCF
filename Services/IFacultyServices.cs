using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFacultyServices" in both code and config file together.
    [ServiceContract]
    public interface IFacultyServices
    {
        [OperationContract]
        IEnumerable<FacultyDTO> GetFaculties();

        [OperationContract]
        FacultyDTO GetFacultyByID(int facultyID);

        [OperationContract]
        FacultyDTO GetFacultyIdByName(string facultyName);

        [OperationContract]
        void AddFaculty(FacultyDTO faculty);

        [OperationContract]
        void UpdateFaculty(FacultyDTO faculty);

        [OperationContract]
        void DeleteFaculty(int facultyID);
    }
}
