using System;

namespace Utf8Json.Tests.Data;

internal class RawJsonWrapper
{
    public byte[] Data { get; set; }

    public RawJsonWrapper()
    {
        Data = Array.Empty<byte>();
    }

    public static implicit operator byte[](RawJsonWrapper w)
    {
        return w.Data;
    }
		
    public static implicit operator RawJsonWrapper(byte[] w)
    {
        return new() { Data = w };
    }
}