using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.TypeFunctions;

internal class GeneralComplexNumberDecimalFunctions : CommonComplexDecimalFunctions, ITypeFunctions<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    public static readonly GeneralComplexNumberDecimalFunctions TypeFunctions = new();

    private static readonly GeneralComplexNumberDecimal OneValue = new(1M, 0M);
    private static readonly GeneralComplexNumberDecimal TwoValue = new(2M, 0M);
    private static readonly GeneralComplexNumberDecimal FourValue = new(4M, 0M);

    public GeneralComplexNumberDecimal One() => OneValue;
    public GeneralComplexNumberDecimal Two() => TwoValue;
    public GeneralComplexNumberDecimal Four() => FourValue;
    public bool Nez(GeneralComplexNumberDecimal v) => v != ZeroValue;
    public bool Eq1(GeneralComplexNumberDecimal v) => v == OneValue;
    public bool Ne1(GeneralComplexNumberDecimal v) => v != OneValue;
    public GeneralComplexNumberDecimal Sub(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => v1 - v2;
    public GeneralComplexNumberDecimal Mod(GeneralComplexNumberDecimal v1, GeneralComplexNumberDecimal v2) => (GeneralComplexNumberDecimal)(v1.Real % v2.Real);

    public GeneralComplexNumberDecimal Cast(GeneralComplexNumberDecimal v) => v;
    public GeneralComplexNumberDecimal Abs(GeneralComplexNumberDecimal v) => (GeneralComplexNumberDecimal)v.Abs();
    public GeneralMatrix<GeneralComplexNumberDecimal> CopyAs(GeneralMatrix<GeneralComplexNumberDecimal> m) => new(m.Columns, [.. m.Values]);
    public GeneralMatrix<GeneralComplexNumberDecimal> Mul(GeneralComplexNumberDecimal v, GeneralMatrix<GeneralComplexNumberDecimal> m) => (v * new ComplexMatrixDecimal(m)).Matrix;
}
