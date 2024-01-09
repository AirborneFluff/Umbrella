using API.Utilities.Models;

namespace API.Utilities;

public static class QueryFilterConfigBuilder
{
    public static IEnumerable<QueryFilterOption> FromList (List<string> options)
    {
        for (int i = 0; i < options.Count(); i++)
        {
            yield return new QueryFilterOption()
            {
                DisplayValue = options[i],
                Value = options[i].ToLower()
            };
        }
    }
}