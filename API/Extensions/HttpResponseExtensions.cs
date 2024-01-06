using System.Text.Json;
using API.Interfaces;
using API.Utilities.Headers;

namespace API.Extensions;

public static class HttpResponseExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, IPagedList list)
    {
        var pageHeader = new PaginationHeader(list);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Add("Pagination", JsonSerializer.Serialize(pageHeader, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}