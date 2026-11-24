using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class DecimalFunctions : CommonComplexDecimalFunctions, ITypeFunctions<decimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>
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
    public decimal Mod(decimal v1, decimal v2) => v1 % v2;

    public GeneralComplexNumberDecimal Cast(decimal v) => (GeneralComplexNumberDecimal)v;
    public decimal Abs(GeneralComplexNumberDecimal v) => v.Abs();
    public GeneralComplexNumberDecimal Add(decimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Sqrt(decimal v) => v.Sqrti();
    public GeneralMatrix<GeneralComplexNumberDecimal> CopyAs(GeneralMatrix<decimal> m) => new(m.Columns, [.. m.Values.Select(x => (GeneralComplexNumberDecimal)x)]);
    public GeneralMatrix<GeneralComplexNumberDecimal> Sub(GeneralMatrix<decimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => ((ComplexMatrixDecimal)new GeneralMatrixDecimal(m1) - new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralComplexNumberDecimal v, GeneralMatrix<decimal> m) => (v * (ComplexMatrixDecimal)new GeneralMatrixDecimal(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Div(GeneralMatrix<GeneralComplexNumberDecimal> m, decimal v) => (new ComplexMatrixDecimal(m) / (GeneralComplexNumberDecimal)v).Matrix;
}
