using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AlumniWCF.DBML;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlumniImageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlumniImageService.svc or AlumniImageService.svc.cs at the Solution Explorer and start debugging.
    public class AlumniImageService : IAlumniImageService
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public AlumniImageService()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        public IEnumerable<AlumniImageDTO> GetAllImages(int alumniID)
        {
            var alumniImages = _context.AlumniImages.Where(a => a.AlumniID == alumniID)
                .OrderByDescending(a => a.UploadDate)
                .Select(a => Mapping.Mapper.Map<AlumniImageDTO>(a))
                .ToList();

            return alumniImages;
        }
        public AlumniImageDTO GetImageByID(int imageID, int alumniID)
        {
            var alumniImage = _context.AlumniImages.FirstOrDefault(a => a.ImageID == imageID && a.AlumniID == alumniID);
            var result = Mapping.Mapper.Map<AlumniImageDTO>(alumniImage);
            return result;
        }

        public async Task AddImage(IEnumerable<AlumniImageDTO> alumniImages)
        {
            if (alumniImages == null || !alumniImages.Any())
            {
                throw new ArgumentException("No image to add");
            }

            try
            {
                var newImages = alumniImages.Select(a => new AlumniImage
                {
                    AlumniID = a.AlumniID,
                    ImagePath = a.ImagePath,
                    FileName = a.FileName,
                    UploadDate = DateTime.Now
                }).ToList();

                _context.AlumniImages.InsertAllOnSubmit(newImages); // menambahkan semua data sekaligus
                await Task.Run(() => _context.SubmitChanges()); // tidak memblokir thread utama
            }catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }

        }

        public async Task DeleteIamgeByIDAsync(int imageID, int alumniID)
        {
            try
            {
                var imageToDelete = _context.AlumniImages.FirstOrDefault(a => a.ImageID == imageID && a.AlumniID == alumniID);
                if (imageToDelete == null)
                {
                    throw new ArgumentException("Image not found");
                }
                _context.AlumniImages.DeleteOnSubmit(imageToDelete);
                await Task.Run(() => _context.SubmitChanges());
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error: {ex.Message}");
            }
        }
    }
}
