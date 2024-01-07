using API.Utilities.Models;

namespace API.Utilities;

public static class QueryFilterConfigBuilder
{
    public static IEnumerable<QueryFilterOption> FromList (List<string> categories)
    {
        for (int i = 0; i < categories.Count(); i++)
        {
            yield return new QueryFilterOption()
            {
                Title = categories[i],
                Parameter = new QueryFilterParam()
                {
                    Param = categories[i]
                }
            };
        }
    }
}