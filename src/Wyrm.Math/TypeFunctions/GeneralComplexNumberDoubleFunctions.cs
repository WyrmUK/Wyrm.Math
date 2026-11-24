using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.TypeFunctions;

internal class GeneralComplexNumberDoubleFunctions : CommonComplexDoubleFunctions, ITypeFunctions<GeneralComplexNumberDouble, GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    public static readonly GeneralComplexNumberDoubleFunctions TypeFunctions = new();

    private static readonly GeneralComplexNumberDouble OneValue = new(1D, 0D);
    private static readonly GeneralComplexNumberDouble TwoValue = new(2D, 0D);
    private static readonly GeneralComplexNumberDouble FourValue = new(4D, 0D);

    public GeneralComplexNumberDouble One() => OneValue;
    public GeneralComplexNumberDouble Two() => TwoValue;
    public GeneralComplexNumberDouble Four() => FourValue;
    public bool Nez(GeneralComplexNumberDouble v) => v != ZeroValue;
    public bool Eq1(GeneralComplexNumberDouble v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDouble v) => v != OneValue;
    public GeneralComplexNumberDouble Sub(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 - v2;
    public GeneralComplexNumberDouble Mod(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => (GeneralComplexNumberDouble)(v1.Real % v2.Real);

    public GeneralComplexNumberDouble Cast(GeneralComplexNumberDouble v) => v;
    public GeneralComplexNumberDouble Abs(GeneralComplexNumberDouble v) => (GeneralComplexNumberDouble)v.Abs();
    public GeneralMatrix<GeneralComplexNumberDouble> CopyAs(GeneralMatrix<GeneralComplexNumberDouble> m) => new(m.Columns, [.. m.Values]);
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralComplexNumberDouble v, GeneralMatrix<GeneralComplexNumberDouble> m) => (v * new ComplexMatrixDouble(m)).Matrix;
}
