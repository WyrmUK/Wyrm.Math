using Shouldly;
using System.Globalization;
using Wyrm.Math.ComplexNumbers;

namespace Wyrm.Math.UnitTests.ComplexNumbers;

public class GeneralComplexNumberDecimalTests
{
    [Fact]
    public void Constructor_Should_Set_Real_And_Imaginary()
    {
        const decimal real = 1.1M;
        const decimal imaginary = 2.2M;
        var result = new GeneralComplexNumberDecimal(real, imaginary);
        result.Real.ShouldBe(real);
        result.Imaginary.ShouldBe(imaginary);
    }

    [Fact]
    public void Constructor_Should_Duplicate_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDecimal(1.1M, 2.2M);
        var result = new GeneralComplexNumberDecimal(complexNumber);
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(complexNumber.Real);
        result.Imaginary.ShouldBe(complexNumber.Imaginary);
    }

    [Theory]
    [MemberData(nameof(ToStringTestData))]
    public void ToString_Should_Return_Expected(GeneralComplexNumberDecimal complexNumber, string expected)
    {
        var result = complexNumber.ToString();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ToStringFormatTestData))]
    public void ToString_Format_Should_Return_Expected(GeneralComplexNumberDecimal complexNumber, string? format, string expected)
    {
        var result = complexNumber.ToString(format);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ToStringProviderTestData))]
    public void ToString_Provider_Should_Return_Expected(GeneralComplexNumberDecimal complexNumber, IFormatProvider? provider, string expected)
    {
        var result = complexNumber.ToString(provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ToStringFormatProviderTestData))]
    public void ToString_Format_Provider_Should_Return_Expected(GeneralComplexNumberDecimal complexNumber, string? format, IFormatProvider? provider, string expected)
    {
        var result = complexNumber.ToString(format, provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseTestData))]
    public void Parse_Should_Return_Expected(string complexNumber, GeneralComplexNumberDecimal expected)
    {
        var result = GeneralComplexNumberDecimal.Parse(complexNumber);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseProviderTestData))]
    public void Parse_Provider_Should_Return_Expected(string complexNumber, IFormatProvider? provider, GeneralComplexNumberDecimal expected)
    {
        var result = GeneralComplexNumberDecimal.Parse(complexNumber, provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TryParseTestData))]
    public void TryParse_Should_Return_Expected(string complexNumber, bool expected, GeneralComplexNumberDecimal expectedNumber)
    {
        var result = GeneralComplexNumberDecimal.TryParse(complexNumber, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(TryParseProviderTestData))]
    public void TryParse_Provider_Should_Return_Expected(string complexNumber, IFormatProvider? provider, bool expected, GeneralComplexNumberDecimal expectedNumber)
    {
        var result = GeneralComplexNumberDecimal.TryParse(complexNumber, provider, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(GetHashCodeTestData))]
    public void GetHashCode_Should_Return_Expected(GeneralComplexNumberDecimal complexNumber, int expected)
    {
        var result = complexNumber.GetHashCode();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Equals_Should_Return_True_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDecimal complexNumber1, object? obj, bool expected)
    {
        complexNumber1.Equals(obj).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberDecimalEqualityTheoryData))]
    public void Equals_Should_Return_True_When_Decimal_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDecimal complexNumber1, GeneralComplexNumberDecimal complexNumber2, bool expected)
    {
        complexNumber1.Equals(complexNumber2).ShouldBe(expected);
    }

    [Fact]
    public void Operator_Cast_Should_Cast_To_Decimal()
    {
        var result = (decimal)new GeneralComplexNumberDecimal(TestValue2_2, TestValue0_0);
        result.ShouldBe(TestValue2_2);
    }

    [Fact]
    public void Operator_Cast_Should_Throw_If_Imaginary()
    {
        Should.Throw<ArgumentException>(() => (decimal)new GeneralComplexNumberDecimal(TestValue2_2, TestValue2_2));
    }

    [Fact]
    public void Operator_Cast_Should_Cast_From_Decimal()
    {
        var result = (GeneralComplexNumberDecimal)TestValue2_2;
        result.Real.ShouldBe(TestValue2_2);
        result.Imaginary.ShouldBe(TestValue0_0);
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Operator_Equals_Should_Return_True_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDecimal complexNumber1, object? obj, bool expected)
    {
        if (obj is decimal decimalValue)
        {
            (complexNumber1 == decimalValue).ShouldBe(expected);
        }
        else
        {
            (complexNumber1 == (obj as GeneralComplexNumberDecimal?)).ShouldBe(expected);
        }
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Operator_Not_Equals_Should_Return_False_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDecimal complexNumber1, object? obj, bool expected)
    {
        if (obj is decimal decimalValue)
        {
            (complexNumber1 != decimalValue).ShouldBe(!expected);
        }
        else
        {
            (complexNumber1 != (obj as GeneralComplexNumberDecimal?)).ShouldBe(!expected);
        }
    }

    [Fact]
    public void ComplexConjugate_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2);
        var result = complexNumber.ComplexConjugate();
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(complexNumber.Real);
        result.Imaginary.ShouldBe(-complexNumber.Imaginary);
    }

    [Fact]
    public void Abs_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2);
        var result = complexNumber.Abs();
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe((complexNumber.Real.Sqr() + complexNumber.Imaginary.Sqr()).Sqrt());
    }

    [Fact]
    public void Argument_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2);
        var result = complexNumber.Argument();
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe(complexNumber.Imaginary.Atan2(complexNumber.Real));
    }

    [Theory]
    [MemberData(nameof(InverseTestData))]
    public void Inverse_Should_Return_Correct_Value(GeneralComplexNumberDecimal complexNumber, GeneralComplexNumberDecimal expected)
    {
        var result = complexNumber.Inverse();
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(expected.Real);
        result.Imaginary.ShouldBe(expected.Imaginary);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Decimal_To_ComplexNumber(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (c + value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_ComplexNumber_To_Decimal(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (value + c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_ComplexNumbers(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2, GeneralComplexNumberDecimal expected)
    {
        (c1 + c2)
            .ShouldBe(expected);
    }

    [Fact]
    public void Operator_Add_Should_Copy_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2);
        var result = +complexNumber;
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe(complexNumber);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Decimal_From_ComplexNumber(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (c - value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_ComplexNumber_From_Decimal(decimal value, GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        (value - c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_ComplexNumbers(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2, GeneralComplexNumberDecimal expected)
    {
        (c1 - c2)
            .ShouldBe(expected);
    }

    [Fact]
    public void Operator_Subtract_Should_Copy_Negative_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2);
        var result = -complexNumber;
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(-complexNumber.Real);
        result.Imaginary.ShouldBe(-complexNumber.Imaginary);
    }

    [Fact]
    public void Operator_Decrement_Should_Decrement_Real()
    {

    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Decimal_With_ComplexNumber(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (c * value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_ComplexNumber_With_Decimal(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (value * c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_ComplexNumbers(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2, GeneralComplexNumberDecimal expected)
    {
        (c1 * c2)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Decimal_Into_ComplexNumber(GeneralComplexNumberDecimal c, decimal value, GeneralComplexNumberDecimal expected)
    {
        (c / value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_ComplexNumber_Into_Decimal(decimal value, GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        (value / c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_ComplexNumbers(GeneralComplexNumberDecimal c1, GeneralComplexNumberDecimal c2, GeneralComplexNumberDecimal expected)
    {
        (c1 / c2)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSqrTheoryData))]
    public void Sqr_Should_Raise_To_Power2(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Sqr();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSqrtTheoryData))]
    public void Sqrt_Should_Raise_To_PowerHalf(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Sqrt();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestPowerTheoryData))]
    public void Pow_Should_Raise_To_Power(GeneralComplexNumberDecimal c, decimal power, GeneralComplexNumberDecimal expected)
    {
        var result = c.Pow(power);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexPowerTheoryData))]
    public void Pow_Should_Raise_To_Complex_Power(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal power, GeneralComplexNumberDecimal expected)
    {
        var result = c.Pow(power);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexSinTheoryData))]
    public void Sin_Should_Get_Sin(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Sin();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCosTheoryData))]
    public void Cos_Should_Get_Cos(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Cos();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexTanTheoryData))]
    public void Tan_Should_Get_Tan(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Tan();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAsinTheoryData))]
    public void Asin_Should_Get_Asin(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Asin();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAcosTheoryData))]
    public void Acos_Should_Get_Acos(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Acos();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAtanTheoryData))]
    public void Atan_Should_Get_Atan(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Atan();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexSinhTheoryData))]
    public void Sinh_Should_Get_Sinh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Sinh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCoshTheoryData))]
    public void Cosh_Should_Get_Cosh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Cosh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexTanhTheoryData))]
    public void Tanh_Should_Get_Tanh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Tanh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAsinhTheoryData))]
    public void Asinh_Should_Get_Asinh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Asinh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAcoshTheoryData))]
    public void Acosh_Should_Get_Acosh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Acosh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAtanhTheoryData))]
    public void Atanh_Should_Get_Atanh(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Atanh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCbrtTheoryData))]
    public void Cbrt_Should_Get_Cbrt(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Cbrt();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCopySignTheoryData))]
    public void CopySign_Should_Get_CopySign(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal s, GeneralComplexNumberDecimal expected)
    {
        var result = c.CopySign(s);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexScaleBTheoryData))]
    public void ScaleB_Should_Get_ScaleB(GeneralComplexNumberDecimal c, int n, GeneralComplexNumberDecimal expected)
    {
        var result = c.ScaleB(n);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexExpTheoryData))]
    public void Exp_Should_Get_Exp(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Exp();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLogTheoryData))]
    public void Log_Should_Get_Log(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Log();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLogBaseTheoryData))]
    public void LogBase_Should_Get_LogBase(GeneralComplexNumberDecimal c, decimal newBase, GeneralComplexNumberDecimal expected)
    {
        var result = c.Log(newBase);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLogComplexBaseTheoryData))]
    public void LogComplexBase_Should_Get_LogComplexBase(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal newBase, GeneralComplexNumberDecimal expected)
    {
        var result = c.Log(newBase);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLog2TheoryData))]
    public void Log2_Should_Get_Log2(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Log2();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLog10TheoryData))]
    public void Log10_Should_Get_Log10(GeneralComplexNumberDecimal c, GeneralComplexNumberDecimal expected)
    {
        var result = c.Log10();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexRoundTheoryData))]
    public void Round_Should_Get_Round(GeneralComplexNumberDecimal z, GeneralComplexNumberDecimal expected)
    {
        var result = z.Round();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexRoundDigitsTheoryData))]
    public void RoundDigits_Should_Get_RoundDigits(GeneralComplexNumberDecimal z, int digits, GeneralComplexNumberDecimal expected)
    {
        var result = z.Round(digits);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexRoundAlgorithmTheoryData))]
    public void RoundAlgorithm_Should_Get_RoundAlgorithm(GeneralComplexNumberDecimal z, MidpointRounding algorithm, GeneralComplexNumberDecimal expected)
    {
        var result = z.Round(algorithm);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexRoundDigitsAlgorithmTheoryData))]
    public void RoundDigitsAlgorithm_Should_Get_RoundDigitsAlgorithm(GeneralComplexNumberDecimal z, int digits, MidpointRounding algorithm, GeneralComplexNumberDecimal expected)
    {
        var result = z.Round(digits, algorithm);
        result.ShouldBe(expected);
    }

    #region Test Data

    public const decimal TestValue0_0 = 0.0M;
    public const decimal TestValue1 = 1.0M;
    public const decimal TestValue2 = 2.0M;
    public const decimal TestValue3 = TestValue1 + TestValue2;
    public const decimal TestValue4 = 2 * TestValue2;
    public const decimal TestValue5 = TestValue1 + TestValue4;
    public const decimal TestValue6 = 2 * TestValue3;
    public const decimal TestValue7 = TestValue1 + TestValue6;
    public const decimal TestValue8 = 2 * TestValue4;
    public const decimal TestValue9 = TestValue1 + TestValue8;
    public const decimal TestValue10 = 2 * TestValue5;
    public const decimal TestValue12 = 2 * TestValue6;
    public const decimal TestValue1_1 = 1.1M;
    public const decimal TestValue2_2 = 2.2M;
    public const decimal TestValue3_3 = TestValue1_1 + TestValue2_2;
    public const decimal TestValueNeg1_1 = -1.1M;
    public const decimal TestValueNeg2_2 = -2.2M;
    public const decimal TestValueNeg3_3 = TestValueNeg1_1 - TestValueNeg2_2;
    public const decimal Error1 = 0.0000000000000000000000000001M;

    private static readonly IFormatProvider FormatProvider = new CultureInfo("it-IT");

    public static readonly TheoryData<GeneralComplexNumberDecimal, string> ToStringTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), "2.2i" },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2), "-2.2i" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), "1.1" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0), "-1.1" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), "(1.1+2.2i)" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), "(1.1-2.2i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), "(-1.1+2.2i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), "(-1.1-2.2i)" }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, string?, string> ToStringFormatTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), "#0.00", "2.20i" },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2), "#0.00", "-2.20i" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), "#0.00", "1.10" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0), "#0.00", "-1.10" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), "#0.00", "(1.10+2.20i)" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), "#0.00", "(1.10-2.20i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), "#0.00", "(-1.10+2.20i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), "#0.00", "(-1.10-2.20i)" }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, IFormatProvider?, string> ToStringProviderTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), FormatProvider, "2,2i" },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2), FormatProvider, "-2,2i" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), FormatProvider, "1,1" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0), FormatProvider, "-1,1" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), FormatProvider, "(1,1+2,2i)" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), FormatProvider, "(1,1-2,2i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), FormatProvider, "(-1,1+2,2i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), FormatProvider, "(-1,1-2,2i)" }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, string?, IFormatProvider?, string> ToStringFormatProviderTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), "#0.00", FormatProvider, "2,20i" },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2), "#0.00", FormatProvider, "-2,20i" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), "#0.00", FormatProvider, "1,10" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0), "#0.00", FormatProvider, "-1,10" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), "#0.00", FormatProvider, "(1,10+2,20i)" },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), "#0.00", FormatProvider, "(1,10-2,20i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), "#0.00", FormatProvider, "(-1,10+2,20i)" },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), "#0.00", FormatProvider, "(-1,10-2,20i)" }
    };

    public static readonly TheoryData<string, GeneralComplexNumberDecimal> ParseTestData = new()
    {
        { "2.2i", new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "-2.2i", new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "1.1", new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "-1.1", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(2.2i)", new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "(-2.2i)", new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "(1.1)", new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "(-1.1)", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(1.1+2.2i)", new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(1.1-2.2i)", new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-1.1+2.2i)", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-1.1-2.2i)", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "( 1.1 +2.2i )", new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "( 1.1 -2.2i )", new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "( -1.1 +2.2i )", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "( -1.1 -2.2i )", new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) }
    };

    public static readonly TheoryData<string, IFormatProvider?, GeneralComplexNumberDecimal> ParseProviderTestData = new()
    {
        { "2,2i", FormatProvider, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "-2,2i", FormatProvider, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "1,1", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "-1,1", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "(-2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "(1,1)", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "(-1,1)", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(1,1+2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(1,1-2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-1,1+2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-1,1-2,2i)", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "( 1,1 +2,2i )", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "( 1,1 -2,2i )", FormatProvider, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "( -1,1 +2,2i )", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "( -1,1 -2,2i )", FormatProvider, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) }
    };

    public static readonly TheoryData<string, bool, GeneralComplexNumberDecimal> TryParseTestData = new()
    {
        { "2.2i", true, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "-2.2i", true, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "1.1", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "-1.1", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(2.2i)", true, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "(-2.2i)", true, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "(1.1)", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "(-1.1)", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(1.1+2.2i)", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(1.1-2.2i)", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-1.1+2.2i)", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-1.1-2.2i)", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "(11E-1+22E-1i)", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(0.11E+1-0.22E+1i)", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-11E-1+22E-1i)", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-0.11E+1-0.22E+1i)", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "( 1.1 +2.2i )", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "( 1.1 -2.2i )", true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "( -1.1 +2.2i )", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "( -1.1 -2.2i )", true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "(1.1 2.2i)", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1.1+2.2 i)", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1.1+2.2i", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "1.1+2.2i)", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "1.1 2.2i", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "()", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "( )", false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1x1+2.2i)", false, new GeneralComplexNumberDecimal(0M, 0M) }
    };

    public static readonly TheoryData<string, IFormatProvider?, bool, GeneralComplexNumberDecimal> TryParseProviderTestData = new()
    {
        { "2,2i", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "-2,2i", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "1,1", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "-1,1", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { "(-2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2) },
        { "(1,1)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { "(-1,1)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0) },
        { "(1,1+2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(1,1-2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-1,1+2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-1,1-2,2i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "(11E-1+22E-1i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "(0,11E+1-0,22E+1i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "(-11E-1+22E-1i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "(-0,11E+1-0,22E+1i)", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "( 1,1 +2,2i )", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { "( 1,1 -2,2i )", FormatProvider, true, new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2) },
        { "( -1,1 +2,2i )", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2) },
        { "( -1,1 -2,2i )", FormatProvider, true, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2) },
        { "(1,1 2,2i)", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1,1+2,2 i)", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1,1+2,2i", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "1,1+2,2i)", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "1,1 2,2i", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "()", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "( )", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) },
        { "(1x1+2.2i)", FormatProvider, false, new GeneralComplexNumberDecimal(0M, 0M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, int> GetHashCodeTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), TestValue0_0.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValueNeg2_2), TestValue0_0.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), TestValue1_1.GetHashCode() * 16777619 + TestValue0_0.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue0_0), TestValueNeg1_1.GetHashCode() * 16777619 + TestValue0_0.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue1_1.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), TestValue1_1.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), TestValueNeg1_1.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), TestValueNeg1_1.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, object?, bool> TestGeneralComplexNumberEqualityTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), null, false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue1_1, false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), TestValue1_1, true },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), TestValue2_2, false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), true },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), true },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), true }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, bool> TestGeneralComplexNumberDecimalEqualityTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), false },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), true },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), true },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), true }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> InverseTestData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue2, TestValue4), new GeneralComplexNumberDecimal(TestValue1 / TestValue10, -TestValue1 / TestValue5) },
        { new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(TestValue1 / TestValue5, -TestValue2 / TestValue5) },
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestScalarAdditionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValue3_3, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1 + TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), TestValue4, new GeneralComplexNumberDecimal(TestValue6, TestValue3) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestAdditionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 + TestValue2_2, TestValue2_2 + TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 + TestValue2_2, TestValueNeg2_2 + TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 + TestValue2_2, TestValueNeg2_2 + TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(TestValue3, TestValue5) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestScalarSubtractionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValue1_1 - TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1 - TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), TestValue1, new GeneralComplexNumberDecimal(TestValue1, TestValue3) }
    };

    public static readonly TheoryData<decimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestSubtractionScalarTheoryData = new()
    {
        { TestValue2_2, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2 - TestValue1_1, -TestValue2_2) },
        { TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2 - TestValueNeg1_1, -TestValue2_2) },
        { TestValue1, new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(-TestValue1, -TestValue3) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestSubtractionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 - TestValue2_2, TestValue2_2 - TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 - TestValue2_2, TestValueNeg2_2 - TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 - TestValue2_2, TestValueNeg2_2 - TestValue1_1) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(TestValue1, TestValue1) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestScalarMultiplicationTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValue1_1 * TestValue2_2, TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1 * TestValue2_2, TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), TestValue2, new GeneralComplexNumberDecimal(TestValue4, TestValue6) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestMultiplicationTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 * TestValue2_2 - TestValue2_2 * TestValue1_1, TestValue1_1 * TestValue1_1 + TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 * TestValue2_2 - TestValue2_2 * TestValue1_1, TestValueNeg1_1 * TestValue1_1 + TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValue1_1 * TestValue2_2 - TestValueNeg2_2 * TestValue1_1, TestValue1_1 * TestValue1_1 + TestValueNeg2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDecimal(TestValue2_2, TestValue1_1), new GeneralComplexNumberDecimal(TestValueNeg1_1 * TestValue2_2 - TestValueNeg2_2 * TestValue1_1, TestValueNeg1_1 * TestValue1_1 + TestValueNeg2_2 * TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(-TestValue4, TestValue7) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestScalarDivisionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValue1_1 / TestValue2_2, TestValue2_2 / TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1 / TestValue2_2, TestValue2_2 / TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue4), TestValue2, new GeneralComplexNumberDecimal(TestValue1, TestValue2) }
    };

    public static readonly TheoryData<decimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestDivisionScalarTheoryData = new()
    {
        { TestValue2_2, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue2 / TestValue5, Error1 -TestValue4 / TestValue5) },
        { TestValue2_2, new GeneralComplexNumberDecimal(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDecimal(-TestValue2 / TestValue5, Error1 -TestValue4 / TestValue5) },
        { TestValue2, new GeneralComplexNumberDecimal(TestValue2, TestValue4), new GeneralComplexNumberDecimal(TestValue1 / TestValue5, -TestValue2 / TestValue5) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestDivisionTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue2, TestValue4), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(TestValue2, TestValue0_0) },
        { new GeneralComplexNumberDecimal(-TestValue2, TestValue4), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(TestValue6 / TestValue5, TestValue8 / TestValue5) },
        { new GeneralComplexNumberDecimal(TestValue2, -TestValue4), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(-TestValue6 / TestValue5, -TestValue8 / TestValue5) },
        { new GeneralComplexNumberDecimal(-TestValue2, -TestValue4), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(-TestValue2, TestValue0_0) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestSqrTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue0_0), new GeneralComplexNumberDecimal(TestValue1, TestValue0_0) },
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue1), new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(-TestValue5, TestValue12) },
        { new GeneralComplexNumberDecimal(-TestValue2, -TestValue3), new GeneralComplexNumberDecimal(-TestValue5, TestValue12) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestSqrtTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue0_0), new GeneralComplexNumberDecimal(TestValue0_0, TestValue1) },
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue1), new GeneralComplexNumberDecimal(0.4550898605622273413043577578M, 1.0986841134678099660398011952M) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(1.6741492280355400404480393007M, 0.8959774761298381247157337555M) },
        { new GeneralComplexNumberDecimal(-TestValue2, -TestValue3), new GeneralComplexNumberDecimal(0.8959774761298381247157337551M, -1.6741492280355400404480393007M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestPowerTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue0_0), 1/TestValue2, new GeneralComplexNumberDecimal(TestValue0_0, TestValue1) },
        { new GeneralComplexNumberDecimal(-TestValue1, TestValue1), 1/TestValue2, new GeneralComplexNumberDecimal(0.4550898605622273413043577577M, 1.0986841134678099660398011950M) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), TestValue3, new GeneralComplexNumberDecimal(-46.000000000000000000000000003M, 8.999999999999999999999999998M) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), 1/TestValue5, new GeneralComplexNumberDecimal(1.2675064916851109046661051640M, 0.2523983872193169856664635377M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexPowerTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(TestValue4, TestValue5), new GeneralComplexNumberDecimal(-0.7530458367485589629719495710M, -0.9864287886477453438344641986M) },
        { new GeneralComplexNumberDecimal(TestValue2, TestValue3), new GeneralComplexNumberDecimal(TestValue1, TestValue1), new GeneralComplexNumberDecimal(-0.8636068988831278165707669549M, 1.0368893969147761870488673113M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexSinTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.8912073600614353399518025777M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(0.0M, 4.4571051705358935215688163705M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(4.0709535228000319552577517667M, 2.0217256181409679681515773407M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(0.0M, -4.4571051705358935215688163705M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(4.0709535228000319552577517667M, -2.0217256181409679681515773407M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-4.0709535228000319552577517667M, -2.0217256181409679681515773407M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexCosTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.4535961214255773877713700517M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(4.5679083288982274049029607963M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(2.0719855010158266488314712904M, -3.9722049325494672219512575164M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(4.5679083288982274049029607963M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(2.0719855010158266488314712904M, 3.9722049325494672219512575164M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(2.0719855010158266488314712904M, -3.9722049325494672219512575164M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexTanTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(1.9647596572486519509309227817M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(0.0M, 0.9757431300314515204143066679M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(0.0201403720704808721675947027M, 1.0143542521857925809578028328M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(0.0M, -0.9757431300314515204143066679M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.0201403720704808721675947027M, -1.0143542521857925809578028328M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-0.0201403720704808721675947027M, -1.0143542521857925809578028328M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAsinTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(0.8912073600614353399518025779M, 0.0M), new GeneralComplexNumberDecimal(1.0999999999999999999999999994M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(0.0M, 4.4571051705358935215688163705M), new GeneralComplexNumberDecimal(TestValue0_0, 2.1999999999999999999999999971M) },
        { new GeneralComplexNumberDecimal(4.0709535228000319552577517676M, 2.0217256181409679681515773411M), new GeneralComplexNumberDecimal(1.0999999999999999999999999990M, 2.1999999999999999999999999855M) },
        { new GeneralComplexNumberDecimal(0.0M, -4.4571051705358935215688163705M), new GeneralComplexNumberDecimal(TestValue0_0, -2.2000000000000000000000000002M) },
        { new GeneralComplexNumberDecimal(4.0709535228000319552577517676M, -2.0217256181409679681515773411M), new GeneralComplexNumberDecimal(1.0999999999999999999999999992M, -TestValue2_2) },
        { new GeneralComplexNumberDecimal(-4.0709535228000319552577517676M, -2.0217256181409679681515773411M), new GeneralComplexNumberDecimal(-1.0999999999999999999999999992M, -TestValue2_2) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAcosTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(0.4535961214255773877713700518M, 0.0M), new GeneralComplexNumberDecimal(1.0999999999999999999999999994M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(4.5679083288982274049029607964M, 0.0M), new GeneralComplexNumberDecimal(TestValue0_0, 2.2000000000000000000000000002M) },
        { new GeneralComplexNumberDecimal(2.0719855010158266488314712908M, -3.9722049325494672219512575172M), new GeneralComplexNumberDecimal(1.0999999999999999999999999996M, 2.2000000000000000000000000002M) },
        { new GeneralComplexNumberDecimal(-4.5679083288982274049029607964M, 0.0M), new GeneralComplexNumberDecimal(3.1415926535897932384626433832M, -2.2000000000000000000000000018M) },
        { new GeneralComplexNumberDecimal(2.0719855010158266488314712908M, 3.9722049325494672219512575172M), new GeneralComplexNumberDecimal(1.0999999999999999999999999979M, -2.1999999999999999999999999898M) },
        { new GeneralComplexNumberDecimal(-2.0719855010158266488314712908M, 3.9722049325494672219512575172M), new GeneralComplexNumberDecimal(2.0415926535897932384626433853M, -2.1999999999999999999999999898M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAtanTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(1.9647596572486519509309227818M, 0.0M), new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDecimal(0.0M, 0.9757431300314515204143066680M), new GeneralComplexNumberDecimal(TestValue0_0, 2.1999999999999999999999999412M) },
        { new GeneralComplexNumberDecimal(0.0201403720704808721675947027M, 1.0143542521857925809578028326M), new GeneralComplexNumberDecimal(1.1000000000000000000000000007M, 2.1999999999999999999999999412M) },
        { new GeneralComplexNumberDecimal(0.0M, -0.9757431300314515204143066680M), new GeneralComplexNumberDecimal(TestValue0_0, -2.1999999999999999999999999699M) },
        { new GeneralComplexNumberDecimal(0.0201403720704808721675947027M, -1.0143542521857925809578028326M), new GeneralComplexNumberDecimal(1.1000000000000000000000000000M, -2.1999999999999999999999999580M) },
        { new GeneralComplexNumberDecimal(-0.0201403720704808721675947027M, -1.0143542521857925809578028326M), new GeneralComplexNumberDecimal(-TestValue1_1, -2.1999999999999999999999999580M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexSinhTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(1.3356474701241767793847805239M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, 0.8084964038195901843040369105M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(-0.7860300284273540129707424331M, 1.3489912504715575753524873472M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, -0.8084964038195901843040369105M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-0.7860300284273540129707424331M, -1.3489912504715575753524873472M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.7860300284273540129707424331M, -1.3489912504715575753524873472M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexCoshTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(1.6685185538222563326736274299M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(-0.5885011172553457085241426125M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(-0.9819250330856715235501751622M, 1.0798661763661304456730142863M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(-0.5885011172553457085241426125M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-0.9819250330856715235501751622M, -1.0798661763661304456730142863M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-0.9819250330856715235501751622M, 1.0798661763661304456730142863M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexTanhTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.8004990217606297060114613308M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, -1.3738230567687951601400367638M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(1.0461275040217014190478914624M, -0.2233505973699565796798234001M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue0_0, 1.3738230567687951601400367638M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(1.0461275040217014190478914624M, 0.2233505973699565796798234001M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-1.0461275040217014190478914624M, 0.2233505973699565796798234001M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAsinhTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(1.3356474701241767793847805236M, TestValue0_0), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, 0.8084964038195901843040369104M), new GeneralComplexNumberDecimal(TestValue0_0, 0.9415926535897932384626433832M) },
        { new GeneralComplexNumberDecimal(-0.7860300284273540129707424332M, 1.3489912504715575753524873472M), new GeneralComplexNumberDecimal(-1.1000000000000000000000000008M, 0.9415926535897932384626433835M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -0.8084964038195901843040369104M), new GeneralComplexNumberDecimal(TestValue0_0, -0.9415926535897932384626433832M) },
        { new GeneralComplexNumberDecimal(-0.7860300284273540129707424332M, -1.3489912504715575753524873472M), new GeneralComplexNumberDecimal(-1.1000000000000000000000000008M, -0.9415926535897932384626433835M) },
        { new GeneralComplexNumberDecimal(0.7860300284273540129707424332M, -1.3489912504715575753524873472M), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, -0.9415926535897932384626433832M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAcoshTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(1.6685185538222563326736274300M, TestValue0_0), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(-0.5885011172553457085241426127M, TestValue0_0), new GeneralComplexNumberDecimal(-0.0000000000000000000000000002M, 2.2000000000000000000000000003M) },
        { new GeneralComplexNumberDecimal(-0.9819250330856715235501751625M, 1.0798661763661304456730142859M), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, TestValue2_2) },
        { new GeneralComplexNumberDecimal(0.5885011172553457085241426127M, TestValue0_0), new GeneralComplexNumberDecimal(-0.0000000000000000000000000002M, 0.9415926535897932384626433830M) },
        { new GeneralComplexNumberDecimal(-0.9819250330856715235501751625M, -1.0798661763661304456730142859M), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, -TestValue2_2) },
        { new GeneralComplexNumberDecimal(0.9819250330856715235501751625M, 1.0798661763661304456730142859M), new GeneralComplexNumberDecimal(1.0999999999999999999999999997M, 0.9415926535897932384626433833M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexAtanhTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(0.8004990217606297060114613306M, TestValue0_0), new GeneralComplexNumberDecimal(1.1000000000000000000000000003M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -1.3738230567687951601400367633M), new GeneralComplexNumberDecimal(TestValue0_0, -0.9415926535897932384626433834M) },
        { new GeneralComplexNumberDecimal(1.0461275040217014190478914623M, -0.2233505973699565796798234003M), new GeneralComplexNumberDecimal(1.1000000000000000000000000001M, -0.9415926535897932384626433836M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, 1.3738230567687951601400367633M), new GeneralComplexNumberDecimal(TestValue0_0, 0.9415926535897932384626433834M) },
        { new GeneralComplexNumberDecimal(1.0461275040217014190478914623M, 0.2233505973699565796798234003M), new GeneralComplexNumberDecimal(1.1000000000000000000000000001M, 0.9415926535897932384626433836M) },
        { new GeneralComplexNumberDecimal(-1.0461275040217014190478914623M, 0.2233505973699565796798234003M), new GeneralComplexNumberDecimal(-1.0999999999999999999999999986M, 0.9415926535897932384626433834M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexCbrtTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(1.0322801154563671592135852249M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(1.1263452329180597013141657562M, 0.6502957234256934994051155751M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(1.2589858696615772964532950315M, 0.4869381619756998088861177134M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(1.1263452329180597013141657562M, -0.6502957234256934994051155751M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(1.2589858696615772964532950315M, -0.4869381619756998088861177134M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(1.0511937531738464642621031890M, -0.8468446651447202192255332969M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexCopySignTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(-TestValue9, -TestValue9), new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(-TestValue9, -TestValue9), new GeneralComplexNumberDecimal(-TestValue0_0, -TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(-TestValue9, -TestValue9), new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue0_0), new GeneralComplexNumberDecimal(-TestValue9, TestValue9), new GeneralComplexNumberDecimal(-TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDecimal(-TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(-TestValue9, TestValue9), new GeneralComplexNumberDecimal(-TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-TestValue9, TestValue9), new GeneralComplexNumberDecimal(-TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(TestValue9, TestValue9), new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(TestValue9, TestValue9), new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue9, TestValue9), new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue0_0), new GeneralComplexNumberDecimal(TestValue9, -TestValue9), new GeneralComplexNumberDecimal(TestValue1_1, -TestValue0_0) },
        { new GeneralComplexNumberDecimal(-TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue9, -TestValue9), new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue9, -TestValue9), new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, int, GeneralComplexNumberDecimal> TestComplexScaleBTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), 3, new GeneralComplexNumberDecimal(8.8M, -TestValue0_0) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), 3, new GeneralComplexNumberDecimal(TestValue0_0, 17.6M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), 3, new GeneralComplexNumberDecimal(8.8M, 17.6M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), 0, new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), 1, new GeneralComplexNumberDecimal(TestValue2_2, 4.4M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), -1, new GeneralComplexNumberDecimal(0.55M, 1.1M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), 2, new GeneralComplexNumberDecimal(4.4M, 8.8M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), -2, new GeneralComplexNumberDecimal(0.275M, 0.55M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexExpTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(0.0953101798043248600439521233M, 0.0M), new GeneralComplexNumberDecimal(1.1000000000000000000000000001M, TestValue0_0) },
        { new GeneralComplexNumberDecimal(0.7884573603642701694611842447M, 1.5707963267948966192313216916M), new GeneralComplexNumberDecimal(-0.0000000000000000000000000002M, 2.2000000000000000000000000004M) },
        { new GeneralComplexNumberDecimal(0.9000291360213750473443317899M, 1.1071487177940905030170654602M), new GeneralComplexNumberDecimal(TestValue1_1, 2.2000000000000000000000000001M) },
        { new GeneralComplexNumberDecimal(0.7884573603642701694611842447M, -1.5707963267948966192313216916M), new GeneralComplexNumberDecimal(-0.0000000000000000000000000002M, -2.2000000000000000000000000004M) },
        { new GeneralComplexNumberDecimal(0.9000291360213750473443317899M, -1.1071487177940905030170654602M), new GeneralComplexNumberDecimal(TestValue1_1, -2.2000000000000000000000000001M) },
        { new GeneralComplexNumberDecimal(0.9000291360213750473443317899M, -2.0344439357957027354455779231M), new GeneralComplexNumberDecimal(-1.1000000000000000000000000006M, -2.1999999999999999999999999997M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexLogTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.0953101798043248600439521231M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(0.7884573603642701694611842448M, 1.5707963267948966192313216916M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(0.9000291360213750473443317901M, 1.1071487177940905030170654599M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(0.7884573603642701694611842448M, -1.5707963267948966192313216916M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.9000291360213750473443317901M, -1.1071487177940905030170654599M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.9000291360213750473443317901M, -2.0344439357957027354455779234M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, decimal, GeneralComplexNumberDecimal> TestComplexLogBaseTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), TestValue3_3, new GeneralComplexNumberDecimal(0.7538421964475786608696944862M, 0.9273204475418183380248195201M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), -TestValue3_3, new GeneralComplexNumberDecimal(0.4030768491608407790189629817M, -0.1333039243036933317717711430M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), TestValue3_3, new GeneralComplexNumberDecimal(0.7538421964475786608696944862M, -0.9273204475418183380248195201M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), -TestValue3_3, new GeneralComplexNumberDecimal(-0.2128050916761761672681486859M, -0.3673620496470617315427931544M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), TestValue3_3, new GeneralComplexNumberDecimal(0.7538421964475786608696944862M, -1.7040000414756197051471099628M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), -TestValue3_3, new GeneralComplexNumberDecimal(-0.4707218328568911005415265077M, -0.4653800380514402798071040265M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexLogComplexBaseTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(1.0409415604699568577092817672M, -0.0563282321468961432894236888M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(-TestValue1, -TestValue2), new GeneralComplexNumberDecimal(-0.3192625929590507142699496361M, 0.5686791246641219235153944712M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(-0.2677062697005214361228723354M, -1.0075046179613546327108158234M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-TestValue1, -TestValue2), new GeneralComplexNumberDecimal(0.6218909236011442913323171253M, 0.1964083227213249657412452132M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(TestValue1, TestValue2), new GeneralComplexNumberDecimal(-0.8157369251408684863637588850M, -1.4058347157458529073366209726M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-TestValue1, -TestValue2), new GeneralComplexNumberDecimal(1.0160236815970675417934835002M, 0.0405101453152329206938306994M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexLog2TheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.137503523749934908329043617M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(1.1375035237499349083290436173M, 2.2661800709135969048138414727M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(1.2984675711936160822642033322M, 1.5972779646881088066382317414M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(1.1375035237499349083290436173M, -2.2661800709135969048138414727M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(1.2984675711936160822642033322M, -1.5972779646881088066382317414M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(1.2984675711936160822642033322M, -2.9350821771390850029894512041M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexLog10TheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.0413926851582250407501999712M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(0.3424226808222062359639388660M, 0.6821881769209206737428918127M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(0.390877687326234443143330524M, 0.4808285787842341027039415824M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDecimal(0.3424226808222062359639388660M, -0.6821881769209206737428918127M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.390877687326234443143330524M, -0.4808285787842341027039415824M) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(0.390877687326234443143330524M, -0.883547775057607244781842043M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexRoundTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(TestValue1, TestValue2) },
        { new GeneralComplexNumberDecimal(1.5M, 2.5M), new GeneralComplexNumberDecimal(TestValue2, TestValue2) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDecimal(-TestValue1, -TestValue2) },
        { new GeneralComplexNumberDecimal(-1.5M, -2.5M), new GeneralComplexNumberDecimal(-TestValue2, -TestValue2) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, int, GeneralComplexNumberDecimal> TestComplexRoundDigitsTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(1.11M, 2.22M), 1, new GeneralComplexNumberDecimal(1.1M, 2.2M) },
        { new GeneralComplexNumberDecimal(1.115M, 2.225M), 2, new GeneralComplexNumberDecimal(1.12M, 2.22M) },
        { new GeneralComplexNumberDecimal(-1.11M, -2.22M), 1, new GeneralComplexNumberDecimal(-1.1M, -2.2M) },
        { new GeneralComplexNumberDecimal(-1.115M, -2.225M), 2, new GeneralComplexNumberDecimal(-1.12M, -2.22M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, MidpointRounding, GeneralComplexNumberDecimal> TestComplexRoundAlgorithmTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(TestValue1, TestValue2) },
        { new GeneralComplexNumberDecimal(1.5M, 2.5M), MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(TestValue2, TestValue3) },
        { new GeneralComplexNumberDecimal(-TestValue1_1, -TestValue2_2), MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(-TestValue1, -TestValue2) },
        { new GeneralComplexNumberDecimal(-1.5M, -2.5M), MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(-TestValue2, -TestValue3) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, int, MidpointRounding, GeneralComplexNumberDecimal> TestComplexRoundDigitsAlgorithmTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(1.11M, 2.22M), 1, MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(1.1M, 2.2M) },
        { new GeneralComplexNumberDecimal(1.15M, 2.25M), 1, MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(1.2M, 2.3M) },
        { new GeneralComplexNumberDecimal(-1.11M, -2.22M), 1, MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(-1.1M, -2.2M) },
        { new GeneralComplexNumberDecimal(-1.15M, -2.25M), 1, MidpointRounding.AwayFromZero, new GeneralComplexNumberDecimal(-1.2M, -2.3M) }
    };

    #endregion
}
