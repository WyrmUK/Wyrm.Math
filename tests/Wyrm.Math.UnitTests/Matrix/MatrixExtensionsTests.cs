using Shouldly;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;

namespace Wyrm.Math.UnitTests.Matrix;

public class MatrixExtensionsTests
{
    [Fact]
    public void AsMatrix_Should_Create_Decimal_Matrix()
    {
        List<List<decimal>> values = [[1M, 2M], [3M, 4M, 5M], [6M, 7M]];
        var result = values.AsMatrix();
        result.ShouldBeEquivalentTo(new GeneralMatrixDecimal(values));
    }

    [Fact]
    public void AsMatrix_Should_Create_Double_Matrix()
    {
        List<List<double>> values = [[1.0, 2.0], [3.0, 4.0, 5.0], [6.0, 7.0]];
        var result = values.AsMatrix();
        result.ShouldBeEquivalentTo(new GeneralMatrixDouble(values));
    }

    [Fact]
    public void AsMatrix_Should_Create_Complex_Decimal_Matrix()
    {
        List<List<GeneralComplexNumberDecimal>> values = [[new GeneralComplexNumberDecimal(1M, 1.1M), new GeneralComplexNumberDecimal(2M, 2.2M)], [new GeneralComplexNumberDecimal(3M, 3.3M), new GeneralComplexNumberDecimal(4M, 4.4M), new GeneralComplexNumberDecimal(5M, 5.5M)], [new GeneralComplexNumberDecimal(6M, 6.6M), new GeneralComplexNumberDecimal(7M, 7.7M)]];
        var result = values.AsMatrix();
        result.ShouldBeEquivalentTo(new ComplexMatrixDecimal(values));
    }

    [Fact]
    public void AsMatrix_Should_Create_Complex_Double_Matrix()
    {
        List<List<GeneralComplexNumberDouble>> values = [[new GeneralComplexNumberDouble(1.0, 1.1), new GeneralComplexNumberDouble(2.0, 2.2)], [new GeneralComplexNumberDouble(3.0, 3.3), new GeneralComplexNumberDouble(4.0, 4.4), new GeneralComplexNumberDouble(5.0, 5.5)], [new GeneralComplexNumberDouble(6.0, 6.6), new GeneralComplexNumberDouble(7.0, 7.7)]];
        var result = values.AsMatrix();
        result.ShouldBeEquivalentTo(new ComplexMatrixDouble(values));
    }
}
