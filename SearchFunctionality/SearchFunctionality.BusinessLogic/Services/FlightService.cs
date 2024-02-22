using Microsoft.EntityFrameworkCore;
using SearchFunctionality.BusinessLogic.Common;
using SearchFunctionality.BusinessLogic.RequestModels;
using SearchFunctionality.BusinessLogic.ResponseModels;
using SearchFunctionality.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.Services
{
    public class FlightService : IFlightService
    {
        private readonly airline_dbContext _dbContext;
        public FlightService(airline_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FlightDetails>> GetAllFlights(SearchFlights searchFlights)
        {
            var query = from flight in _dbContext.Flight
                        where flight.FromAirport.Code == searchFlights.FromAirportCode
                        && flight.ToAirport.Code == searchFlights.ToAirportCode                     
                        select new FlightDetails
                        {
                            ArrivalTime = flight.ArrivalTime,
                            DepartureTime = flight.DepartureTime,
                            FlightNumber = flight.FlightNumber,
                            FromAirport = flight.FromAirport.AirportName,
                            ToAirport = flight.ToAirport.AirportName,
                            Price = flight.Price
                        };

            if(searchFlights.FromPrice > 0)
            {
                query = query.Where(x => x.Price >= searchFlights.FromPrice);
            }

            if(searchFlights.ToPrice > 0)
            {
                query = query.Where(x => x.Price <= searchFlights.ToPrice);
            }

            var flightDetails = new List<FlightDetails>();
            if (!string.IsNullOrEmpty(searchFlights.SearchText))
            {
                var metadataBuilder = new MetadataBuilder<FlightDetails>();
                metadataBuilder.AddTextSearchProperty(x => x.FromAirport)
                   .AddTextSearchProperty(x => x.ToAirport);

                var result = await query.ApplySearch(searchFlights.SearchText, metadataBuilder.Metadata);
                flightDetails.AddRange(result.Items);
            }
            else
            {
                flightDetails = await query.ToListAsync();
            }
            return flightDetails;
        }

    }
}
