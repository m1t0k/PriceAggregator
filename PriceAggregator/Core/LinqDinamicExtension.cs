using System;
using System.Linq;
using System.Linq.Expressions;

namespace PriceAggregator.Core
{
    public static class LinqDinamicExtension

    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool ascending)
        {
            ParameterExpression param = Expression.Parameter(typeof (T), "p");
            if (string.IsNullOrEmpty(sortField))
                sortField = "Id";

            MemberExpression prop = Expression.Property(param, sortField);
            LambdaExpression exp = Expression.Lambda(prop, param);
            string method = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = {q.ElementType, exp.Body.Type};
            MethodCallExpression mce = Expression.Call(typeof (Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}