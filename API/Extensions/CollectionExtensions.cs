namespace API.Extensions;

public static class CollectionExtensions
{
    public static void AddUnique<T>(this ICollection<T> collection, T item)
    {
        if (collection.Contains(item)) return;
        collection.Add(item);
    }
}