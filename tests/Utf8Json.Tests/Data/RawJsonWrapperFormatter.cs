using System;

namespace Utf8Json.Tests.Data;

internal class RawJsonWrapperFormatter : IJsonFormatter<RawJsonWrapper>
{
    public void Serialize(ref JsonWriter writer, RawJsonWrapper value, IJsonFormatterResolver formatterResolver)
    {
        if (ReferenceEquals(value, null) || ReferenceEquals(value.Data, null) || value.Data == Array.Empty<byte>())
        {
            writer.WriteNull();
            return;
        }
			
        writer.WriteRaw(value.Data);
    }

    public RawJsonWrapper Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
    {
        return new()
        {
            Data = reader.ReadIsNull() ? Array.Empty<byte>() : reader.ReadNextBlockSegment().ToArray()
        };
    }
}