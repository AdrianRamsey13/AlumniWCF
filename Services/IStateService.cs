﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AlumniWCF.DTO;

namespace AlumniWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStateService" in both code and config file together.
    [ServiceContract]
    public interface IStateService
    {
        [OperationContract]
        IEnumerable<StateDTO> GetStates();
    }
}
