using System.Buffers;
using System.Text;
using System.Text.Json;

namespace NIBAUTH.Application.Common.Utilities;

public class JsonObject
{
    public static T Deserialize<T>(byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(bytes)!;
    }

    public static T Deserialize<T>(string jsonString)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
        return JsonSerializer.Deserialize<T>(bytes)!;
    }

    public static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(buffer);
        JsonSerializer.Serialize(writer, value);
        return buffer.WrittenSpan.ToArray();
    }
}
