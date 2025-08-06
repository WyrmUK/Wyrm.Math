using Shouldly;
using Wyrm.Math.UnitTests.TestHelpers;

namespace Wyrm.Math.UnitTests;

public class DecimalExtensionsTests
{
    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Abs_Should_Return_Abs(decimal value)
    {
        value.Abs().ShouldBe(System.Math.Abs(value));
    }

    [Theory]
    [MemberData(nameof(DecimalAcosValues))]
    public void Acos_Should_Return_Acos(decimal value, decimal expected)
    {
        value.Acos().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalAcoshValues))]
    public void Acosh_Should_Return_Acosh(decimal value, decimal expected, decimal tolerance)
    {
        value.Acosh().ShouldBeWithinTolerance(expected, tolerance);
        if (value > 1M)
        {
            Should.Throw<InvalidOperationException>(() => (1M / value).Acosh());
            Should.Throw<InvalidOperationException>(() => (-value).Acosh());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalAsinValues))]
    public void Asin_Should_Return_Asin(decimal value, decimal expected)
    {
        value.Asin().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalAsinhValues))]
    public void Asinh_Should_Return_Asinh(decimal value, decimal expected, decimal tolerance)
    {
        value.Asinh().ShouldBeWithinTolerance(expected, tolerance);
    }

    [Theory]
    [MemberData(nameof(DecimalAtanValues))]
    public void Atan_Should_Return_Atan(decimal value, decimal expected)
    {
        value.Atan().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalAtan2Values))]
    public void Atan2_Should_Return_Atan2(decimal value, decimal divisor, decimal expected)
    {
        value.Atan2(divisor).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalAtanhValues))]
    public void Atanh_Should_Return_Atanh(decimal value, decimal expected, decimal tolerance)
    {
        value.Atanh().ShouldBeWithinTolerance(expected, tolerance);
        if (value != 0M)
        {
            Should.Throw<InvalidOperationException>(() => (value * 10M).Atanh());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalBitDecValues))]
    public void BitDecrement_Should_Return_BitDecrement(decimal value, decimal expected)
    {
        value.BitDecrement().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalBitIncValues))]
    public void BitIncrement_Should_Return_BitIncrement(decimal value, decimal expected)
    {
        value.BitIncrement().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalCbrtValues))]
    public void Cbrt_Should_Return_Cbrt(decimal value, decimal expected)
    {
        value.Cbrt().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Ceiling_Should_Return_Ceiling(decimal value)
    {
        value.Ceiling().ShouldBe(System.Math.Ceiling(value));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Clamp_Should_Return_Clamp(decimal value)
    {
        value.Clamp(0.5M, 1.5M).ShouldBe(System.Math.Clamp(value, 0.5M, 1.5M));
        value.Clamp(-0.5M, 0.5M).ShouldBe(System.Math.Clamp(value, -0.5M, 0.5M));
        value.Clamp(-1.5M, -0.5M).ShouldBe(System.Math.Clamp(value, -1.5M, -0.5M));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void CopySign_Should_ReturnCopySign(decimal value)
    {
        value.CopySign(0.5M).ShouldBe(value < 0 ? -value : value);
        value.CopySign(-0.5M).ShouldBe(value < 0 ? value : -value);
    }

    [Theory]
    [MemberData(nameof(DecimalCosValues))]
    public void Cos_Should_Return_Cos(decimal value, decimal expected)
    {
        value.Cos().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalCoshValues))]
    public void Cosh_Should_Return_Cosh(decimal value, decimal expected, decimal tolerance)
    {
        value.Cosh().ShouldBeWithinTolerance(expected, tolerance);
    }

    [Theory]
    [MemberData(nameof(DecimalExpValues))]
    public void Exp_Should_Return_Exp(decimal value, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.Exp().ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<OverflowException>(() => value.Exp());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Floor_Should_Return_Floor(decimal value)
    {
        value.Floor().ShouldBe(System.Math.Floor(value));
    }

    [Theory]
    [MemberData(nameof(DecimalFusedMultiplyAddValues))]
    public void FusedMultiplyAdd_Should_Return_FusedMultiplyAdd(decimal x, decimal y, decimal z, decimal expected)
    {
        x.FusedMultiplyAdd(y, z).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalIEEERemainderValues))]
    public void IEEERemainder_Should_Return_IEEERemainder(decimal x, decimal y, decimal? expected)
    {
        if (expected.HasValue)
        {
            x.IEEERemainder(y).ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<OverflowException>(() => x.IEEERemainder(y));
        }
    }

    [Theory]
    [MemberData(nameof(DecimalILogBValues))]
    public void ILogB_Should_Return_ILogB(decimal value, int expected)
    {
        value.ILogB().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalLogValues))]
    public void Log_Should_Return_Log(decimal value, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.Log().ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<Exception>(() => value.Log());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalLogBaseValues))]
    public void LogBase_Should_Return_LogBase(decimal value, decimal newBase, decimal expected)
    {
        value.Log(newBase).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalLog2Values))]
    public void Log2_Should_Return_Log2(decimal value, decimal expected)
    {
        value.Log2().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalLog10Values))]
    public void Log10_Should_Return_Log10(decimal value, decimal expected)
    {
        value.Log10().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Max_Should_Return_Max(decimal value)
    {
        value.Max(1.1M).ShouldBe(System.Math.Max(value, 1.1M));
        value.Max(-1.1M).ShouldBe(System.Math.Max(value, -1.1M));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void MaxMagnitude_Should_Return_MaxMagnitude(decimal value)
    {
        value.MaxMagnitude(1.1M).ShouldBe(value.Abs() > 1.1M ? value : 1.1M);
        value.MaxMagnitude(-1.1M).ShouldBe(value.Abs() > 1.1M ? value : value == 1.1M ? value : -1.1M);
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Min_Should_Return_Min(decimal value)
    {
        value.Min(1.1M).ShouldBe(System.Math.Min(value, 1.1M));
        value.Min(-1.1M).ShouldBe(System.Math.Min(value, -1.1M));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void MinMagnitude_Should_Return_MinMagnitude(decimal value)
    {
        value.MinMagnitude(1.1M).ShouldBe(value.Abs() < 1.1M ? value : value == -1.1M ? value : 1.1M);
        value.MinMagnitude(-1.1M).ShouldBe(value.Abs() < 1.1M ? value : -1.1M);
    }

    [Theory]
    [MemberData(nameof(DecimalPowValues))]
    public void Pow_Should_Return_Pow(decimal value, decimal power, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.Pow(power).ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<InvalidOperationException>(() => value.Pow(power));
        }
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void ReciprocalEstimate_Should_Return_ReciprocalEstimate(decimal value)
    {
        if (value == 0M)
        {
            Should.Throw<OverflowException>(() => value.ReciprocalEstimate());
            return;
        }
        value.ReciprocalEstimate().ShouldBe((decimal)System.Math.ReciprocalEstimate((double)value));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void ReciprocalSqrtEstimate_Should_Return_ReciprocalEstimate(decimal value)
    {
        if (value <= 0M)
        {
            Should.Throw<OverflowException>(() => value.ReciprocalSqrtEstimate());
            return;
        }
        value.ReciprocalSqrtEstimate().ShouldBe((decimal)System.Math.ReciprocalSqrtEstimate((double)value));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Round_Should_Return_Round(decimal value)
    {
        value.Round().ShouldBe(System.Math.Round(value));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void RoundDigits_Should_Return_RoundDigits(decimal value)
    {
        value.Round(2).ShouldBe(System.Math.Round(value, 2));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void RoundMidPoint_Should_Return_RoundMidPoint(decimal value)
    {
        value.Round(MidpointRounding.ToEven).ShouldBe(System.Math.Round(value, MidpointRounding.ToEven));
        value.Round(MidpointRounding.AwayFromZero).ShouldBe(System.Math.Round(value, MidpointRounding.AwayFromZero));
        value.Round(MidpointRounding.ToZero).ShouldBe(System.Math.Round(value, MidpointRounding.ToZero));
        value.Round(MidpointRounding.ToNegativeInfinity).ShouldBe(System.Math.Round(value, MidpointRounding.ToNegativeInfinity));
        value.Round(MidpointRounding.ToPositiveInfinity).ShouldBe(System.Math.Round(value, MidpointRounding.ToPositiveInfinity));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void RoundDigitsMidPoint_Should_Return_RoundDigitsMidPoint(decimal value)
    {
        value.Round(2, MidpointRounding.ToEven).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToEven));
        value.Round(2, MidpointRounding.AwayFromZero).ShouldBe(System.Math.Round(value, 2, MidpointRounding.AwayFromZero));
        value.Round(2, MidpointRounding.ToZero).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToZero));
        value.Round(2, MidpointRounding.ToNegativeInfinity).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToNegativeInfinity));
        value.Round(2, MidpointRounding.ToPositiveInfinity).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToPositiveInfinity));
    }

    [Theory]
    [MemberData(nameof(DecimalScaleBValues))]
    public void ScaleB_Should_Return_ScaleB(decimal value, int power, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.ScaleB(power).ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<OverflowException>(() => value.ScaleB(power));
        }
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Sign_Should_Return_Sign(decimal value)
    {
        value.Sign().ShouldBe(System.Math.Sign(value));
    }

    [Theory]
    [MemberData(nameof(DecimalSinValues))]
    public void Sin_Should_Return_Sin(decimal value, decimal expected)
    {
        value.Sin().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalSinCosValues))]
    public void SinCos_Should_Return_SinCos(decimal value, (decimal Sin, decimal Cos) expected)
    {
        value.SinCos().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(DecimalSinhValues))]
    public void Sinh_Should_Return_Sinh(decimal value, decimal expected, decimal tolerance)
    {
        value.Sinh().ShouldBeWithinTolerance(expected, tolerance);
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Sqr_Should_Return_Sqr(decimal value)
    {
        value.Sqr().ShouldBe(value * value);
    }

    [Theory]
    [MemberData(nameof(DecimalSqrtValues))]
    public void Sqrt_Should_Return_Sqrt(decimal value, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.Sqrt().ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<InvalidOperationException>(() => value.Sqrt());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalTanValues))]
    public void Tan_Should_Return_Tan(decimal value, decimal? expected)
    {
        if (expected.HasValue)
        {
            value.Tan().ShouldBe(expected.Value);
        }
        else
        {
            Should.Throw<OverflowException>(() => value.Tan());
        }
    }

    [Theory]
    [MemberData(nameof(DecimalTanhValues))]
    public void Tanh_Should_Return_Tanh(decimal value, decimal expected, decimal tolerance)
    {
        value.Tanh().ShouldBeWithinTolerance(expected, tolerance);
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Truncate_Should_Return_Truncate(decimal value)
    {
        value.Truncate().ShouldBe(System.Math.Truncate(value));
    }

    #region Test Data

    public static readonly TheoryData<decimal> DecimalValues =
    [0.0M, 1.1M, 2.2M, 0.1M, 0.25M, 0.625M, 10.0M, 100.0M, Decimal.E, -1.1M, -2.2M, -0.1M, -0.25M, -0.625M, -10.0M, -100.0M, -Decimal.E];

    public static readonly TheoryData<decimal, decimal> DecimalAcosValues = new()
    {
        { -1M, Decimal.Pi },
        { -0.7071067811865475244008443621M, 2.3561944901923449288469825375M },
        { -0.5M, 2.0943951023931954923084289218M },
        { -0.0000000000000000000000000002M, 1.5707963267948966192313216918M },
        { 0M, Decimal.HalfPi },
        { 0.0000000000000000000000000001M, 1.5707963267948966192313216915M },
        { 0.5M, 1.0471975511965977461542144614M },
        { 0.7071067811865475244008443621M, 0.7853981633974483096156608457M },
        { 1M, 0M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalAcoshValues = new()
    {
        { 1M, 0M, 0.0000000000000000000000000000M },
        { 1.3246090892520058466628454770M, 0.7853981633974483096156608458159803203865000760042619678749218238881M, 0.0000000000000000000000000002M },
        { 2.5091784786580567820099956430M, 1.5707963267948966192313216915226845335184274327940654810467415599261M, 0.0000000000000000000000000001M },
        { 2.5091784786580567820099956433M, 1.5707963267948966192313216916530456959468361711071328765306760315721M, 0.0000000000000000000000000000M },
        { 2.5091784786580567820099956437M, 1.5707963267948966192313216918268605791847144888578894038137655730328M, 0.0000000000000000000000000001M },
        { 5.3227521495199585202790466581M, 2.3561944901923449288469825374548237724153953832266815362971792166588M, 0.0000000000000000000000000001M },
        { 11.591953275521520627751752053M, 3.1415926535897932384626433833175903576972664922043758037732458494388M, 0.0000000000000000000000000004M },
        { 25.386861192360776356310757542M, 3.9269908169872415480783042291190860288072358040700103403343603879638M, 0.0000000000000000000000000005M },
        { 267.74676148374822224593187990M, 6.2831853071795864769252867665553044681038174946613551224880584297318M, 0.0000000000000000000000000006M },
        { 587.24200841874135065486842953M, 7.0685834705770347865409476123707499975254943665374853023533444432032M, 0.0000000000000000000000000006M },
    };

    public static readonly TheoryData<decimal, decimal> DecimalAsinValues = new()
    {
        { -1M, -Decimal.HalfPi },
        { -0.7071067811865475244008443621M, -0.7853981633974483096156608459M },
        { -0.5M, -0.5235987755982988730771072302M },
        { -0.0000000000000000000000000002M, -0.0000000000000000000000000002M },
        { 0M, 0M },
        { 0.0000000000000000000000000001M, 0.0000000000000000000000000001M },
        { 0.5M, 0.5235987755982988730771072302M },
        { 0.7071067811865475244008443621M, 0.7853981633974483096156608459M },
        { 1M, Decimal.HalfPi }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalAsinhValues = new()
    {
        { -587.24115698039854549683257708M, -7.068583470577034786540947612376362005798042834685800104364189438798M, 0.0000000000000000000000000008M },
        { -267.74489404101651425711744969M, -6.283185307179586476925286766566265698507113661842464818216079667461M, 0.0000000000000000000000000006M },
        { -25.367158319374159246028073687M, -3.926990816987241548078304229092165929216645817597027705752280395807M, 0.0000000000000000000000000004M },
        { -11.548739357257748377977334315M, -3.141592653589793238462643383245996047578410292107448307711589245777M, 0.0000000000000000000000000003M },
        { -5.2279719246778036674880247893M, -2.356194490192344928846982537455833320861150725891346780846181217126M, 0.0000000000000000000000000003M },
        { -2.3012989023072948734630400234M, -1.570796326794896619231321691626030944146378985851369018700960844536M, 0.0000000000000000000000000002M },
        { -0.8686709614860096098969241823M, -0.785398163397448309615660845838481661607499221560674563493509415404M, 0.0000000000000000000000000000M },
        { -0.0000000000000000000000000002M, -0.0000000000000000000000000002M, 0.0000000000000000000000000000M },
        { 0M, 0M, 0.0000000000000000000000000000M },
        { 0.0000000000000000000000000001M, 0.0000000000000000000000000001M, 0.0000000000000000000000000000M },
        { 0.8686709614860096098969241823M, 0.7853981633974483096156608458384816616074992215606745634935094154041M, 0.0000000000000000000000000000M },
        { 2.3012989023072948734630400234M, 1.5707963267948966192313216916260309441463789858513690187009608445362M, 0.0000000000000000000000000002M },
        { 5.2279719246778036674880247893M, 2.3561944901923449288469825374558333208611507258913467808461812171267M, 0.0000000000000000000000000003M },
        { 11.548739357257748377977334315M, 3.1415926535897932384626433832459960475784102921074483077115892457778M, 0.0000000000000000000000000003M },
        { 25.367158319374159246028073687M, 3.9269908169872415480783042290921659292166458175970277057522803958076M, 0.0000000000000000000000000004M },
        { 267.74489404101651425711744969M, 6.2831853071795864769252867665662656985071136618424648182160796674619M, 0.0000000000000000000000000006M },
        { 587.24115698039854549683257708M, 7.0685834705770347865409476123763620057980428346858001043641894387984M, 0.0000000000000000000000000008M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalAtanValues = new()
    {
        { -281474976710656M, -1.5707963267948966192313216916M },
        { -281474976710655M, -1.5707963267948966192313216916M },
        { -0.0000000000000000000000000001M, -0.0000000000000000000000000001M },
        { 0M, 0M },
        { 0.0000000000000000000000000001M, 0.0000000000000000000000000001M },
        { 0.25M, 0.2449786631268641541720824813M },
        { 0.5M, 0.4636476090008061162142562314M },
        { 0.75M, 0.6435011087932843868028092284M },
        { 0.8M, 0.6747409422235526630565209738M },
        { 0.9M, 0.7328151017865065916407920741M },
        { 0.99M, 0.7803730800666358988978715175M },
        { 1M, 0.7853981633974483096156608459M },
        { 1.01M, 0.7903732467283023870000543409M },
        { 1.1M, 0.8329812666744317054176935616M },
        { 281474976710655M, 1.5707963267948966192313216916M },
        { 281474976710656M, 1.5707963267948966192313216916M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalAtan2Values = new()
    {
        { 0M, 0M, 0M },
        { 0M, 2M, 0M },
        { 2M, 0M, Decimal.HalfPi },
        { -2M, 0M, -Decimal.HalfPi },
        { 0.5M, -2M, 2.8966139904629290842905609020M },
        { -0.5M, -2M, -2.8966139904629290842905609020M },
        { 0.5M, 2M, 0.2449786631268641541720824813M },
        { 0.25M, 0.5M, 0.4636476090008061162142562314M },
        { 1.5M, 2M, 0.6435011087932843868028092284M },
        { 4M, 5M, 0.6747409422235526630565209738M },
        { 9M, 10M, 0.7328151017865065916407920741M },
        { 99M, 100M, 0.7803730800666358988978715175M },
        { 1M, 1M, 0.7853981633974483096156608459M },
        { 202M, 200M, 0.7903732467283023870000543409M },
        { 22M, 20M, 0.8329812666744317054176935616M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalAtanhValues = new()
    {
        { -0.9999985501065478986855902619M, -7.068583470577034786540925600679073193001346519603710226091963221987M, 0.0000000000000000000000000008M },
        { -0.9999930253396106106051072118M, -6.283185307179586476925284447746517233544296183623847797598939907632M, 0.0000000000000000000000000008M },
        { -0.9992238948786411629778100889M, -3.926990816987241548078304174510741373974369078968717069928752491257M, 0.0000000000000000000000000005M },
        { -0.9962720762207499442646905800M, -3.141592653589793238462643381594902523223951616780949178697888027249M, 0.0000000000000000000000000005M },
        { -0.9821933800072387798479071252M, -2.356194490192344928846982537834722327099265886077847592844484856499M, 0.0000000000000000000000000002M },
        { -0.9171523356672743463730929214M, -1.570796326794896619231321691371424628052141620051159966566249130272M, 0.0000000000000000000000000002M },
        { -0.6557942026326724356531211427M, -0.785398163397448309615660845834293377368347801770210643322000308423M, 0.0000000000000000000000000000M },
        { -0.5568933069002105824038512757M, -0.628318530717958647692528676590875078742291730665104365941832214840M, 0.0000000000000000000000000001M },
        { 0M, 0M, 0.0000000000000000000000000000M },
        { 0.5568933069002105824038512757M, 0.6283185307179586476925286765908750787422917306651043659418322148400M, 0.0000000000000000000000000001M },
        { 0.6557942026326724356531211427M, 0.7853981633974483096156608458342933773683478017702106433220003084232M, 0.0000000000000000000000000001M },
        { 0.9171523356672743463730929214M, 1.5707963267948966192313216913714246280521416200511599665662491302728M, 0.0000000000000000000000000002M },
        { 0.9821933800072387798479071252M, 2.3561944901923449288469825378347223270992658860778475928444848564991M, 0.0000000000000000000000000002M },
        { 0.9962720762207499442646905800M, 3.1415926535897932384626433815949025232239516167809491786978880272495M, 0.0000000000000000000000000005M },
        { 0.9992238948786411629778100889M, 3.9269908169872415480783041745107413739743690789687170699287524912578M, 0.0000000000000000000000000005M },
        { 0.9999930253396106106051072118M, 6.2831853071795864769252844477465172335442961836238477975989399076327M, 0.0000000000000000000000000008M },
        { 0.9999985501065478986855902619M, 7.0685834705770347865409256006790731930013465196037102260919632219873M, 0.0000000000000000000000000008M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalBitDecValues = new()
    {
        { 0.0M, -0.0000000000000000000000000001M },
        { 1.1M, 1.0999999999999999999999999999M },
        { 2.2M, 2.1999999999999999999999999999M },
        { 0.1M, 0.0999999999999999999999999999M },
        { 0.25M, 0.2499999999999999999999999999M },
        { 0.625M, 0.6249999999999999999999999999M },
        { 10.0M, 9.999999999999999999999999999M },
        { 100.0M, 99.99999999999999999999999999M },
        { Decimal.E, 2.7182818284590452353602874713M },
        { -1.1M, -1.1000000000000000000000000001M },
        { -2.2M, -2.2000000000000000000000000001M },
        { -0.1M, -0.1000000000000000000000000001M },
        { -0.25M, -0.2500000000000000000000000001M },
        { -0.625M, -0.6250000000000000000000000001M },
        { -10.0M, -10.000000000000000000000000001M },
        { -100.0M, -100.00000000000000000000000001M },
        { -Decimal.E, -2.7182818284590452353602874715M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalBitIncValues = new()
    {
        { 0.0M, 0.0000000000000000000000000001M },
        { 1.1M, 1.1000000000000000000000000001M },
        { 2.2M, 2.2000000000000000000000000001M },
        { 0.1M, 0.1000000000000000000000000001M },
        { 0.25M, 0.2500000000000000000000000001M },
        { 0.625M, 0.6250000000000000000000000001M },
        { 10.0M, 10.000000000000000000000000001M },
        { 100.0M, 100.00000000000000000000000001M },
        { Decimal.E, 2.7182818284590452353602874715M },
        { -1.1M, -1.0999999999999999999999999999M },
        { -2.2M, -2.1999999999999999999999999999M },
        { -0.1M, -0.0999999999999999999999999999M },
        { -0.25M, -0.2499999999999999999999999999M },
        { -0.625M, -0.6249999999999999999999999999M },
        { -10.0M, -9.999999999999999999999999999M },
        { -100.0M, -99.99999999999999999999999999M },
        { -Decimal.E, -2.7182818284590452353602874713M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalCbrtValues = new()
    {
        { 0.0000000000000000000000000001M, 0.0000000004641588833681470289M },
        { 1.1M, 1.0322801154563671592135852251M },
        { 1M, 1M },
        { 0M, 0M },
        { 5.525M, 1.7678446318975074452506642253M },
        { -5.625M, -1.7784466522450314030030773112M },
        { 5.625M, 1.7784466522450314030030773112M },
        { decimal.MinValue, -4294967296.0000000000000000001M },
        { decimal.MaxValue, 4294967296.0000000000000000001M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalCosValues = new()
    {
        { -Decimal.Pi * 9M / 4M, 0.7071067811865475244008443622M },
        { -Decimal.TwoPi, 1M },
        { -Decimal.Pi * 5M / 4M, -0.7071067811865475244008443624M },
        { -Decimal.Pi, -1M },
        { -Decimal.Pi * 3M / 4M, -0.7071067811865475244008443622M },
        { -Decimal.HalfPi - 0.0000000000000000000000000002M, 0M },
        { -Decimal.HalfPi, 0M },
        { -Decimal.HalfPi + 0.0000000000000000000000000001M, 0M },
        { -Decimal.Pi / 4M, 0.7071067811865475244008443622M },
        { 0M, 1M },
        { Decimal.Pi / 4M, 0.7071067811865475244008443622M },
        { Decimal.HalfPi - 0.0000000000000000000000000001M, 0M },
        { Decimal.HalfPi, 0M },
        { Decimal.HalfPi + 0.0000000000000000000000000002M, 0M },
        { Decimal.Pi * 3M / 4M, -0.7071067811865475244008443622M },
        { Decimal.Pi, -1M },
        { Decimal.Pi * 5M / 4M, -0.7071067811865475244008443624M },
        { Decimal.TwoPi, 1M },
        { Decimal.Pi * 9M / 4M, 0.7071067811865475244008443622M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalCoshValues = new()
    {
        { -Decimal.Pi * 9M / 4M, 587.242008418741350654868429534775146721983404110241308883600538939239953206M, 0.0000000000000000000000000700M },
        { -Decimal.TwoPi, 267.746761483748222245931879900991004254099610204148695412202318287976406239M, 0.0000000000000000000000000000M },
        { -Decimal.Pi * 5M / 4M, 25.3868611923607763563107575415000786664668801176631893062449508564532618987M, 0.0000000000000000000000000010M },
        { -Decimal.Pi, 11.5919532755215206277517520525601376957709171762054225382128830484626965582M, 0.0000000000000000000000000010M },
        { -Decimal.Pi * 3M / 4M, 5.32275214951995852027904665812511199189267170214028974617038678112025398856M, 0.0000000000000000000000000002M },
        { -Decimal.HalfPi - 0.0000000000000000000000000002M, 2.5091784786580567820099956437296657286734833328407602787850446913577M, 0.0000000000000000000000000000M },
        { -Decimal.HalfPi, 2.50917847865805678200999564326940594821202435814815227404797568614898589108M, 0.0000000000000000000000000002M },
        { -Decimal.HalfPi + 0.0000000000000000000000000001M, 2.5091784786580567820099956430392760579812948708018482717170788607244M, 0.0000000000000000000000000000M },
        { -Decimal.Pi / 4M, 1.32460908925200584666284547700338382143912100361606210242715344904309325470M, 0.0000000000000000000000000000M },
        { 0M, 1M, 0.0000000000000000000000000000M },
        { Decimal.Pi / 4M, 1.32460908925200584666284547700338382143912100361606210242715344904309325470M, 0.0000000000000000000000000000M },
        { Decimal.HalfPi - 0.0000000000000000000000000001M, 2.5091784786580567820099956430392760579812948708018482717170788607244M, 0.0000000000000000000000000000M },
        { Decimal.HalfPi, 2.50917847865805678200999564326940594821202435814815227404797568614898589108M, 0.0000000000000000000000000002M },
        { Decimal.HalfPi + 0.0000000000000000000000000002M, 2.5091784786580567820099956437296657286734833328407602787850446913577M, 0.0000000000000000000000000000M },
        { Decimal.Pi * 3M / 4M, 5.32275214951995852027904665812511199189267170214028974617038678112025398856M, 0.0000000000000000000000000002M },
        { Decimal.Pi, 11.5919532755215206277517520525601376957709171762054225382128830484626965582M, 0.0000000000000000000000000010M },
        { Decimal.Pi * 5M / 4M, 25.3868611923607763563107575415000786664668801176631893062449508564532618987M, 0.0000000000000000000000000010M },
        { Decimal.TwoPi, 267.746761483748222245931879900991004254099610204148695412202318287976406239M, 0.0000000000000000000000000000M },
        { Decimal.Pi * 9M / 4M, 587.242008418741350654868429534775146721983404110241308883600538939239953206M, 0.0000000000000000000000000700M },
    };

    public static readonly TheoryData<decimal, decimal?> DecimalExpValues = new()
    {
        { Decimal.MinExpPow - 0.000000000000000000000000001M, 0M },
        { Decimal.MinExpPow, 0.0000000000000000000000000001M },
        { -15M, 0.0000003059023205018257883715M },
        { -10.625M, 0.0000243008312593294629935544M },
        { -3.5M, 0.0301973834223185007397862924M },
        { -2M, 0.1353352832366126918939994950M },
        { -1M, 0.3678794411714423215955237701M },
        { -0.9M, 0.4065696597405991118834542398M },
        { -0.5M, 0.6065306597126334236037995352M },
        { -0.1M, 0.9048374180359595731642490594M },
        { 0M, 1M },
        { 1M, 2.7182818284590452353602874711M },
        { 2M, 7.3890560989306502272304274606M },
        { 3.5M, 33.115451958692313750653249350M },
        { 10.625M, 41150.855677666771781813446264M },
        { 15M, 3269017.3724721106393018550459M },
        { Decimal.MaxExpPow, 79228162514264337593543950268M },
        { Decimal.MaxExpPow + 0.000000000000000000000000001M, null }
    };

    public static readonly TheoryData<decimal, decimal, decimal, decimal> DecimalFusedMultiplyAddValues = new()
    {
        { 10000000000000000000000000000M, 2M, 0.6M, 20000000000000000000000000001M },
        { 1000000000000000000000000000M, 20M, 0.51M, 20000000000000000000000000001M },
        { 1000000000000000000000000000M, 20M, 0.5M, 20000000000000000000000000000M },
        { 100000000000000000000000000M, 200M, 0.4M, 20000000000000000000000000000M }
    };

    public static readonly TheoryData<decimal, decimal, decimal?> DecimalIEEERemainderValues = new()
    {
        { 2.0M, 0.0M, null },
        { 15.0M, 2.0M, -1.0M },
        { 15.0M, 6.0M, 3.0M },
        { 1.5M, 2.0M, -0.5M },
        { 33.3M, 6.6M, 0.3M }
    };

    public static readonly TheoryData<decimal, int> DecimalILogBValues = new()
    {
        { 1M, 0 },
        { 0.01562M, -7 },
        { 0.015625M, -6 },
        { 0.0301973834223185007397862924M, -6 },
        { 0.6065306597126334236037995352M, -1 },
        { 7.3890560989306502272304274606M, 2 },
        { 33.115451958692313750653249350M, 5 },
        { 64M, 6 },
        { 64.1M, 6 }
    };

    public static readonly TheoryData<decimal, decimal?> DecimalLogValues = new()
    {
        { -1M, null },
        { 0M, null },
        { 1M, 0M },
        { 0.0301973834223185007397862924M, -3.4999999999999999999999999986M },
        { 0.1353352832366126918939994950M, -2M },
        { 0.3678794411714423215955237701M, -1.0000000000000000000000000003M },
        { 0.4065696597405991118834542398M, -0.8999999999999999999999999999M },
        { 0.6065306597126334236037995352M, -0.4999999999999999999999999995M },
        { 0.9048374180359595731642490594M, -0.0999999999999999999999999999M },
        { 2.7182818284590452353602874711M, 1M },
        { 2M, 0.6931471805599453094172321218M },
        { 7.3890560989306502272304274606M, 2.0000000000000000000000000007M },
        { 10M, 2.3025850929940456840179914554M },
        { 33.115451958692313750653249350M, 3.5000000000000000000000000005M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalLogBaseValues = new()
    {
        { 0.01562M, 2M, -6.0004617362948324217454873067M },
        { 0.015625M, 2M, -5.9999999999999999999999999986M },
        { 0.01562M, 10M, -1.8063189704587184779432316986M },
        { 0.015625M, 10M, -1.8061799739838871712824333682M },
        { 0.01562M, 15M, -1.5358663339689012456107434063M },
        { 0.015625M, 15M, -1.5357481488588929363260660733M },
    };

    public static readonly TheoryData<decimal, decimal> DecimalLog2Values = new()
    {
        { 1M, 0M },
        { 0.01562M, -6.0004617362948324217454873067M },
        { 0.015625M, -5.9999999999999999999999999986M },
        { 0.0301973834223185007397862924M, -5.049432643111371925759736379M },
        { 0.6065306597126334236037995352M, -0.7213475204444817036799623394M },
        { 7.3890560989306502272304274606M, 2.8853900817779268147198493616M },
        { 33.115451958692313750653249350M, 5.0494326431113719257597363817M },
        { 64M, 5.999999999999999999999999998M },
        { 64.1M, 6.0022524517313786799657009888M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalLog10Values = new()
    {
        { 1M, 0M },
        { 0.01562M, -1.8063189704587184779432316986M },
        { 0.015625M, -1.8061799739838871712824333682M },
        { 0.0301973834223185007397862924M, -1.5200306866613813967789512151M },
        { 0.6065306597126334236037995352M, -0.2171472409516259138255644592M },
        { 7.3890560989306502272304274606M, 0.8685889638065036553022578379M },
        { 33.115451958692313750653249350M, 1.520030686661381396778951216M },
        { 64M, 1.8061799739838871712824333681M },
        { 64.1M, 1.806858029518817422248377009M }
    };

    public static readonly TheoryData<decimal, decimal, decimal?> DecimalPowValues = new()
    {
        { 1.1M, 0M, 1M },
        { 1M, 10M, 1M },
        { 1.1M, 1M, 1.1M },
        { 0M, 0M, null },
        { 0M, -1M, null },
        { 0M, 10M, 0M },
        { 5.525M, -1M, 1M / 5.525M },
        { -5.625M, 2.1M, null },
        { -5.625M, -2.1M, null },
        { -5.625M, 3M, -177.97851562500000000000000011M },
        { -5.625M, -3M, -0.0056186556927297668038408779M },
        { 5.625M, 2.1M, 37.606014247289008669017113858M },
        { 5.625M, -2.1M, 0.0265914912817991420705496454M },
        { 5.625M, 3M, 177.97851562500000000000000011M },
        { 5.625M, -3M, 0.0056186556927297668038408779M }
    };

    public static readonly TheoryData<decimal, int, decimal?> DecimalScaleBValues = new()
    {
        { 2M, 97, null },
        { 2M, -94, null },
        { 1.1M, 5, 35.2M },
        { 1.1M, -5, 0.034375M },
        { 2.2M, 66, 162331347848644054220.80M },
        { 2.2M, -66, 0.0000000000000000000298155598M }
    };

    public static readonly TheoryData<decimal, decimal> DecimalSinValues = new()
    {
        { -Decimal.Pi * 9M / 4M, -0.7071067811865475244008443622M },
        { -Decimal.TwoPi, 0M },
        { -Decimal.Pi * 5M / 4M, 0.7071067811865475244008443620M },
        { -Decimal.Pi, 0M },
        { -Decimal.Pi * 3M / 4M, -0.7071067811865475244008443621M },
        { -Decimal.HalfPi, -1M },
        { -Decimal.Pi / 4M, -0.7071067811865475244008443621M },
        { -0.0000000000000000000000000002M, -0.0000000000000000000000000002M },
        { 0M, 0M },
        { 0.0000000000000000000000000001M, 0.0000000000000000000000000001M },
        { Decimal.Pi / 4M, 0.7071067811865475244008443621M },
        { Decimal.HalfPi, 1M },
        { Decimal.Pi * 3M / 4M, 0.7071067811865475244008443621M },
        { Decimal.Pi, 0M },
        { Decimal.Pi * 5M / 4M, -0.7071067811865475244008443620M },
        { Decimal.TwoPi, 0M },
        { Decimal.Pi * 9M / 4M, 0.7071067811865475244008443622M }
    };

    public static readonly TheoryData<decimal, (decimal Sin, decimal Cos)> DecimalSinCosValues = new()
    {
        { -Decimal.Pi * 9M / 4M, (-0.7071067811865475244008443622M, 0.7071067811865475244008443622M) },
        { -Decimal.TwoPi, (0M, 1M) },
        { -Decimal.Pi * 5M / 4M, (0.7071067811865475244008443620M, -0.7071067811865475244008443624M) },
        { -Decimal.Pi, (0M, -1M) },
        { -Decimal.Pi * 3M / 4M, (-0.7071067811865475244008443621M, -0.7071067811865475244008443622M) },
        { -Decimal.HalfPi, (-1M, 0M) },
        { -Decimal.Pi / 4M, (-0.7071067811865475244008443621M, 0.7071067811865475244008443622M) },
        { 0M, (0M, 1M) },
        { Decimal.Pi / 4M, (0.7071067811865475244008443621M, 0.7071067811865475244008443622M) },
        { Decimal.HalfPi, (1M, 0M) },
        { Decimal.Pi * 3M / 4M, (0.7071067811865475244008443621M, -0.7071067811865475244008443622M) },
        { Decimal.Pi, (0M, -1M) },
        { Decimal.Pi * 5M / 4M, (-0.7071067811865475244008443620M, -0.7071067811865475244008443624M) },
        { Decimal.TwoPi, (0M, 1M) },
        { Decimal.Pi * 9M / 4M, (0.7071067811865475244008443622M, 0.7071067811865475244008443622M) }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalSinhValues = new()
    {
        { -Decimal.Pi * 9M / 4M, -587.24115698039854549683257708147954663621345378548514695900905731085129062M, 0.0000000000000000000000001400M },
        { -Decimal.TwoPi, -267.74489404101651425711744968805617722370618739914622009500293674933808830M, 0.0000000000000000000000000100M },
        { -Decimal.Pi * 5M / 4M, -25.367158319374159246028073687183107205194404872371472346672568422603536367M, 0.0000000000000000000000000000M },
        { -Decimal.Pi, -11.548739357257748377977334315388409684495189066394789455232163361061645792M, 0.0000000000000000000000000000M },
        { -Decimal.Pi * 3M / 4M, -5.2279719246778036674880247893201936821866122539511650999157670384668422176M, 0.0000000000000000000000000003M },
        { -Decimal.HalfPi, -2.3012989023072948734630400234344271781781465165163826659728398030935660138M, 0.0000000000000000000000000002M },
        { -Decimal.Pi / 4M, -0.8686709614860096098969241822753544020225166383782102324308624696139336094M, 0.0000000000000000000000000002M },
        { -0.0000000000000000000000000002M, -0.0000000000000000000000000002M, 0.0000000000000000000000000000M },
        { 0M, 0M, 0.0000000000000000000000000000M },
        { 0.0000000000000000000000000001M, 0.0000000000000000000000000001M, 0.0000000000000000000000000000M },
        { Decimal.Pi / 4M, 0.86867096148600960989692418227535440202251663837821023243086246961393360948M, 0.0000000000000000000000000002M },
        { Decimal.HalfPi, 2.30129890230729487346304002343442717817814651651638266597283980309356601380M, 0.0000000000000000000000000002M },
        { Decimal.Pi * 3M / 4M, 5.22797192467780366748802478932019368218661225395116509991576703846684221760M, 0.0000000000000000000000000003M },
        { Decimal.Pi, 11.5487393572577483779773343153884096844951890663947894552321633610616457924M, 0.0000000000000000000000000000M },
        { Decimal.Pi * 5M / 4M, 25.3671583193741592460280736871831072051944048723714723466725684226035363671M, 0.0000000000000000000000000000M },
        { Decimal.TwoPi, 267.744894041016514257117449688056177223706187399146220095002936749338088304M, 0.0000000000000000000000000100M },
        { Decimal.Pi * 9M / 4M, 587.241156980398545496832577081479546636213453785485146959009057310851290623M, 0.0000000000000000000000001400M }
    };

    public static readonly TheoryData<decimal, decimal?> DecimalSqrtValues = new()
    {
        { -1.1M, null },
        { 4M, 2M },
        { 1.1M, 1.0488088481701515469914535137M },
        { 2.2M, 1.4832396974191325897422794882M },
        { 0.1M, 0.3162277660168379331998893544M },
        { 5.625M, 2.3717082451262844989991701583M },
        { decimal.MaxValue, 281474976710656M }
    };

    public static readonly TheoryData<decimal, decimal?> DecimalTanValues = new()
    {
        { -Decimal.Pi * 9M / 4M, -1M },
        { -Decimal.TwoPi, 0M },
        { -Decimal.Pi * 5M / 4M, -0.9999999999999999999999999994M },
        { -Decimal.Pi, 0M },
        { -Decimal.Pi * 3M / 4M, 0.9999999999999999999999999999M },
        { -Decimal.HalfPi, null },
        { -Decimal.Pi / 4M, -0.9999999999999999999999999999M },
        { -Decimal.Pi / 5M, -0.7265425280053608858954667574M },
        { 0M, 0M },
        { Decimal.Pi / 5M, 0.7265425280053608858954667574M },
        { Decimal.Pi / 4M, 0.9999999999999999999999999999M },
        { Decimal.HalfPi, null },
        { Decimal.Pi * 3M / 4M, -0.9999999999999999999999999999M },
        { Decimal.Pi, 0M },
        { Decimal.Pi * 5M / 4M, 0.9999999999999999999999999994M },
        { Decimal.TwoPi, 0M },
        { Decimal.Pi * 9M / 4M, 1M }
    };

    public static readonly TheoryData<decimal, decimal, decimal> DecimalTanhValues = new()
    {
        { -Decimal.Pi * 9M / 4M, -0.9999985501065478986855902619638291925705398128505908134364839394889041326M, 0.0000000000000000000000000001M },
        { -Decimal.TwoPi, -0.9999930253396106106051072118323457464277193773757108412245587000557877793M, 0.0000000000000000000000000001M },
        { -Decimal.Pi * 5M / 4M, -0.9992238948786411629778100889847001609725172689413850636888080707894606272M, 0.0000000000000000000000000000M },
        { -Decimal.Pi, -0.9962720762207499442646905800125367118968991908045876143626124159785412989M, 0.0000000000000000000000000001M },
        { -Decimal.Pi * 3M / 4M, -0.9821933800072387798479071251867605794763057627678017594985065734455356560M, 0.0000000000000000000000000000M },
        { -Decimal.HalfPi, -0.9171523356672743463730929214426187753679271486010889453435741242915061714M, 0.0000000000000000000000000000M },
        { -Decimal.Pi / 4M, -0.6557942026326724356531211426917828879854700916801954830283305685915817700M, 0.0000000000000000000000000001M },
        { -Decimal.Pi / 5M, -0.5568933069002105824038512757448591302757499699575977727451010540084720570M, 0.0000000000000000000000000001M },
        { 0M, 0M, 0.0000000000000000000000000000M },
        { Decimal.Pi / 5M, 0.55689330690021058240385127574485913027574996995759777274510105400847205706M, 0.0000000000000000000000000001M},
        { Decimal.Pi / 4M, 0.65579420263267243565312114269178288798547009168019548302833056859158177008M, 0.0000000000000000000000000001M },
        { Decimal.HalfPi, 0.91715233566727434637309292144261877536792714860108894534357412429150617140M, 0.0000000000000000000000000000M},
        { Decimal.Pi * 3M / 4M, 0.98219338000723877984790712518676057947630576276780175949850657344553565605M, 0.0000000000000000000000000000M },
        { Decimal.Pi, 0.99627207622074994426469058001253671189689919080458761436261241597854129898M, 0.0000000000000000000000000001M },
        { Decimal.Pi * 5M / 4M, 0.99922389487864116297781008898470016097251726894138506368880807078946062729M, 0.0000000000000000000000000000M },
        { Decimal.TwoPi, 0.99999302533961061060510721183234574642771937737571084122455870005578777936M, 0.0000000000000000000000000001M },
        { Decimal.Pi * 9M / 4M, 0.99999855010654789868559026196382919257053981285059081343648393948890413260M, 0.0000000000000000000000000001M }
    };

    #endregion
}
