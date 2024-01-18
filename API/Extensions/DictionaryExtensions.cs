namespace API.Extensions;

public static class DictionaryExtensions
{
    public static string GetValueOrThrow(this Dictionary<string, string> dictionary, string key, string errorMessage)
    {
        return dictionary.TryGetValue(key, out var value) ? value : throw new Exception(errorMessage);
    }
}