using System.Text.Json;

namespace PL.DataAccess.TestApp;

public class Constants
{
    public static JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new JsonSerializerOptions
    {
        // property's name uses a case-insensitive comparison during deserialization
        PropertyNameCaseInsensitive = true,

        // pretty printed on serialization
        WriteIndented = true,

        // CamelCase
        //PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
        //DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,

        // UpperCase
        PropertyNamingPolicy = new UpperCaseNamingPolicy(),
        DictionaryKeyPolicy = new UpperCaseNamingPolicy(),
    };
}
