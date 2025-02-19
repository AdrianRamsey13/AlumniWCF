using AlumniWCF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAlumniImageService" in both code and config file together.
    [ServiceContract]
    public interface IAlumniImageService
    {
        [OperationContract]
        IEnumerable<AlumniImageDTO> GetAllImages(int alumniID);

        [OperationContract]
        AlumniImageDTO GetImageByID(int imageID, int alumniID);

        [OperationContract]
        Task AddImage(IEnumerable<AlumniImageDTO> alumniImages);

        [OperationContract]
        Task DeleteIamgeByIDAsync(int imageID, int alumniID);
    }
}
