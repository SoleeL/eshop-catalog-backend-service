namespace Catalog.Application.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByColumns<T>(this IQueryable<T> source, List<string> columns)
    {
        // Quizas no sea necesario
        if (columns == null || !columns.Any())
            throw new ArgumentException("No se especificaron columnas para ordenar.");

        IOrderedQueryable<T>? orderedQueryable = null;

        for (int i = 0; i < columns.Count; i++)
        {
            string columnName = columns[i].StartsWith("-") == false ? columns[i] : columns[i].Substring(1);
            bool ascending = columns[i].StartsWith("-") == false; // true: ascending - false: descending

            if (i == 0)
            {
                orderedQueryable = ApplyOrdering(source, columnName, ascending, isInitialOrder: true);
            }
            else
            {
                orderedQueryable = ApplyOrdering(orderedQueryable, columnName, ascending, isInitialOrder: false);
            }
        }

        return orderedQueryable!;
    }

    private static IOrderedQueryable<T> ApplyOrdering<T>(IQueryable<T> source, string columnName, bool ascending,
        bool isInitialOrder)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        MemberExpression property = Expression.Property(parameter, columnName);
        LambdaExpression lambda = Expression.Lambda(property, parameter);

        string methodName = isInitialOrder
            ? (ascending ? "OrderBy" : "OrderByDescending")
            : (ascending ? "ThenBy" : "ThenByDescending");

        MethodCallExpression methodCallExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), property.Type],
            source.Expression,
            Expression.Quote(lambda)
        );

        return (IOrderedQueryable<T>) source.Provider.CreateQuery<T>(methodCallExpression);
    }
}