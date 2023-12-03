using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Azure.Cosmos;

namespace API.ActionResults;

public sealed class CosmosExceptionResult : ObjectResult
{
    /// <summary>
    /// Creates a new <see cref="ForbiddenObjectResult"/> instance.
    /// </summary>
    /// <param name="e">The exception from which to gather error information</param>
    public CosmosExceptionResult(CosmosException e)
        : base(e.Message)
    {
        StatusCode = (int) e.StatusCode;
    }
}