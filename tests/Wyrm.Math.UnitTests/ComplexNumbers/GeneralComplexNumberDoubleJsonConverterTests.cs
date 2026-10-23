using Shouldly;
using System.Text.Json;
using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.UnitTests.ComplexNumbers;

public class GeneralComplexNumberDoubleJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteComplexAsJson()
    {
        var complexNumber = new GeneralComplexNumberDouble(1.1, 2.2);
        var json = JsonSerializer.Serialize(complexNumber);
        json.ShouldBe("[1.1,2.2]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new GeneralComplexNumberDouble(1.1, 2.2)
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[1.1,2.2]}");
    }

    [Fact]
    public void Read_Should_ReadComplexFromJson()
    {
        var complexNumber = JsonSerializer.Deserialize<GeneralComplexNumberDouble>("[1.1,2.2]");
        complexNumber.ShouldBe(new GeneralComplexNumberDouble(1.1, 2.2));
    }

    [Theory]
    [InlineData("[1.1,2.2")]
    [InlineData("1.1,2.2]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<GeneralComplexNumberDouble>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[1.1,2.2]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new GeneralComplexNumberDouble(1.1, 2.2)
        });
    }

    [Fact]
    public void Write_Should_Read()
    {
        var complexNumber = new GeneralComplexNumberDouble(1.1, -2.2);
        var json = JsonSerializer.Serialize(complexNumber);
        var result = JsonSerializer.Deserialize<GeneralComplexNumberDouble>(json);
        result.ShouldBe(complexNumber);
    }

    #region Test Class

    private class TestClass
    {
        public GeneralComplexNumberDouble Value { get; set; }
    }

    #endregion
}
