using System.Security.Cryptography;
using System.Text.Json;

namespace API.Helpers;

public static class EnumUtilities
{
    public static string GetNameAndValueHash<T>() where T : Enum
    {
        var nameBytes = JsonSerializer.SerializeToUtf8Bytes(Enum.GetNames(typeof(T)));
        var valueBytes = JsonSerializer.SerializeToUtf8Bytes(Enum.GetValues(typeof(T)));
        var hashBytes = MD5.HashData(nameBytes.Concat(valueBytes).ToArray());
        return BitConverter.ToString(hashBytes).Replace("-", "");
    }
}