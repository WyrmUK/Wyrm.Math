using Shouldly;

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
    [MemberData(nameof(DecimalAsinValues))]
    public void Asin_Should_Return_Asin(decimal value, decimal expected)
    {
        value.Asin().ShouldBe(expected);
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
    [MemberData(nameof(DecimalCosValues))]
    public void Cos_Should_Return_Cos(decimal value, decimal expected)
    {
        value.Cos().ShouldBe(expected);
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
    [MemberData(nameof(DecimalValues))]
    public void Max_Should_Return_Max(decimal value)
    {
        value.Max(1.1M).ShouldBe(System.Math.Max(value, 1.1M));
        value.Max(-1.1M).ShouldBe(System.Math.Max(value, -1.1M));
    }

    [Theory]
    [MemberData(nameof(DecimalValues))]
    public void Min_Should_Return_Min(decimal value)
    {
        value.Min(1.1M).ShouldBe(System.Math.Min(value, 1.1M));
        value.Min(-1.1M).ShouldBe(System.Math.Min(value, -1.1M));
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
    [MemberData(nameof(DecimalValues))]
    public void Truncate_Should_Return_Truncate(decimal value)
    {
        value.Truncate().ShouldBe(System.Math.Truncate(value));
    }

    #region Test Data

    public static readonly TheoryData<decimal> DecimalValues =
    [ 0.0M, 1.1M, 2.2M, 0.1M, 0.25M, 0.625M, 10.0M, 100.0M, Decimal.E, -1.1M, -2.2M, -0.1M, -0.25M, -0.625M, -10.0M, -100.0M, -Decimal.E ];

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
        { 7.3890560989306502272304274606M, 2.0000000000000000000000000007M },
        { 33.115451958692313750653249350M, 3.5000000000000000000000000005M }
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

    #endregion
}
