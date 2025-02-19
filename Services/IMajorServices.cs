using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMajorServices" in both code and config file together.
    [ServiceContract]
    public interface IMajorServices
    {
        [OperationContract]
        IEnumerable<MajorDTO> GetMajors();

        [OperationContract]
        MajorDTO GetMajorByID(int majorID);

        [OperationContract]
        MajorDTO GetMajorIdByName(string majorName);

        [OperationContract]
        void AddMajor(MajorDTO major);

        [OperationContract]
        void UpdateMajor(MajorDTO major);

        [OperationContract]
        void DeleteMajor(int majorID);
    }
}
