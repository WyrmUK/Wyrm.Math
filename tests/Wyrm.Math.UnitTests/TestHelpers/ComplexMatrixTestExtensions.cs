using Shouldly;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.UnitTests.TestHelpers;

internal static class ComplexMatrixTestExtensions
{
    public static void ShouldMatch(this ComplexMatrixDouble m1, ComplexMatrixDouble m2) =>
        m1.Matrix.ShouldMatch(m2.Matrix);

    internal static void ShouldMatch(this GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2)
    {
        m1.Columns.ShouldBe(m2.Columns);
        m1.Rows.ShouldBe(m2.Rows);
        m1.Values.SequenceEqual(m2.Values).ShouldBeTrue();
    }

    public static void ShouldMatch(this ComplexMatrixDecimal m1, ComplexMatrixDecimal m2) =>
        m1.Matrix.ShouldMatch(m2.Matrix);

    internal static void ShouldMatch(this GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2)
    {
        m1.Columns.ShouldBe(m2.Columns);
        m1.Rows.ShouldBe(m2.Rows);
        m1.Values.SequenceEqual(m2.Values).ShouldBeTrue();
    }
}
