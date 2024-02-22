using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable warnings

namespace SearchFunctionality.BusinessLogic.RequestModels
{
    public class SearchFlights
    {
        public string FromAirportCode { get; set; }
        public string ToAirportCode { get;set; }
        public long? FromPrice { get; set; }
        public long? ToPrice { get; set; }
        public string SearchText { get; set; }
    }
}
