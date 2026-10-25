using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class GeneralComplexNumberDecimalFunctions : ITypeFunctions<GeneralComplexNumberDecimal>
{
    private static readonly GeneralComplexNumberDecimal Zero = new(0M, 0M);
    private static readonly GeneralComplexNumberDecimal OneValue = new(1M, 0M);

    public GeneralComplexNumberDecimal One() => OneValue;
    public bool Eqz(GeneralComplexNumberDecimal v) => v == Zero;
    public bool Nez(GeneralComplexNumberDecimal v) => v != Zero;
    public bool Eq1(GeneralComplexNumberDecimal v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDecimal v) => v != OneValue;
    public GeneralComplexNumberDecimal Neg(GeneralComplexNumberDecimal v) => -v;
    public GeneralComplexNumberDecimal Add(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 + v2;
    public GeneralComplexNumberDecimal Sub(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 - v2;
    public GeneralComplexNumberDecimal Mul(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 * v2;
    public GeneralComplexNumberDecimal Div(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 / v2;
}
