using Shouldly;
using System.Text.Json;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;

namespace Wyrm.Math.UnitTests.Matrix;

public class ComplexMatrixDoubleJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteMatrixAsJson()
    {
        var matrix = new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1.1, 0.1), new GeneralComplexNumberDouble(-2.2, -0.2)], [new GeneralComplexNumberDouble(-1, 0), new GeneralComplexNumberDouble(-2, 0)]]);
        var json = JsonSerializer.Serialize(matrix);
        json.ShouldBe("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1.1, 0.1), new GeneralComplexNumberDouble(-2.2, -0.2)], [new GeneralComplexNumberDouble(-1, 0), new GeneralComplexNumberDouble(-2, 0)]])
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]}");
    }

    [Fact]
    public void Read_Should_ReadEmptyMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<ComplexMatrixDouble>("[]");
        matrix.ShouldBe(new ComplexMatrixDouble([]));
    }

    [Fact]
    public void Read_Should_ReadMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<ComplexMatrixDouble>("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]");
        matrix.ShouldBe(new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1.1, 0.1), new GeneralComplexNumberDouble(-2.2, -0.2)], [new GeneralComplexNumberDouble(-1, 0), new GeneralComplexNumberDouble(-2, 0)]]));
    }

    [Theory]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]],[-1,0],[-2,0]]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2]][[-1,0],[-2,0]]]")]
    [InlineData("[[[1.1,0.1],[-2.2,-0.2],[[-1,0],[-2,0]]]")]
    [InlineData("[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<ComplexMatrixDouble>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[[[1.1,0.1],[-2.2,-0.2]],[[-1,0],[-2,0]]]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1.1, 0.1), new GeneralComplexNumberDouble(-2.2, -0.2)], [new GeneralComplexNumberDouble(-1, 0), new GeneralComplexNumberDouble(-2, 0)]])
        });
    }

    [Fact]
    public void Write_Should_Read_Empty()
    {
        var matrix = new ComplexMatrixDouble([]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<ComplexMatrixDouble>(json);
        result.ShouldBe(matrix);
    }

    [Fact]
    public void Write_Should_Read()
    {
        var matrix = new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1.1, 0.1), new GeneralComplexNumberDouble(-2.2, -0.2)], [new GeneralComplexNumberDouble(-1, 0), new GeneralComplexNumberDouble(-2, 0)]]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<ComplexMatrixDouble>(json);
        result.ShouldBe(matrix);
    }

    #region Test Class

    private class TestClass
    {
        public ComplexMatrixDouble Value { get; set; }
    }

    #endregion
}
