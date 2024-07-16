using Shouldly;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.UnitTests.TestHelpers;

public static class GeneralMatrixDoubleExtensions
{
    public static void ShouldMatch(this GeneralMatrixDouble m1, GeneralMatrixDouble m2) =>
        m1.Matrix.ShouldMatch(m2.Matrix);

    internal static void ShouldMatch(this GeneralMatrix<double> m1, GeneralMatrix<double> m2)
    {
        m1.Columns.ShouldBe(m2.Columns);
        m1.Rows.ShouldBe(m2.Rows);
        m1.Values.SequenceEqual(m2.Values).ShouldBeTrue();
    }
}
