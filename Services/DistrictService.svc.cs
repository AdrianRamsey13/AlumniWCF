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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DistrictService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DistrictService.svc or DistrictService.svc.cs at the Solution Explorer and start debugging.
    public class DistrictService : IDistrictService
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public DistrictService()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        public IEnumerable<DistrictDTO> GetDistrictByStateID(int stateID)
        {
            var query = from d in _context.Districts
                        join s in _context.States on d.StateID equals s.StateID
                        where d.StateID == stateID
                        select new DistrictDTO
                        {
                            DistrictID = d.DistrictID,
                            DistrictName = d.DistrictName,
                            StateID = s.StateID,
                            StateName = s.StateName,
                        };
            var result = query.ToList();
            return result;
        }
    }
}
