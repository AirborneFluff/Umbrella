namespace API.Extensions;

public static class ICollectionExtensions
{
    public static bool IsValidIndex<T>(this ICollection<T> collection, int index)
    {
        return index <= collection.Count - 1;
    }
}