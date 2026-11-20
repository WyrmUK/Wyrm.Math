using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class DoubleFunctions : ITypeFunctions<double, GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    public double One() => 1D;
    public double Four() => 4D;
    public bool Eqz(double v) => v == 0D;
    public bool Nez(double v) => v != 0D;
    public bool Eq1(double v) => v == 1D;
    public bool Ne1(double v) => v != 1D;
    public double Neg(double v) => -v;
    public double Add(double v1, double v2) => v1 + v2;
    public double Sub(double v1, double v2) => v1 - v2;
    public double Mul(double v1, double v2) => v1 * v2;
    public double Div(double v1, double v2) => v1 / v2;

    public GeneralComplexNumberDouble Cast(double v) => (GeneralComplexNumberDouble)v;
    public bool Eqz(GeneralComplexNumberDouble v) => v == (GeneralComplexNumberDouble)0D;
    public GeneralComplexNumberDouble Neg(GeneralComplexNumberDouble v) => -v;
    public GeneralComplexNumberDouble Add(double v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Add(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 * v2;
    public GeneralComplexNumberDouble HalfOf(GeneralComplexNumberDouble v) => v / 2.0;
    public GeneralComplexNumberDouble Div(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 / v2;
    public GeneralComplexNumberDouble Sqrt(double v) => v.Sqrti();
    public GeneralComplexNumberDouble Sqrt(GeneralComplexNumberDouble v) => v.Sqrt();
    public bool AboveTolerance(GeneralComplexNumberDouble v) => v.Abs() > 1e-10;
    public GeneralMatrix<GeneralComplexNumberDouble> Cast(GeneralMatrix<double> m) => ((ComplexMatrixDouble)new GeneralMatrixDouble(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Sub(GeneralMatrix<double> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => ((ComplexMatrixDouble)new GeneralMatrixDouble(m1) - new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Sub(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) - new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralComplexNumberDouble v, GeneralMatrix<double> m) => (v * (ComplexMatrixDouble)new GeneralMatrixDouble(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m, GeneralComplexNumberDouble v) => (new ComplexMatrixDouble(m) * v).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralMatrix<GeneralComplexNumberDouble> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => (new ComplexMatrixDouble(m1) * new ComplexMatrixDouble(m2)).Matrix;

    public ComplexMatrixDouble ComplexMatrix(GeneralMatrix<GeneralComplexNumberDouble> m) => new(m);
}
