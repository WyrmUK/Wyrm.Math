using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.Matrix.Base;

internal class GeneralComplexNumberDoubleFunctions : ITypeFunctions<GeneralComplexNumberDouble>
{
    private static readonly GeneralComplexNumberDouble Zero = new(0D, 0D);
    private static readonly GeneralComplexNumberDouble OneValue = new(1D, 0D);

    public GeneralComplexNumberDouble One() => OneValue;
    public bool Eqz(GeneralComplexNumberDouble v) => v == Zero;
    public bool Nez(GeneralComplexNumberDouble v) => v != Zero;
    public bool Eq1(GeneralComplexNumberDouble v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDouble v) => v != OneValue;
    public GeneralComplexNumberDouble Neg(GeneralComplexNumberDouble v) => -v;
    public GeneralComplexNumberDouble Add(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 + v2;
    public GeneralComplexNumberDouble Sub(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 - v2;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 * v2;
    public GeneralComplexNumberDouble Div(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 / v2;
}
