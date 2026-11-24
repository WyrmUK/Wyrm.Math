using Shouldly;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Formula;

namespace Wyrm.Math.UnitTests.Formula;

public class CubicTests
{
    [Theory]
    [MemberData(nameof(RootsDecimalTests))]
    public void Roots_Decimal_Should_Return_Correctly(decimal a, decimal b, decimal c, decimal d, GeneralComplexNumberDecimal[] expected)
    {
        var roots = Cubic.Roots(a, b, c, d);
        roots.Select(c => c.Round(15)).ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsDoubleTests))]
    public void Roots_Double_Should_Return_Correctly(double a, double b, double c, double d, GeneralComplexNumberDouble[] expected)
    {
        var roots = Cubic.Roots(a, b, c, d);
        roots.Select(c => c.Round(15)).ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsGeneralComplexNumberDecimalTests))]
    public void Roots_GeneralComplexNumberDecimal_Should_Return_Correctly(GeneralComplexNumberDecimal a, GeneralComplexNumberDecimal b, GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal d, GeneralComplexNumberDecimal[] expected)
    {
        var roots = Cubic.Roots(a, b, c, d);
        roots.Select(c => c.Round(15)).ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsGeneralComplexNumberDoubleTests))]
    public void Roots_GeneralComplexNumberDouble_Should_Return_Correctly(GeneralComplexNumberDouble a, GeneralComplexNumberDouble b, GeneralComplexNumberDouble c, GeneralComplexNumberDouble d, GeneralComplexNumberDouble[] expected)
    {
        var roots = Cubic.Roots(a, b, c, d);
        roots.Select(c => c.Round(15)).ToArray().ShouldBeEquivalentTo(expected);
    }

    #region Test Data

    public static TheoryData<decimal, decimal, decimal, decimal, GeneralComplexNumberDecimal[]> RootsDecimalTests = new()
    {
        { 0M, 0M, 2M, 3M, [new GeneralComplexNumberDecimal(-1.5M, 0M)] },
        { 0M, 2M, 3M, 4M, [new GeneralComplexNumberDecimal(-0.75M, -1.198957880828180M), new GeneralComplexNumberDecimal(-0.75M, 1.198957880828180M)] },
        { 1M, -6M, 11M, -6M, [new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(2M, 0M)] },
        { 1M, -3M, 4M, -2M, [new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(1M, -1M), new GeneralComplexNumberDecimal(1M, 1M)] },
        { 1M, -1M, 3M, 5M, [new GeneralComplexNumberDecimal(-1M, 0M), new GeneralComplexNumberDecimal(1M, -2M), new GeneralComplexNumberDecimal(1M, 2M)] },
        { 2M, -3M, 5M, -7M, [new GeneralComplexNumberDecimal(1.445528458679522M, 0M), new GeneralComplexNumberDecimal(0.027235770660239M, -1.555801417157619M), new GeneralComplexNumberDecimal(0.027235770660239M, 1.555801417157619M)] }
    };

    public static TheoryData<double, double, double, double, GeneralComplexNumberDouble[]> RootsDoubleTests = new()
    {
        { 0D, 0D, 2D, 3D, [new GeneralComplexNumberDouble(-1.5D, 0D)] },
        { 0D, 2D, 3D, 4D, [new GeneralComplexNumberDouble(-0.75D, -1.198957880828180D), new GeneralComplexNumberDouble(-0.75D, 1.198957880828180D)] },
        { 1D, -6D, 11D, -6D, [new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(2D, 0D)] },
        { 1D, -3D, 4D, -2D, [new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(1D, -1D), new GeneralComplexNumberDouble(1D, 1D)] },
        { 1D, -1D, 3D, 5D, [new GeneralComplexNumberDouble(-0.999999999999999D, 0D), new GeneralComplexNumberDouble(0.999999999999999D, -1.999999999999999D), new GeneralComplexNumberDouble(0.999999999999999D, 1.999999999999999D)] },
        { 2D, -3D, 5D, -7D, [new GeneralComplexNumberDouble(1.445528458679522D, 0D), new GeneralComplexNumberDouble(0.027235770660239D, -1.555801417157619D), new GeneralComplexNumberDouble(0.027235770660239D, 1.555801417157618D)] }
    };

    public static TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal[]> RootsGeneralComplexNumberDecimalTests = new()
    {
        { (GeneralComplexNumberDecimal)0M, (GeneralComplexNumberDecimal)0M, (GeneralComplexNumberDecimal)2M, (GeneralComplexNumberDecimal)3M, [new GeneralComplexNumberDecimal(-1.5M, 0M)] },
        { (GeneralComplexNumberDecimal)0M, (GeneralComplexNumberDecimal)2M, (GeneralComplexNumberDecimal)3M, (GeneralComplexNumberDecimal)4M, [new GeneralComplexNumberDecimal(-0.75M, -1.198957880828180M), new GeneralComplexNumberDecimal(-0.75M, 1.198957880828180M)] },
        { (GeneralComplexNumberDecimal)1M, (GeneralComplexNumberDecimal)(-6M), (GeneralComplexNumberDecimal)11M, (GeneralComplexNumberDecimal)(-6M), [new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(2M, 0M)] },
        { new GeneralComplexNumberDecimal(1M, 1M), new GeneralComplexNumberDecimal(-3M, -1M), (GeneralComplexNumberDecimal)4M, (GeneralComplexNumberDecimal)(-2M), [new GeneralComplexNumberDecimal(0.742934135878323M, 0.529085513635746M), new GeneralComplexNumberDecimal(0.257065864121677M, -1.529085513635746M), new GeneralComplexNumberDecimal(1M, 0M)] },
        { new GeneralComplexNumberDecimal(1M, 1M), new GeneralComplexNumberDecimal(-1M, -1M), (GeneralComplexNumberDecimal)3M, (GeneralComplexNumberDecimal)5M, [new GeneralComplexNumberDecimal(-0.909055827540268M, 0.197352352072992M), new GeneralComplexNumberDecimal(0.378346838556507M, -1.719505733775866M), new GeneralComplexNumberDecimal(1.530708988983761M, 1.522153381702873M)] },
        { new GeneralComplexNumberDecimal(2M, 1M), new GeneralComplexNumberDecimal(-3M, -1M), (GeneralComplexNumberDecimal)5M, (GeneralComplexNumberDecimal)(-7M), [new GeneralComplexNumberDecimal(1.416614781707766M, -0.094675100980015M), new GeneralComplexNumberDecimal(-0.301175270055369M, -1.509365681580981M), new GeneralComplexNumberDecimal(0.284560488347603M, 1.404040782560997M)] }
    };

    public static TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble[]> RootsGeneralComplexNumberDoubleTests = new()
    {
        { (GeneralComplexNumberDouble)0D, (GeneralComplexNumberDouble)0D, (GeneralComplexNumberDouble)2D, (GeneralComplexNumberDouble)3D, [new GeneralComplexNumberDouble(-1.5D, 0D)] },
        { (GeneralComplexNumberDouble)0D, (GeneralComplexNumberDouble)2D, (GeneralComplexNumberDouble)3D, (GeneralComplexNumberDouble)4D, [new GeneralComplexNumberDouble(-0.75D, -1.198957880828180D), new GeneralComplexNumberDouble(-0.75D, 1.198957880828180D)] },
        { (GeneralComplexNumberDecimal)1D, (GeneralComplexNumberDecimal)(-6D), (GeneralComplexNumberDecimal)11D, (GeneralComplexNumberDecimal)(-6D), [new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(2D, 0D)] },
        { new GeneralComplexNumberDouble(1D, 1D), new GeneralComplexNumberDouble(-3D, -1D), (GeneralComplexNumberDouble)4D, (GeneralComplexNumberDouble)(-2D), [new GeneralComplexNumberDouble(0.742934135878323D, 0.529085513635746D), new GeneralComplexNumberDouble(0.257065864121677D, -1.529085513635746D), new GeneralComplexNumberDouble(1D, 0D)] },
        { new GeneralComplexNumberDouble(1D, 1D), new GeneralComplexNumberDouble(-1D, -1D), (GeneralComplexNumberDecimal)3D, (GeneralComplexNumberDecimal)5D, [new GeneralComplexNumberDouble(-0.909055827540267D, 0.197352352072992D), new GeneralComplexNumberDouble(0.378346838556507D, -1.719505733775865D), new GeneralComplexNumberDouble(1.53070898898376D, 1.522153381702873D)] },
        { new GeneralComplexNumberDouble(2D, 1D), new GeneralComplexNumberDouble(-3D, -1D), (GeneralComplexNumberDecimal)5D, (GeneralComplexNumberDecimal)(-7D), [new GeneralComplexNumberDouble(1.416614781707768D, -0.094675100980015D), new GeneralComplexNumberDouble(-0.301175270055369D, -1.509365681580982D), new GeneralComplexNumberDouble(0.284560488347601D, 1.404040782560997D)] }
    };

    #endregion
}
