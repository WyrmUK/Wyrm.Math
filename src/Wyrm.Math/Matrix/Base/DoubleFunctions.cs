using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.ComplexNumbers.Base;

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
    public GeneralComplexNumberDouble Neg(GeneralComplexNumberDouble v) => -v;
    public GeneralComplexNumberDouble Add(double v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble HalfOf(GeneralComplexNumberDouble v) => v / 2.0;
    public GeneralComplexNumberDouble Sqrt(double v) => v.Sqrti();

    public ComplexMatrixDouble ComplexMatrix(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> v) => new ComplexMatrixDouble(v);
}
