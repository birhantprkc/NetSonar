using System.Formats.Asn1;
using System.Net;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace NetSonar.Avalonia.Converters;

public class IPEndPointJsonConverter : JsonConverter<IPEndPoint>
{
    public override IPEndPoint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var token = reader.GetString();

        if (reader.TokenType != JsonTokenType.String || string.IsNullOrEmpty(token))
        {
            return null;
        }

        return IPEndPoint.Parse(token);
    }

    public override void Write(Utf8JsonWriter writer, IPEndPoint? value, JsonSerializerOptions options)
    {
        var ipAddressString = string.Empty;

        if (value != null)
        {
            ipAddressString = value.ToString();
        }

        writer.WriteStringValue(ipAddressString);
    }
}