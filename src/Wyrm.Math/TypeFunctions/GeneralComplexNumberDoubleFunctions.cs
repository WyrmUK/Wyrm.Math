using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.TypeFunctions;

internal class GeneralComplexNumberDoubleFunctions : CommonComplexDoubleFunctions, ITypeFunctions<GeneralComplexNumberDouble, GeneralComplexNumberDouble, ComplexMatrixDouble>
{
    public static readonly GeneralComplexNumberDoubleFunctions TypeFunctions = new();

    private static readonly GeneralComplexNumberDouble OneValue = new(1D, 0D);
    private static readonly GeneralComplexNumberDouble TwoValue = new(2D, 0D);
    private static readonly GeneralComplexNumberDouble ThreeValue = new(3D, 0D);
    private static readonly GeneralComplexNumberDouble FourValue = new(4D, 0D);
    private static readonly GeneralComplexNumberDouble NineValue = new(9D, 0D);
    private static readonly GeneralComplexNumberDouble TwentySevenValue = new(27D, 0D);
    private static readonly GeneralComplexNumberDouble NegHalfValue = new(-0.5D, 0D);
    private static readonly GeneralComplexNumberDouble NegOneValue = new(-1D, 0D);

    public GeneralComplexNumberDouble One() => OneValue;
    public GeneralComplexNumberDouble Two() => TwoValue;
    public GeneralComplexNumberDouble Three() => ThreeValue;
    public GeneralComplexNumberDouble Four() => FourValue;
    public GeneralComplexNumberDouble Nine() => NineValue;
    public GeneralComplexNumberDouble TwentySeven() => TwentySevenValue;
    public GeneralComplexNumberDouble NegHalf() => NegHalfValue;
    public GeneralComplexNumberDouble NegOne() => NegOneValue;
    public bool Nez(GeneralComplexNumberDouble v) => v != ZeroValue;
    public bool Eq1(GeneralComplexNumberDouble v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDouble v) => v != OneValue;
    public GeneralComplexNumberDouble Add(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2, GeneralComplexNumberDouble v3) => v1 + v2 + v3;
    public GeneralComplexNumberDouble Sub(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => v1 - v2;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2, GeneralComplexNumberDouble v3) => v1 * v2 * v3;
    public GeneralComplexNumberDouble Mul(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2, GeneralComplexNumberDouble v3, GeneralComplexNumberDouble v4) => v1 * v2 * v3 * v4;
    public GeneralComplexNumberDouble Mod(GeneralComplexNumberDouble v1, GeneralComplexNumberDouble v2) => (GeneralComplexNumberDouble)(v1.Real % v2.Real);

    public GeneralComplexNumberDouble Cast(GeneralComplexNumberDouble v) => v;
    public GeneralComplexNumberDouble Abs(GeneralComplexNumberDouble v) => (GeneralComplexNumberDouble)v.Abs();
    public GeneralMatrix<GeneralComplexNumberDouble> CopyAs(GeneralMatrix<GeneralComplexNumberDouble> m) => new(m.Columns, [.. m.Values]);
    public GeneralMatrix<GeneralComplexNumberDouble> Mul(GeneralComplexNumberDouble v, GeneralMatrix<GeneralComplexNumberDouble> m) => (v * new ComplexMatrixDouble(m)).Matrix;
}
