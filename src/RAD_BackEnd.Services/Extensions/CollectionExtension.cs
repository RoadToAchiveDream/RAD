using Newtonsoft.Json;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Helpers;
using System.Linq.Expressions;

namespace RAD_BackEnd.Services.Extensions;

public static class CollectionExtension
{
    public static IEnumerable<T> ToPaginateAsEnumerable<T>(this IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var json = JsonConvert.SerializeObject(new PaginationMetaData(totalCount, @params));

        HttpContextHelper.ResponseHeaders.Remove("X-Pagination");
        HttpContextHelper.ResponseHeaders?.Add("X-Pagination", json);

        return @params.PageIndex > 0 && @params.PageSize > 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IQueryable<T> ToPaginateAsQueryable<T>(this IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var json = JsonConvert.SerializeObject(new PaginationMetaData(totalCount, @params));

        HttpContextHelper.ResponseHeaders.Remove("X-Pagination");
        HttpContextHelper.ResponseHeaders?.Add("X-Pagination", json);

        return @params.PageIndex > 0 && @params.PageSize > 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Filter filter)
    {
        var expression = source.Expression;

        var parameter = Expression.Parameter(typeof(T), "x");

        MemberExpression selector;
        try
        {
            selector = Expression.PropertyOrField(parameter, filter?.OrderBy ?? "Id");
        }
        catch
        {
            throw new ArgumentIsNotValidException("Specified property is not found");
        }

        var method = string.Equals(filter?.OrderType ?? "asc", "desc", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";

        expression = Expression.Call(typeof(Queryable), method,
            new Type[] { source.ElementType, selector.Type },
            expression, Expression.Quote(Expression.Lambda(selector, parameter)));

        return source.Provider.CreateQuery<T>(expression);
    }
}
