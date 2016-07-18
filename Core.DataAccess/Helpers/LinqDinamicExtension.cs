using System;
using System.Linq;
using System.Linq.Expressions;

namespace PriceAggregator.Core.DataAccess.Helpers
{
    public static class LinqDinamicExtension

    {
        public static IQueryable<T> OrderByField<T>(this IQueryable query, string sortField, bool ascending)
        {
            ParameterExpression param = Expression.Parameter(typeof (T), "p");
            if (string.IsNullOrEmpty(sortField))
                sortField = "Id";

            MemberExpression prop = Expression.Property(param, sortField);
            LambdaExpression exp = Expression.Lambda(prop, param);
            string method = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = {query.ElementType, exp.Body.Type};
            MethodCallExpression mce = Expression.Call(typeof (Queryable), method, types, query.Expression, exp);
            return query.Provider.CreateQuery<T>(mce);
        }
    }
}