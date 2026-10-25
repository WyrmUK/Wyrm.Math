using Shouldly;
using System.Text.Json;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;

namespace Wyrm.Math.UnitTests.Matrix;

public class ComplexMatrixDecimalJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteMatrixAsJson()
    {
        var matrix = new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1.1M, 0.1M), new GeneralComplexNumberDecimal(-2.2M, -0.2M)], [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M)]]);
        var json = JsonSerializer.Serialize(matrix);
        json.ShouldBe("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1.1M, 0.1M), new GeneralComplexNumberDecimal(-2.2M, -0.2M)], [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M)]])
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]}");
    }

    [Fact]
    public void Read_Should_ReadEmptyMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<ComplexMatrixDecimal>("[]");
        matrix.ShouldBe(new ComplexMatrixDecimal([]));
    }

    [Fact]
    public void Read_Should_ReadMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<ComplexMatrixDecimal>("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]");
        matrix.ShouldBe(new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1.1M, 0.1M), new GeneralComplexNumberDecimal(-2.2M, -0.2M)], [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M)]]));
    }

    [Theory]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]],[-1,0],[-2,0]]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]][[-1,0],[-2,0]]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2],[[-1,0],[-2,0]]]")]
    [InlineData("[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ComplexMatrixDecimal>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1.1M, 0.1M), new GeneralComplexNumberDecimal(-2.2M, -0.2M)], [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M)]])
        });
    }

    [Fact]
    public void Write_Should_Read_Empty()
    {
        var matrix = new ComplexMatrixDecimal([]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<ComplexMatrixDecimal>(json);
        result.ShouldBe(matrix);
    }

    [Fact]
    public void Write_Should_Read()
    {
        var matrix = new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1.1M, 0.1M), new GeneralComplexNumberDecimal(-2.2M, -0.2M)], [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M)]]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<ComplexMatrixDecimal>(json);
        result.ShouldBe(matrix);
    }

    #region Test Class

    private class TestClass
    {
        public ComplexMatrixDecimal Value { get; set; }
    }

    #endregion
}
