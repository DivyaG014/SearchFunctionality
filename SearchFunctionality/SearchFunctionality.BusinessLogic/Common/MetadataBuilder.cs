using SearchFunctionality.BusinessLogic.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.Common
{
    public class MetadataBuilder<TDto>
    {
        public Metadata<TDto> Metadata { get; private set; }

        public MetadataBuilder()
        {
            Metadata = new Metadata<TDto>();
            Metadata.SearchFilters = new List<PropertyInfo>();
        }

        public MetadataBuilder<TDto> AddTextSearchProperty(Expression<Func<TDto, string>> expression)
        {
            var me = ((MemberExpression)expression.Body);
            Metadata.SearchFilters.Add((PropertyInfo)me.Member);
            return this;
        }
    }
}
