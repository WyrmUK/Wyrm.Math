using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class DecimalFunctions : ITypeFunctions<decimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    public decimal One() => 1M;
    public decimal Four() => 4M;
    public bool Eqz(decimal v) => v == 0M;
    public bool Nez(decimal v) => v != 0M;
    public bool Eq1(decimal v) => v == 1M;
    public bool Ne1(decimal v) => v != 1M;
    public decimal Neg(decimal v) => -v;
    public decimal Add(decimal v1, decimal v2) => v1 + v2;
    public decimal Sub(decimal v1, decimal v2) => v1 - v2;
    public decimal Mul(decimal v1, decimal v2) => v1 * v2;
    public decimal Div(decimal v1, decimal v2) => v1 / v2;

    public GeneralComplexNumberDecimal Cast(decimal v) => (GeneralComplexNumberDecimal)v;
    public GeneralComplexNumberDecimal Neg(GeneralComplexNumberDecimal v) => -v;
    public GeneralComplexNumberDecimal Add(decimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal HalfOf(GeneralComplexNumberDecimal v) => v / 2M;
    public GeneralComplexNumberDecimal Sqrt(decimal v) => v.Sqrti();

    public ComplexMatrixDecimal ComplexMatrix(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> v) => new ComplexMatrixDecimal(v);
}
