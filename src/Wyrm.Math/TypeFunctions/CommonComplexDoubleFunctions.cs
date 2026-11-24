using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.TypeFunctions;

internal class CommonComplexDoubleFunctions : IComplexTypeFunctions<GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    protected static readonly GeneralComplexNumberDouble ZeroValue = new(0D, 0D);
    private const double Tolerance = 1e-10;

    public bool Eqz(GeneralComplexNumberDouble v) => v == ZeroValue;
    public bool Ltz(GeneralComplexNumberDouble v) => v.Real < ZeroValue.Real;
    public GeneralComplexNumberDouble Neg(GeneralComplexNumberDouble v) => -v;
    public GeneralComplexNumberDouble Add(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 * v2;
    public GeneralComplexNumberDouble HalfOf(GeneralComplexNumberDouble v) => v / 2.0;
    public GeneralComplexNumberDouble Div(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 / v2;
    public GeneralComplexNumberDouble Sqrt(GeneralComplexNumberDouble v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDouble v) => v.Abs() > Tolerance;

    public GeneralMatrix<GeneralComplexNumberDouble> Sub(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) - new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m, GeneralComplexNumberDouble v) => (new ComplexMatrixDouble(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) * new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Div(GeneralMatrix<GeneralComplexNumberDouble> m, GeneralComplexNumberDouble v) => (new ComplexMatrixDouble(m) / v).Matrix;
    public bool IsInteger(GeneralComplexNumberDouble v) => v.Imaginary == ZeroValue.Imaginary && double.IsInteger(v.Real);

    public ComplexMatrixDouble ComplexMatrix(GeneralMatrix<GeneralComplexNumberDouble> m) => new(m);
}
