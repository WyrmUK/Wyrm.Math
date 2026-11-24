using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class DoubleFunctions : CommonComplexDoubleFunctions, ITypeFunctions<double, GeneralComplexNumberDouble, ComplexMatrixDouble>
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
    public double Mod(double v1, double v2) => v1 % v2;

    public GeneralComplexNumberDouble Cast(double v) => (GeneralComplexNumberDouble)v;
    public double Abs(GeneralComplexNumberDouble v) => v.Abs();
    public GeneralComplexNumberDouble Add(double v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Sqrt(double v) => v.Sqrti();
    public GeneralMatrix<GeneralComplexNumberDouble> CopyAs(GeneralMatrix<double> m) => new(m.Columns, [.. m.Values.Select(x => (GeneralComplexNumberDouble)x)]);
    public GeneralMatrix<GeneralComplexNumberDouble> Sub(GeneralMatrix<double> m1, GeneralMatrix<GeneralComplexNumberDouble> m2) => ((ComplexMatrixDouble)new GeneralMatrixDouble(m1) - new ComplexMatrixDouble(m2)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralComplexNumberDouble v, GeneralMatrix<double> m) => (v * (ComplexMatrixDouble)new GeneralMatrixDouble(m)).Matrix;
    public GeneralMatrix<GeneralComplexNumberDouble> Div(GeneralMatrix<GeneralComplexNumberDouble> m, double v) => (new ComplexMatrixDouble(m) / (GeneralComplexNumberDouble)v).Matrix;
}
