using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lab6.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderByPropertyInternal(source, propertyName, "OrderBy");
        }

        public static IQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderByPropertyInternal(source, propertyName, "OrderByDescending");
        }

        private static IQueryable<T> OrderByPropertyInternal<T>(IQueryable<T> source, string propertyName, string methodName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property {propertyName} not found on type {type.FullName}");
            }
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType },
                                                    source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
