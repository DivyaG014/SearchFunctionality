using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.ResponseModels
{
    public class FlightDetails
    {
        public string FlightNumber { get; set; }
        public string FromAirport { get; set;}
        public string ToAirport { get; set;}
        public DateTime? ArrivalTime { get; set;}
        public DateTime? DepartureTime { get; set;}
        public long? Price { get; set;}
    }
}
