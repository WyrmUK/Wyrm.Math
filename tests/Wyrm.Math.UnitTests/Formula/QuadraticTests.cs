using Shouldly;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Formula;

namespace Wyrm.Math.UnitTests.Formula;

public class QuadraticTests
{
    [Theory]
    [MemberData(nameof(RootsDecimalTests))]
    public void Roots_Decimal_Should_Return_Correctly(decimal a, decimal b, decimal c, GeneralComplexNumberDecimal[] expected)
    {
        var roots = Quadratic.Roots(a, b, c);
        roots.ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsDoubleTests))]
    public void Roots_Double_Should_Return_Correctly(double a, double b, double c, GeneralComplexNumberDouble[] expected)
    {
        var roots = Quadratic.Roots(a, b, c);
        roots.ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsGeneralComplexNumberDecimalTests))]
    public void Roots_GeneralComplexNumberDecimal_Should_Return_Correctly(GeneralComplexNumberDecimal a, GeneralComplexNumberDecimal b, GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal[] expected)
    {
        var roots = Quadratic.Roots(a, b, c);
        roots.ToArray().ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(RootsGeneralComplexNumberDoubleTests))]
    public void Roots_GeneralComplexNumberDouble_Should_Return_Correctly(GeneralComplexNumberDouble a, GeneralComplexNumberDouble b, GeneralComplexNumberDouble c, GeneralComplexNumberDouble[] expected)
    {
        var roots = Quadratic.Roots(a, b, c);
        roots.ToArray().ShouldBeEquivalentTo(expected);
    }

    #region Test Data

    public static TheoryData<decimal, decimal, decimal, GeneralComplexNumberDecimal[]> RootsDecimalTests = new()
    {
        { 2M, 3M, 4M, [new GeneralComplexNumberDecimal(0.75M, -1.198957880828179885399359516M), new GeneralComplexNumberDecimal(0.75M, 1.198957880828179885399359516M)] },
        { 2M, 3M, -4M, [new GeneralComplexNumberDecimal(-0.8507810593582121716220544186M, 0M), new GeneralComplexNumberDecimal(2.3507810593582121716220544185M, 0M)] },
        { 2M, -3M, 4M, [new GeneralComplexNumberDecimal(-0.75M, -1.198957880828179885399359516M), new GeneralComplexNumberDecimal(-0.75M, 1.198957880828179885399359516M)] },
        { 2M, -3M, -4M, [new GeneralComplexNumberDecimal(-2.3507810593582121716220544185M, 0M), new GeneralComplexNumberDecimal(0.8507810593582121716220544186M, 0M)] },
        { -2M, 3M, 4M, [new GeneralComplexNumberDecimal(0.8507810593582121716220544186M, 0M), new GeneralComplexNumberDecimal(-2.3507810593582121716220544185M, 0M)] },
        { -2M, 3M, -4M, [new GeneralComplexNumberDecimal(-0.75M, 1.198957880828179885399359516M), new GeneralComplexNumberDecimal(-0.75M, -1.198957880828179885399359516M)] },
        { -2M, -3M, 4M, [new GeneralComplexNumberDecimal(2.3507810593582121716220544185M, 0M), new GeneralComplexNumberDecimal(-0.8507810593582121716220544186M, 0M)] },
        { -2M, -3M, -4M, [new GeneralComplexNumberDecimal(0.75M, 1.198957880828179885399359516M), new GeneralComplexNumberDecimal(0.75M, -1.198957880828179885399359516M)] }
    };

    public static TheoryData<double, double, double, GeneralComplexNumberDouble[]> RootsDoubleTests = new()
    {
        { 2D, 3D, 4D, [new GeneralComplexNumberDouble(0.7499999999999999, -1.1989578808281798), new GeneralComplexNumberDouble(0.7500000000000001, 1.1989578808281798)] },
        { 2D, 3D, -4D, [new GeneralComplexNumberDouble(-0.8507810593582121, 0), new GeneralComplexNumberDouble(2.3507810593582121, 0)] },
        { 2D, -3D, 4D, [new GeneralComplexNumberDouble(-0.7500000000000001, -1.1989578808281798), new GeneralComplexNumberDouble(-0.7499999999999999, 1.1989578808281798)] },
        { 2D, -3D, -4D, [new GeneralComplexNumberDouble(-2.3507810593582121, 0), new GeneralComplexNumberDouble(0.8507810593582121, 0)] },
        { -2D, 3D, 4D, [new GeneralComplexNumberDouble(0.8507810593582121, 0), new GeneralComplexNumberDouble(-2.3507810593582121, 0)] },
        { -2D, 3D, -4D, [new GeneralComplexNumberDouble(-0.7499999999999999, 1.1989578808281798), new GeneralComplexNumberDouble(-0.7500000000000001, -1.1989578808281798)] },
        { -2D, -3D, 4D, [new GeneralComplexNumberDouble(2.3507810593582121, 0), new GeneralComplexNumberDouble(-0.8507810593582121, 0)] },
        { -2D, -3D, -4D, [new GeneralComplexNumberDouble(0.7500000000000001, 1.1989578808281798), new GeneralComplexNumberDouble(0.7499999999999999, -1.1989578808281798)] }
    };

    public static TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal[]> RootsGeneralComplexNumberDecimalTests = new()
    {
        { new GeneralComplexNumberDecimal(2M, 0M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(4M, 0M), [new GeneralComplexNumberDecimal(0.75M, -1.198957880828179885399359516M), new GeneralComplexNumberDecimal(0.75M, 1.198957880828179885399359516M)] },
        { new GeneralComplexNumberDecimal(2M, 0M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(-4M, 1M), [new GeneralComplexNumberDecimal(-0.8583104211836217919618486279M, 0.1554426289273277959622841876M), new GeneralComplexNumberDecimal(2.3583104211836217919618486280M, -0.1554426289273277959622841876M)] },
        { new GeneralComplexNumberDecimal(2M, 0M), new GeneralComplexNumberDecimal(-3M, 1M), new GeneralComplexNumberDecimal(4M, 0M), [new GeneralComplexNumberDecimal(-0.9019286174600142706488969710M, 1.4841322071817554478822864499M), new GeneralComplexNumberDecimal(-0.5980713825399857293511030290M, -0.9841322071817554478822864499M)] },
        { new GeneralComplexNumberDecimal(2M, 0M), new GeneralComplexNumberDecimal(-3M, 1M), new GeneralComplexNumberDecimal(-4M, 1M), [new GeneralComplexNumberDecimal(-2.3544784997861667118589089882M, 0.5226742677189546854369243518M), new GeneralComplexNumberDecimal(0.8544784997861667118589089882M, -0.0226742677189546854369243518M)] },
        { new GeneralComplexNumberDecimal(-2M, 1M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(4M, 0M), [new GeneralComplexNumberDecimal(0.8266338192519022665874529428M, 0.1065514164693924003402729572M), new GeneralComplexNumberDecimal(-2.0266338192519022665874529429M, -0.7065514164693924003402729572M)] },
        { new GeneralComplexNumberDecimal(-2M, 1M), new GeneralComplexNumberDecimal(3M, 0M), new GeneralComplexNumberDecimal(-4M, 1M), [new GeneralComplexNumberDecimal(-0.6161676606439632771288805178M, 0.9370373451317863746593406548M), new GeneralComplexNumberDecimal(-0.5838323393560367228711194822M, -1.5370373451317863746593406548M)] },
        { new GeneralComplexNumberDecimal(-2M, 1M), new GeneralComplexNumberDecimal(-3M, 1M), new GeneralComplexNumberDecimal(4M, 0M), [new GeneralComplexNumberDecimal(2.1769127330520692415072688651M, 0.4182313954520086953675239093M), new GeneralComplexNumberDecimal(-0.7769127330520692415072688651M, -0.2182313954520086953675239093M)] },
        { new GeneralComplexNumberDecimal(-2M, 1M), new GeneralComplexNumberDecimal(-3M, 1M), new GeneralComplexNumberDecimal(-4M, 1M), [new GeneralComplexNumberDecimal(0.5873891552572066181014846815M, 1.2544181228453083826171987325M), new GeneralComplexNumberDecimal(0.8126108447427933818985153185M, -1.0544181228453083826171987325M)] }
    };

    public static TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble[]> RootsGeneralComplexNumberDoubleTests = new()
    {
        { new GeneralComplexNumberDouble(2D, 0D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(4D, 0D), [new GeneralComplexNumberDouble(0.7499999999999999, -1.1989578808281798), new GeneralComplexNumberDouble(0.7500000000000001, 1.1989578808281798)] },
        { new GeneralComplexNumberDouble(2D, 0D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(-4D, 1D), [new GeneralComplexNumberDouble(-0.8583104211836217, 0.1554426289273278), new GeneralComplexNumberDouble(2.3583104211836217, -0.1554426289273278)] },
        { new GeneralComplexNumberDouble(2D, 0D), new GeneralComplexNumberDouble(-3D, 1D), new GeneralComplexNumberDouble(4D, 0D), [new GeneralComplexNumberDouble(-0.9019286174600144, 1.4841322071817555), new GeneralComplexNumberDouble(-0.5980713825399856, -0.9841322071817555)] },
        { new GeneralComplexNumberDouble(2D, 0D), new GeneralComplexNumberDouble(-3D, 1D), new GeneralComplexNumberDouble(-4D, 1D), [new GeneralComplexNumberDouble(-2.3544784997861665, 0.5226742677189546), new GeneralComplexNumberDouble(0.8544784997861667, -0.022674267718954644)] },
        { new GeneralComplexNumberDouble(-2D, 1D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(4D, 0D), [new GeneralComplexNumberDouble(0.8266338192519023, 0.10655141646939242), new GeneralComplexNumberDouble(-2.0266338192519022, -0.7065514164693925)] },
        { new GeneralComplexNumberDouble(-2D, 1D), new GeneralComplexNumberDouble(3D, 0D), new GeneralComplexNumberDouble(-4D, 1D), [new GeneralComplexNumberDouble(-0.6161676606439634, 0.9370373451317865), new GeneralComplexNumberDouble(-0.5838323393560365, -1.5370373451317865)] },
        { new GeneralComplexNumberDouble(-2D, 1D), new GeneralComplexNumberDouble(-3D, 1D), new GeneralComplexNumberDouble(4D, 0D), [new GeneralComplexNumberDouble(2.1769127330520694, 0.4182313954520088), new GeneralComplexNumberDouble(-0.7769127330520693, -0.21823139545200876)] },
        { new GeneralComplexNumberDouble(-2D, 1D), new GeneralComplexNumberDouble(-3D, 1D), new GeneralComplexNumberDouble(-4D, 1D), [new GeneralComplexNumberDouble(0.5873891552572066, 1.2544181228453084), new GeneralComplexNumberDouble(0.8126108447427934, -1.0544181228453082)] }
    };

    #endregion
}
