using System.Collections.Generic;
using Xunit;

namespace Utf8Json.Tests
{
    public class DecimalTest
    {
        private const string MaxOverflowString = "79228162514264337593543950335.0";
        private const string MinOverflowString = "-79228162514264337593543950335.0";

        public class Foo
        {
            public decimal Bar { get; set; }
            public string More { get; set; }
        }

        [Fact]
        public void D()
        {
            var d = decimal.MaxValue;
            var bin = JsonSerializer.Serialize(d);
            JsonSerializer.Deserialize<decimal>(bin).Is(d);

            var foo = JsonSerializer.Serialize(new Foo { Bar = -31.42323m, More = "mmmm" });
            var ddd = JsonSerializer.Deserialize<Foo>(foo);
            ddd.Bar.Is(-31.42323m);
            ddd.More.Is("mmmm");

            JsonSerializer.Deserialize<Foo>("{\"Bar\":1.23}").Bar.Is(1.23m);
        }

        [Fact]
        public void Overflow_Maximum()
        {
            JsonSerializer.Deserialize<decimal>(MaxOverflowString).Is(decimal.MaxValue);
        }
        
        [Fact]
        public void Overflow_Minimum()
        {
            JsonSerializer.Deserialize<decimal>(MinOverflowString).Is(decimal.MinValue);
        }

        [MemberData(nameof(FractionalData))]
        [Theory]
        public void FractionalNumbers(string source, decimal target)
        {
            var instance = JsonSerializer.Deserialize<decimal>(source);
            instance.Is(target);
        }
        
        [MemberData(nameof(RoundData))]
        [Theory]
        public void RoundNumbers(string source, decimal target)
        {
            var instance = JsonSerializer.Deserialize<decimal>(source);
            instance.Is(target);
        }

        public static IEnumerable<object[]> FractionalData = new List<object[]>
        {
            new object[] { "123123123123123.654",  123123123123123.654m },
            new object[] { "7777777777.123", 7777777777.123m },
            new object[] { "77777777777.123", 77777777777.123m },
            new object[] { "777777777778.123", 777777777778.123m },
            new object[] { "7777777777778.123", 7777777777778.123m },
            new object[] { "-123123123123123.654", -123123123123123.654m },
            new object[] { "2000000.654", 2000000.654m },
            new object[] { "20000000.654", 20000000.654m },
            new object[] { "200000000.654", 200000000.654m },
            new object[] { "2000000000.654", 2000000000.654m },
            new object[] { "2000000005.654", 2000000005.654m },
            new object[] { "20000000050.654", 20000000050.654m },
            new object[] { "-984132156.987151", -984132156.987151m }
        };
        
        public static IEnumerable<object[]> RoundData = new List<object[]>
        {
            new object[] { "123123123123123.0", 123123123123123m },
            new object[] { "7777777777.0", 7777777777m },
            new object[] { "7777787777", 7777787777m },
            new object[] { "77777777777.0", 77777777777m },
            new object[] { "777377577378.0", 777377577378m },
            new object[] { "1561145.0", 1561145m },
            new object[] { "1561145", 1561145m },
            new object[] { "7777777777778.0", 7777777777778m },
            new object[] { "2000000.0", 2000000m },
            new object[] { "20000708.0", 20000708m },
            new object[] { "200005000.0",  200005000m },
            new object[] { "2000400000.0", 2000400000m },
            new object[] { "2000000005.0", 2000000005m },
            new object[] { "20100030050.0", 20100030050m },
            new object[] { "-984132156.0", -984132156m }
        };
    }
}
