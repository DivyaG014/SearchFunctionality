using Microsoft.EntityFrameworkCore;
using SearchFunctionality.BusinessLogic.ResponseModels;
using SearchFunctionality.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.Services
{
    public class AirportService : IAirportService
    {
        private readonly airline_dbContext _dbContext;
        public AirportService(airline_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AirportsResponseModel>> GetAllAirports()
        {
            var data = await (from airport in _dbContext.Airport
                       select new AirportsResponseModel
                       {
                           AirportCode = airport.Code,
                           AirportName = airport.AirportName
                       }).ToListAsync();
            return data;
        }
    }
}
