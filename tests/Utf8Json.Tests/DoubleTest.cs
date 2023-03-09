using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Utf8Json.Tests;

public class DoubleTest
{
    [MemberData(nameof(FractionalData))]
    [Theory]
    public void Deserialize_FractionalNumbers(string source, double target)
    {
        var instance = JsonSerializer.Deserialize<double>(source);
        instance.Is(target);
    }
    
    [MemberData(nameof(RoundData))]
    [Theory]
    public void Deserialize_RoundNumbers(string source, double target)
    {
        var instance = JsonSerializer.Deserialize<double>(source);
        instance.Is(target);
    }
    
    [MemberData(nameof(Serialize_FractionalData))]
    [Theory]
    public void Serialize_FractionalNumbers(double source, string target)
    {
        var instance = Encoding.UTF8.GetString(JsonSerializer.Serialize(source));
        instance.Is(target);
    }
    
    [MemberData(nameof(Serialize_RoundData))]
    [Theory]
    public void Serialize_RoundNumbers(double source, string target)
    {
        var instance = Encoding.UTF8.GetString(JsonSerializer.Serialize(source));
        instance.Is(target);
    }

    public static IEnumerable<object[]> FractionalData = new List<object[]>
    {
        // new object[] { "123123123123123.654", 123123123123123.654d },
        new object[] { "7777777777.123", 7777777777.123d },
        new object[] { "77777777777.123", 77777777777.123d },
        new object[] { "777777777778.123", 777777777778.123d },
        new object[] { "7777777777778.123", 7777777777778.123d },
        // new object[] { "-123123123123123.654", -123123123123123.654d },
        new object[] { "2000000.654", 2000000.654d },
        new object[] { "20000000.654", 20000000.654d },
        new object[] { "200000000.654", 200000000.654d },
        new object[] { "2000000000.654", 2000000000.654d },
        new object[] { "2000000005.654", 2000000005.654d },
        new object[] { "20000000050.654", 20000000050.654d },
        new object[] { "-984132156.987151", -984132156.987151d }
    };

    public static IEnumerable<object[]> Serialize_FractionalData = FractionalData.Select(p => new[] { p[1], p[0] });
        
    public static IEnumerable<object[]> RoundData = new List<object[]>
    {
        new object[] { "123123123123123.0", 123123123123123d },
        new object[] { "7777777777.0", 7777777777d },
        new object[] { "7777787777", 7777787777d },
        new object[] { "77777777777.0", 77777777777d },
        new object[] { "777377577378.0", 777377577378d },
        new object[] { "1561145.0", 1561145d },
        new object[] { "1561145", 1561145d },
        new object[] { "7777777777778.0", 7777777777778d },
        new object[] { "2000000.0", 2000000d },
        new object[] { "20000708.0", 20000708d },
        new object[] { "200005000.0",  200005000d },
        new object[] { "2000400000.0", 2000400000d },
        new object[] { "2000000005.0", 2000000005d },
        new object[] { "20100030050.0", 20100030050d },
        new object[] { "-984132156.0", -984132156d }
    };
    
    public static IEnumerable<object[]> Serialize_RoundData = RoundData.Select(p => new[] { p[1], (p[0] as string).Replace(".0", string.Empty) });
}