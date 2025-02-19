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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StateService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StateService.svc or StateService.svc.cs at the Solution Explorer and start debugging.
    public class StateService : IStateService
    {
        private DataClasses1DataContext _context;

        private string connectionString = ConfigurationManager.ConnectionStrings["KDP22ConnectionString"].ToString();

        public string ConnectionString { get => connectionString; set => connectionString = value; }
       
        public StateService()
        {
            _context = new DataClasses1DataContext(ConnectionString);
        }

        public IEnumerable<StateDTO> GetStates()
        {
            var states = _context.States.ToList().Select(s => Mapping.Mapper.Map<StateDTO>(s));
            return states;
        }
    }
}
