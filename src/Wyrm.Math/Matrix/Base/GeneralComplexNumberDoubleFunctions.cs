using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.Matrix.Base;

internal class GeneralComplexNumberDoubleFunctions : ITypeFunctions<GeneralComplexNumberDouble, GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    private static readonly GeneralComplexNumberDouble ZeroValue = new(0D, 0D);
    private static readonly GeneralComplexNumberDouble OneValue = new(1D, 0D);
    private static readonly GeneralComplexNumberDouble FourValue = new(4D, 0D);

    public GeneralComplexNumberDouble One() => OneValue;
    public GeneralComplexNumberDouble Four() => FourValue;
    public bool Eqz(GeneralComplexNumberDouble v) => v == ZeroValue;
    public bool Nez(GeneralComplexNumberDouble v) => v != ZeroValue;
    public bool Eq1(GeneralComplexNumberDouble v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDouble v) => v != OneValue;
    public GeneralComplexNumberDouble Neg(GeneralComplexNumberDouble v) => -v;
    public GeneralComplexNumberDouble Add(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Sub(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 - v2;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 * v2;
    public GeneralComplexNumberDouble Div(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 / v2;

    public GeneralComplexNumberDouble Cast(GeneralComplexNumberDouble v) => v;
    public GeneralComplexNumberDouble HalfOf(GeneralComplexNumberDouble v) => v / 2.0;
    public GeneralComplexNumberDouble Sqrt(GeneralComplexNumberDouble v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDouble v) => v.Abs() > 1e-10;
    public GeneralMatrix<GeneralComplexNumberDouble> Cast(GeneralMatrix<GeneralComplexNumberDouble> m) => new ComplexMatrixDouble(m).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Sub(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) - new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralComplexNumberDouble v, GeneralMatrix<GeneralComplexNumberDouble> m) => (v * new ComplexMatrixDouble(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m, GeneralComplexNumberDouble v) => (new ComplexMatrixDouble(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) * new ComplexMatrixDouble(m2)).Matrix;

    public ComplexMatrixDouble ComplexMatrix(GeneralMatrix<GeneralComplexNumberDouble> m) => new(m);
}
