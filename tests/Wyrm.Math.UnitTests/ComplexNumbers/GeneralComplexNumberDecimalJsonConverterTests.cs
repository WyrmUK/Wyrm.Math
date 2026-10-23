using Shouldly;
using System.Text.Json;
using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.UnitTests.ComplexNumbers;

public class GeneralComplexNumberDecimalJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteComplexAsJson()
    {
        var complexNumber = new GeneralComplexNumberDecimal(1.1M, 2.2M);
        var json = JsonSerializer.Serialize(complexNumber);
        json.ShouldBe("[1.1,2.2]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new GeneralComplexNumberDecimal(1.1M, 2.2M)
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[1.1,2.2]}");
    }

    [Fact]
    public void Read_Should_ReadComplexFromJson()
    {
        var complexNumber = JsonSerializer.Deserialize<GeneralComplexNumberDecimal>("[1.1,2.2]");
        complexNumber.ShouldBe(new GeneralComplexNumberDecimal(1.1M, 2.2M));
    }

    [Theory]
    [InlineData("[1.1,2.2")]
    [InlineData("1.1,2.2]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<GeneralComplexNumberDecimal>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[1.1,2.2]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new GeneralComplexNumberDecimal(1.1M, 2.2M)
        });
    }

    [Fact]
    public void Write_Should_Read()
    {
        var complexNumber = new GeneralComplexNumberDecimal(1.1M, -2.2M);
        var json = JsonSerializer.Serialize(complexNumber);
        var result = JsonSerializer.Deserialize<GeneralComplexNumberDecimal>(json);
        result.ShouldBe(complexNumber);
    }

    #region Test Class

    private class TestClass
    {
        public GeneralComplexNumberDecimal Value { get; set; }
    }

    #endregion
}
