using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class GeneralComplexNumberDecimalFunctions : ITypeFunctions<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    private static readonly GeneralComplexNumberDecimal ZeroValue = new(0M, 0M);
    private static readonly GeneralComplexNumberDecimal OneValue = new(1M, 0M);
    private static readonly GeneralComplexNumberDecimal FourValue = new(4M, 0M);

    public GeneralComplexNumberDecimal One() => OneValue;
    public GeneralComplexNumberDecimal Four() => FourValue;
    public bool Eqz(GeneralComplexNumberDecimal v) => v == ZeroValue;
    public bool Nez(GeneralComplexNumberDecimal v) => v != ZeroValue;
    public bool Eq1(GeneralComplexNumberDecimal v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDecimal v) => v != OneValue;
    public GeneralComplexNumberDecimal Neg(GeneralComplexNumberDecimal v) => -v;
    public GeneralComplexNumberDecimal Add(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Sub(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 - v2;
    public GeneralComplexNumberDecimal Mul(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 * v2;
    public GeneralComplexNumberDecimal Div(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 / v2;
    public GeneralComplexNumberDecimal Mod(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => (GeneralComplexNumberDecimal)(v1.Real % v2.Real);

    public GeneralComplexNumberDecimal Cast(GeneralComplexNumberDecimal v) => v;
    public bool Ltz(GeneralComplexNumberDecimal v) => v.Real < 0M;
    public GeneralComplexNumberDecimal Abs(GeneralComplexNumberDecimal v) => (GeneralComplexNumberDecimal)v.Abs();
    public GeneralComplexNumberDecimal HalfOf(GeneralComplexNumberDecimal v) => v / 2M;
    public GeneralComplexNumberDecimal Sqrt(GeneralComplexNumberDecimal v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDecimal v) => v.Abs() > (decimal)1e-10;
    public GeneralMatrix<GeneralComplexNumberDecimal> Cast(GeneralMatrix<GeneralComplexNumberDecimal> m) => new ComplexMatrixDecimal(m).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Sub(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) - new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralComplexNumberDecimal v, GeneralMatrix<GeneralComplexNumberDecimal> m) => (v * new ComplexMatrixDecimal(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) * new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Div(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) / v).Matrix;
    public bool IsInteger(GeneralComplexNumberDecimal v) => v.Imaginary == 0M && decimal.IsInteger(v.Real);

    public ComplexMatrixDecimal ComplexMatrix(GeneralMatrix<GeneralComplexNumberDecimal> m) => new(m);
}
