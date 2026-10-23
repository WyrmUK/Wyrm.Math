using Shouldly;
using System.Text.Json;
using Wyrm.Math.Matrix;

namespace Wyrm.Math.UnitTests.Matrix;

public class GeneralMatrixDecimalJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteMatrixAsJson()
    {
        var matrix = new GeneralMatrixDecimal([[1.1M, -2.2M], [-1M, -2M]]);
        var json = JsonSerializer.Serialize(matrix);
        json.ShouldBe("[[1.1,-2.2],[-1,-2]]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new GeneralMatrixDecimal([[1.1M, -2.2M], [-1M, -2M]])
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[[1.1,-2.2],[-1,-2]]}");
    }

    [Fact]
    public void Read_Should_ReadEmptyMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<GeneralMatrixDecimal>("[]");
        matrix.ShouldBe(new GeneralMatrixDecimal([]));
    }

    [Fact]
    public void Read_Should_ReadMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<GeneralMatrixDecimal>("[[1.1,-2.2],[-1,-2]]");
        matrix.ShouldBe(new GeneralMatrixDecimal([[1.1M, -2.2M], [-1M, -2M]]));
    }

    [Theory]
    [InlineData("[[1.1,-2.2],[-1,-2]")]
    [InlineData("[[1.1,-2.2],-1,-2]]")]
    [InlineData("[[1.1,-2.2][-1,-2]]")]
    [InlineData("[[1.1,-2.2,[-1,-2]]")]
    [InlineData("[1.1,-2.2],[-1,-2]]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<GeneralMatrixDecimal>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[[1.1,-2.2],[-1,-2]]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new GeneralMatrixDecimal([[1.1M, -2.2M], [-1M, -2M]])
        });
    }

    [Fact]
    public void Write_Should_Read_Empty()
    {
        var matrix = new GeneralMatrixDecimal([]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<GeneralMatrixDecimal>(json);
        result.ShouldBe(matrix);
    }

    [Fact]
    public void Write_Should_Read()
    {
        var matrix = new GeneralMatrixDecimal([[1.1M, -2.2M], [-1M, -2M]]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<GeneralMatrixDecimal>(json);
        result.ShouldBe(matrix);
    }

    #region Test Class

    private class TestClass
    {
        public GeneralMatrixDecimal Value { get; set; }
    }

    #endregion
}
