using System.Text.Json;
using System.Text.Json.Serialization;

namespace Metafar.Challenge.Infrastructure.Utility;

/// <summary>
/// Contains customs configuration for serializing and deserialize objects
/// </summary>
public class JsonSerializerUtility
{
    /// <summary>
    /// Configuration to set 
    /// </summary>
    public static JsonSerializerOptions SerializeOptions { get; set; } = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// Used to transform properties of an object in camel case format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectToConvert"></param>
    /// <returns>Object as a string</returns>
    public static string SetObjectPropertiesToCamelCase<T>(T objectToConvert)
    {
        return JsonSerializer.Serialize(objectToConvert, SerializeOptions);
    }
}