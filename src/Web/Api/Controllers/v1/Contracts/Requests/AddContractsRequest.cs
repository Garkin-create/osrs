using System.Collections.Generic;
using OSRS.Api.Controllers.v1.Contracts.Model.Input;

namespace OSRS.Api.Controllers.v1.Contracts.Requests
{
    public class AddContractsRequest
    {
        public List<ContractInputModel> Data { get; set; }
        public string IdLabel { get; set; }
        public string IdAgente { get; set; }
    }
}

