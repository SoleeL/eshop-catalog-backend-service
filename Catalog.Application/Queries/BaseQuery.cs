using MediatR;

namespace Catalog.Application.Queries;

public abstract class BaseQuery<TResponse> : IRequest<TResponse>
{
    // public QueryParameters QueryParameters { get; set; }
    //
    // protected BaseQuery(QueryParameters queryParameters)
    // {
    //     QueryParameters = queryParameters;
    // }

    protected List<string> ParseToList(string? parameters)
    {
        if (parameters == null) return new List<string>();

        return parameters.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(value => value.Trim())
            .ToList();
    }
}