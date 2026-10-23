using Shouldly;
using System.Text.Json;
using Wyrm.Math.Matrix;

namespace Wyrm.Math.UnitTests.Matrix;

public class GeneralMatrixDoubleJsonConverterTests
{
    [Fact]
    public void Write_Should_WriteMatrixAsJson()
    {
        var matrix = new GeneralMatrixDouble([[1.1, -2.2], [-1, -2]]);
        var json = JsonSerializer.Serialize(matrix);
        json.ShouldBe("[[1.1,-2.2],[-1,-2]]");
    }

    [Fact]
    public void Write_Should_WriteTestClassAsJson()
    {
        var testClass = new TestClass
        {
            Value = new GeneralMatrixDouble([[1.1, -2.2], [-1, -2]])
        };
        var json = JsonSerializer.Serialize(testClass);
        json.ShouldBe("{\"Value\":[[1.1,-2.2],[-1,-2]]}");
    }

    [Fact]
    public void Read_Should_ReadEmptyMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<GeneralMatrixDouble>("[]");
        matrix.ShouldBe(new GeneralMatrixDouble([]));
    }

    [Fact]
    public void Read_Should_ReadMatrixFromJson()
    {
        var matrix = JsonSerializer.Deserialize<GeneralMatrixDouble>("[[1.1,-2.2],[-1,-2]]");
        matrix.ShouldBe(new GeneralMatrixDouble([[1.1, -2.2], [-1, -2]]));
    }

    [Theory]
    [InlineData("[[1.1,-2.2],[-1,-2]")]
    [InlineData("[[1.1,-2.2],-1,-2]]")]
    [InlineData("[[1.1,-2.2][-1,-2]]")]
    [InlineData("[[1.1,-2.2,[-1,-2]]")]
    [InlineData("[1.1,-2.2],[-1,-2]]")]
    public void Read_Should_ThrowJsonExceptionForBadJson(string json)
    {
        Should.Throw<JsonException>(() => JsonSerializer.Deserialize<GeneralMatrixDouble>(json));
    }

    [Fact]
    public void Read_Should_ReadTestClassFromJson()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"Value\":[[1.1,-2.2],[-1,-2]]}");
        testClass.ShouldBeEquivalentTo(new TestClass
        {
            Value = new GeneralMatrixDouble([[1.1, -2.2], [-1, -2]])
        });
    }

    [Fact]
    public void Write_Should_Read_Empty()
    {
        var matrix = new GeneralMatrixDouble([]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<GeneralMatrixDouble>(json);
        result.ShouldBe(matrix);
    }

    [Fact]
    public void Write_Should_Read()
    {
        var matrix = new GeneralMatrixDouble([[1.1, -2.2], [-1, -2]]);
        var json = JsonSerializer.Serialize(matrix);
        var result = JsonSerializer.Deserialize<GeneralMatrixDouble>(json);
        result.ShouldBe(matrix);
    }

    #region Test Class

    private class TestClass
    {
        public GeneralMatrixDouble Value { get; set; }
    }

    #endregion
}
