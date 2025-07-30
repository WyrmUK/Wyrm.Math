using Shouldly;

namespace Wyrm.Math.UnitTests;

public class DoubleExtensionsTests
{
    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Abs_Should_Return_Abs(double value)
    {
        value.Abs().ShouldBe(System.Math.Abs(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Acos_Should_Return_Acos(double value)
    {
        value.Acos().ShouldBe(System.Math.Acos(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Asin_Should_Return_Asin(double value)
    {
        value.Asin().ShouldBe(System.Math.Asin(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Atan_Should_Return_Atan(double value)
    {
        value.Atan().ShouldBe(System.Math.Atan(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Atan2_Should_Return_Atan2(double value)
    {
        value.Atan2(0.5).ShouldBe(System.Math.Atan2(value, 0.5));
        value.Atan2(-0.5).ShouldBe(System.Math.Atan2(value, -0.5));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void BitDecrement_Should_Return_BitDecrement(double value)
    {
        value.BitDecrement().ShouldBe(System.Math.BitDecrement(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void BitIncrement_Should_Return_BitIncrement(double value)
    {
        value.BitIncrement().ShouldBe(System.Math.BitIncrement(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Cbrt_Should_Return_Cbrt(double value)
    {
        value.Cbrt().ShouldBe(System.Math.Cbrt(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Ceiling_Should_Return_Ceiling(double value)
    {
        value.Ceiling().ShouldBe(System.Math.Ceiling(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Clamp_Should_Return_Clamp(double value)
    {
        value.Clamp(0.5, 1.5).ShouldBe(System.Math.Clamp(value, 0.5, 1.5));
        value.Clamp(-0.5, 0.5).ShouldBe(System.Math.Clamp(value, -0.5, 0.5));
        value.Clamp(-1.5, -0.5).ShouldBe(System.Math.Clamp(value, -1.5, -0.5));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void CopySign_Should_Return_CopySign(double value)
    {
        value.CopySign(-0.5).ShouldBe(System.Math.CopySign(value, -0.5));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Cos_Should_Return_Cos(double value)
    {
        value.Cos().ShouldBe(System.Math.Cos(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Exp_Should_Return_Exp(double value)
    {
        value.Exp().ShouldBe(System.Math.Exp(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Floor_Should_Return_Floor(double value)
    {
        value.Floor().ShouldBe(System.Math.Floor(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void FusedMultiplyAdd_Should_Return_FusedMultiplyAdd(double value)
    {
        value.FusedMultiplyAdd(value / 0.5, value / 0.25).ShouldBe(System.Math.FusedMultiplyAdd(value, value / 0.5, value / 0.25));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void IEEERemainderAdd_Should_Return_IEEERemainder(double value)
    {
        value.IEEERemainder(value / 3.0).ShouldBe(System.Math.IEEERemainder(value, value / 3.0));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void ILogB_Should_Return_ILogB(double value)
    {
        value.ILogB().ShouldBe(System.Math.ILogB(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Log_Should_Return_Log(double value)
    {
        value.Log().ShouldBe(System.Math.Log(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void LogBase_Should_Return_LogBase(double value)
    {
        value.Log(2.0).ShouldBe(System.Math.Log(value, 2.0));
        value.Log(10.0).ShouldBe(System.Math.Log(value, 10.0));
        value.Log(100.0).ShouldBe(System.Math.Log(value, 100.0));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Log2_Should_Return_Log2(double value)
    {
        value.Log2().ShouldBe(System.Math.Log2(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Log10_Should_Return_Log10(double value)
    {
        value.Log10().ShouldBe(System.Math.Log10(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Max_Should_Return_Max(double value)
    {
        value.Max(1.1).ShouldBe(System.Math.Max(value, 1.1));
        value.Max(-1.1).ShouldBe(System.Math.Max(value, -1.1));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void MaxMagnitude_Should_Return_MaxMagnitude(double value)
    {
        value.MaxMagnitude(1.1).ShouldBe(System.Math.MaxMagnitude(value, 1.1));
        value.MaxMagnitude(-1.1).ShouldBe(System.Math.MaxMagnitude(value, -1.1));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Min_Should_Return_Min(double value)
    {
        value.Min(1.1).ShouldBe(System.Math.Min(value, 1.1));
        value.Min(-1.1).ShouldBe(System.Math.Min(value, -1.1));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void MinMagnitude_Should_Return_MinMagnitude(double value)
    {
        value.MinMagnitude(1.1).ShouldBe(System.Math.MinMagnitude(value, 1.1));
        value.MinMagnitude(-1.1).ShouldBe(System.Math.MinMagnitude(value, -1.1));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Pow_Should_Return_Pow(double value)
    {
        value.Pow(1.1).ShouldBe(System.Math.Pow(value, 1.1));
        value.Pow(-1.1).ShouldBe(System.Math.Pow(value, -1.1));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Round_Should_Return_Round(double value)
    {
        value.Round().ShouldBe(System.Math.Round(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void RoundDigits_Should_Return_RoundDigits(double value)
    {
        value.Round(2).ShouldBe(System.Math.Round(value, 2));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void RoundMidPoint_Should_Return_RoundMidPoint(double value)
    {
        value.Round(MidpointRounding.ToEven).ShouldBe(System.Math.Round(value, MidpointRounding.ToEven));
        value.Round(MidpointRounding.AwayFromZero).ShouldBe(System.Math.Round(value, MidpointRounding.AwayFromZero));
        value.Round(MidpointRounding.ToZero).ShouldBe(System.Math.Round(value, MidpointRounding.ToZero));
        value.Round(MidpointRounding.ToNegativeInfinity).ShouldBe(System.Math.Round(value, MidpointRounding.ToNegativeInfinity));
        value.Round(MidpointRounding.ToPositiveInfinity).ShouldBe(System.Math.Round(value, MidpointRounding.ToPositiveInfinity));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void RoundDigitsMidPoint_Should_Return_RoundDigitsMidPoint(double value)
    {
        value.Round(2, MidpointRounding.ToEven).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToEven));
        value.Round(2, MidpointRounding.AwayFromZero).ShouldBe(System.Math.Round(value, 2, MidpointRounding.AwayFromZero));
        value.Round(2, MidpointRounding.ToZero).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToZero));
        value.Round(2, MidpointRounding.ToNegativeInfinity).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToNegativeInfinity));
        value.Round(2, MidpointRounding.ToPositiveInfinity).ShouldBe(System.Math.Round(value, 2, MidpointRounding.ToPositiveInfinity));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Sign_Should_Return_Sign(double value)
    {
        value.Sign().ShouldBe(System.Math.Sign(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Sin_Should_Return_Sin(double value)
    {
        value.Sin().ShouldBe(System.Math.Sin(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void SinCos_Should_Return_SinCos(double value)
    {
        value.SinCos().ShouldBe(System.Math.SinCos(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Sqr_Should_Return_Sqr(double value)
    {
        value.Sqr().ShouldBe(value * value);
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Sqrt_Should_Return_Sqrt(double value)
    {
        value.Sqrt().ShouldBe(System.Math.Sqrt(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Tan_Should_Return_Tan(double value)
    {
        value.Tan().ShouldBe(System.Math.Tan(value));
    }

    [Theory]
    [MemberData(nameof(DoubleValues))]
    public void Truncate_Should_Return_Truncate(double value)
    {
        value.Truncate().ShouldBe(System.Math.Truncate(value));
    }

    #region Test Data

    public static readonly TheoryData<double> DoubleValues =
    [ 0.0, 1.1, 2.2, 0.1, 0.25, 0.625, 10.0, 100.0, double.E, -1.1, -2.2, -0.1, -0.25, -0.625, -10.0, -100.0, -double.E ];

    #endregion
}
