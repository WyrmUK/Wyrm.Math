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
    public decimal Mod(decimal v1, decimal v2) => v1 % v2;

    public GeneralComplexNumberDecimal Cast(decimal v) => (GeneralComplexNumberDecimal)v;
    public bool Ltz(GeneralComplexNumberDecimal v) => v.Real < 0M;
    public bool Eqz(GeneralComplexNumberDecimal v) => v == (GeneralComplexNumberDecimal)0M;
    public GeneralComplexNumberDecimal Neg(GeneralComplexNumberDecimal v) => -v;
    public decimal Abs(GeneralComplexNumberDecimal v) => v.Abs();
    public GeneralComplexNumberDecimal Add(decimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Add(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Mul(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 * v2;
    public GeneralComplexNumberDecimal HalfOf(GeneralComplexNumberDecimal v) => v / 2M;
    public GeneralComplexNumberDecimal Div(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 / v2;
    public GeneralComplexNumberDecimal Sqrt(decimal v) => v.Sqrti();
    public GeneralComplexNumberDecimal Sqrt(GeneralComplexNumberDecimal v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDecimal v) => v.Abs() > (decimal)1e-10;
    public GeneralMatrix<GeneralComplexNumberDecimal> Cast(GeneralMatrix<decimal> m) => ((ComplexMatrixDecimal)new GeneralMatrixDecimal(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Sub(GeneralMatrix<decimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => ((ComplexMatrixDecimal)new GeneralMatrixDecimal(m1) - new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Sub(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) - new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralComplexNumberDecimal v, GeneralMatrix<decimal> m) => (v * (ComplexMatrixDecimal)new GeneralMatrixDecimal(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) * new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Div(GeneralMatrix<GeneralComplexNumberDecimal> m, decimal v) => (new ComplexMatrixDecimal(m) / (GeneralComplexNumberDecimal)v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Div(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) / v).Matrix;
    public bool IsInteger(GeneralComplexNumberDecimal v) => v.Imaginary == 0M && decimal.IsInteger(v.Real);

    public ComplexMatrixDecimal ComplexMatrix(GeneralMatrix<GeneralComplexNumberDecimal> m) => new(m);
}
