using Shouldly;
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
}
