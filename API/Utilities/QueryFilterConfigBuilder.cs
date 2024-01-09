using API.Utilities.Models;

namespace API.Utilities;

public static class QueryFilterConfigBuilder
{
    public static IEnumerable<QueryFilterOption> FromList (List<string> item)
    {
        for (int i = 0; i < item.Count(); i++)
        {
            yield return new QueryFilterOption()
            {
                Title = item[i],
                Parameter = new QueryFilterParam()
                {
                    Param = item[i]
                }
            };
        }
    }
}