using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class CommonComplexDecimalFunctions : IComplexTypeFunctions<GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    protected static readonly GeneralComplexNumberDecimal ZeroValue = new(0M, 0M);
    private const decimal Tolerance = 1e-10M;

    public bool Eqz(GeneralComplexNumberDecimal v) => v == ZeroValue;
    public bool Ltz(GeneralComplexNumberDecimal v) => v.Real < ZeroValue.Real;
    public GeneralComplexNumberDecimal Neg(GeneralComplexNumberDecimal v) => -v;
    public GeneralComplexNumberDecimal Add(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Mul(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 * v2;
    public GeneralComplexNumberDecimal HalfOf(GeneralComplexNumberDecimal v) => v / 2M;
    public GeneralComplexNumberDecimal Div(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 / v2;
    public GeneralComplexNumberDecimal Sqrt(GeneralComplexNumberDecimal v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDecimal v) => v.Abs() > Tolerance;

    public GeneralMatrix<GeneralComplexNumberDecimal> Sub(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) - new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralMatrix<GeneralComplexNumberDecimal> m1, GeneralMatrix<GeneralComplexNumberDecimal> m2) => (new ComplexMatrixDecimal(m1) * new ComplexMatrixDecimal(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDecimal> Div(GeneralMatrix<GeneralComplexNumberDecimal> m, GeneralComplexNumberDecimal v) => (new ComplexMatrixDecimal(m) / v).Matrix;
    public bool IsInteger(GeneralComplexNumberDecimal v) => v.Imaginary == 0M && decimal.IsInteger(v.Real);

    public ComplexMatrixDecimal ComplexMatrix(GeneralMatrix<GeneralComplexNumberDecimal> m) => new(m);
}
