namespace API.Extensions;

public static class ICollectionExtensions
{
    public static bool IsValidIndex<T>(this ICollection<T> collection, int index)
    {
        return index <= collection.Count - 1;
    }
    
    public static T? ElementAtOrDefault<T>(this ICollection<T> collection, int index)
    {
        if (!collection.IsValidIndex(index)) return default;
        return collection.ElementAt(index);
    }
}