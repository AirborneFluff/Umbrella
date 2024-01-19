namespace API.Extensions;

public static class DictionaryExtensions
{
    public static string GetValueOrThrow(this Dictionary<string, string> dictionary, string key, string errorMessage)
    {
        return dictionary.TryGetValue(key, out var value) ? value : throw new Exception(errorMessage);
    }

    public static void AddWithQuantity(this Dictionary<string, int> dictionary, string key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += 1;
            return;
        }
        dictionary.Add(key, 1);
    }

    public static void RemoveWithQuantity(this Dictionary<string, int> dictionary, string key)
    {
        if (!dictionary.TryGetValue(key, out var currentValue)) return;
        
        if (currentValue == 1)
        {
            dictionary.Remove(key);
            return;
        }
        dictionary[key] -= 1;
    }
}