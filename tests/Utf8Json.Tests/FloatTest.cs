using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Utf8Json.Tests;

public class FloatTest
{
    [MemberData(nameof(FractionalData))]
    [Theory]
    public void Deserialize_FractionalNumbers(string source, float target)
    {
        var instance = JsonSerializer.Deserialize<float>(source);
        instance.Is(target);
    }
    
    [MemberData(nameof(RoundData))]
    [Theory]
    public void Deserialize_RoundNumbers(string source, float target)
    {
        var instance = JsonSerializer.Deserialize<float>(source);
        instance.Is(target);
    }
    
    [MemberData(nameof(Serialize_FractionalData))]
    [Theory]
    public void Serialize_FractionalNumbers(float source, string target)
    {
        var instance = Encoding.UTF8.GetString(JsonSerializer.Serialize(source));
        instance.Is(target);
    }
    
    [MemberData(nameof(Serialize_RoundData))]
    [Theory]
    public void Serialize_RoundNumbers(float source, string target)
    {
        var instance = Encoding.UTF8.GetString(JsonSerializer.Serialize(source));
        instance.Is(target);
    }


    public static IEnumerable<object[]> FractionalData = new List<object[]>
    {
        new object[] { "123123123123123.654", 123123123123123.654f },
        new object[] { "7777777777.123", 7777777777.123f },
        new object[] { "77777777777.123", 77777777777.123f },
        new object[] { "777777777778.123", 777777777778.123f },
        new object[] { "7777777777778.123", 7777777777778.123f },
        new object[] { "-123123123123123.654", -123123123123123.654f },
        new object[] { "2000000.654", 2000000.654f },
        new object[] { "20000000.654", 20000000.654f },
        new object[] { "200000000.654", 200000000.654f },
        new object[] { "2000000000.654", 2000000000.654f },
        new object[] { "2000000005.654", 2000000005.654f },
        new object[] { "20000000050.654", 20000000050.654f },
        new object[] { "-984132156.987151", -984132156.987151f }
    };
    
    public static IEnumerable<object[]> Serialize_FractionalData = FractionalData.Select(p => new[] { p[1], p[0] });
        
    public static IEnumerable<object[]> RoundData = new List<object[]>
    {
        new object[] { "123123123123123.0", 123123123123123f },
        new object[] { "7777777777.0", 7777777777f },
        new object[] { "7777787777", 7777787777f },
        new object[] { "77777777777.0", 77777777777f },
        new object[] { "777377577378.0", 777377577378f },
        new object[] { "1561145.0", 1561145f },
        new object[] { "1561145", 1561145f },
        new object[] { "7777777777778.0", 7777777777778f },
        new object[] { "2000000.0", 2000000f },
        new object[] { "20000708.0", 20000708f },
        new object[] { "200005000.0",  200005000f },
        new object[] { "2000400000.0", 2000400000f },
        new object[] { "2000000005.0", 2000000005f },
        new object[] { "20100030050.0", 20100030050f },
        new object[] { "-984132156.0", -984132156f },
        new object[] { "-984132156", -984132156f }
    };
    
    public static IEnumerable<object[]> Serialize_RoundData = RoundData.Select(p => new[] { p[1], (p[0] as string).Replace(".0", string.Empty) });
}