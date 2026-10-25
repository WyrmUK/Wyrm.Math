namespace Wyrm.Math.Matrix.Base;

internal class DoubleFunctions : ITypeFunctions<double>
{
    public double One() => 1D;
    public bool Eqz(double v) => v == 0D;
    public bool Nez(double v) => v != 0D;
    public bool Eq1(double v) => v == 1D;
    public bool Ne1(double v) => v != 1D;
    public double Neg(double v) => -v;
    public double Add(double v1, double v2) => v1 + v2;
    public double Sub(double v1, double v2) => v1 - v2;
    public double Mul(double v1, double v2) => v1 * v2;
    public double Div(double v1, double v2) => v1 / v2;
}
