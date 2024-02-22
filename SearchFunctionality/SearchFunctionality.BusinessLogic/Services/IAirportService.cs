using SearchFunctionality.BusinessLogic.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.Services
{
    public interface IAirportService
    {
        Task<List<AirportsResponseModel>> GetAllAirports();
    }
}
