using Microsoft.EntityFrameworkCore;
using SearchFunctionality.BusinessLogic.SearchModels;
using System.Linq.Expressions;
using System.Reflection;

namespace SearchFunctionality.BusinessLogic.Common
{
    public static class SearchHelper
    {
        static MethodInfo string_Contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

        public static async Task<SearchData<TDto>> ApplySearch<TDto>(
        this IQueryable<TDto> query,
        string searchText,
        Metadata<TDto> metadata)
        {         
            var parameter = Expression.Parameter(typeof(TDto));
            var filters = metadata.SearchFilters.Select(item =>
            {
                var property = Expression.Property(parameter, item);
                var constantValue = Expression.Constant(searchText);
                var call = Expression.Call(property, string_Contains, constantValue);
                return call;
            }).ToList();

            Expression<Func<TDto, bool>> lambda;
            if (filters.Count >= 2)
            {
                var combinedFilters = filters.Skip(2)
                    .Aggregate(Expression.OrElse(filters[0], filters[1]), (x, y) => Expression.OrElse(x, y));
                lambda = Expression.Lambda<Func<TDto, bool>>(combinedFilters, parameter);
            }
            else
            {
                lambda = Expression.Lambda<Func<TDto, bool>>(filters[0], parameter);
            }
            query = query.Where(lambda);
            var data = await query.ToListAsync();
            return new SearchData<TDto>
            {
                Items = await query.ToListAsync()
            };
        }
    }
}
