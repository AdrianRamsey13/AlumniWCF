using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAlumniServices" in both code and config file together.
    [ServiceContract]
    public interface IAlumniServices
    {
        [OperationContract]
        IEnumerable<AlumniDTO> GetAlumnis();

        [OperationContract]
        AlumniDTO GetAlumniByID(int alumniID);

        [OperationContract]
        IEnumerable<AlumniDTO> GetAlumniFullName(string fullName);

        [OperationContract]
        void AddAlumni(AlumniDTO alumni);

        [OperationContract]
        void UpdateAlumni(AlumniDTO alumni);

        [OperationContract]
        void DeleteAlumni(int alumniID);

        [OperationContract]
        int GetDistrictIdByName(string districtName);

        [OperationContract]
        int GetStateIdByName(string stateName);
               

        //-------------------------Import Excel File--------------------------------
        [OperationContract]
        void ImportFromExcel(AlumniDTO alumniDTO);
    }
}
