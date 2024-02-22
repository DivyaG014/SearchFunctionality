using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.SearchModels
{
    public class SearchData<T>
    {
        public List<T> Items { get; set; }
    }

    public class Metadata<TDto>
    {
        public List<PropertyInfo> SearchFilters { get; set; }
    }
}
