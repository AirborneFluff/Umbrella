using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.ActionResults;

public sealed class ForbiddenObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status403Forbidden;

    /// <summary>
    /// Creates a new <see cref="ForbiddenObjectResult"/> instance.
    /// </summary>
    /// <param name="value">The value to format in the entity body.</param>
    public ForbiddenObjectResult([ActionResultObjectValue] object? value)
        : base(value)
    {
        StatusCode = DefaultStatusCode;
    }
}