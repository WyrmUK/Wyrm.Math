namespace Wyrm.Math.Matrix.Base;

internal class DecimalFunctions : ITypeFunctions<decimal>
{
    public decimal One() => 1M;
    public bool Eqz(decimal v) => v == 0M;
    public bool Nez(decimal v) => v != 0M;
    public bool Eq1(decimal v) => v == 1M;
    public bool Ne1(decimal v) => v != 1M;
    public decimal Neg(decimal v) => -v;
    public decimal Add(decimal v1, decimal v2) => v1 + v2;
    public decimal Sub(decimal v1, decimal v2) => v1 - v2;
    public decimal Mul(decimal v1, decimal v2) => v1 * v2;
    public decimal Div(decimal v1, decimal v2) => v1 / v2;
}
