using Shouldly;
using Wyrm.Math.ComplexNumbers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wyrm.Math.UnitTests.ComplexNumbers;

public class GeneralComplexNumberDoubleTests
{
    [Fact]
    public void Constructor_Should_Set_Real_And_Imaginary()
    {
        const double real = 1.1;
        const double imaginary = 2.2;
        var result = new GeneralComplexNumberDouble(real, imaginary);
        result.Real.ShouldBe(real);
        result.Imaginary.ShouldBe(imaginary);
    }

    [Fact]
    public void Constructor_Should_Duplicate_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDouble(1.1, 2.2);
        var result = new GeneralComplexNumberDouble(complexNumber);
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(complexNumber.Real);
        result.Imaginary.ShouldBe(complexNumber.Imaginary);
    }

    [Theory]
    [MemberData(nameof(ToStringTestData))]
    public void ToString_Should_Return_Expected(GeneralComplexNumberDouble complexNumber, string expected)
    {
        var result = complexNumber.ToString();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(GetHashCodeTestData))]
    public void GetHashCode_Should_Return_Expected(GeneralComplexNumberDouble complexNumber, int expected)
    {
        var result = complexNumber.GetHashCode();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Equals_Should_Return_True_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDouble complexNumber1, object? obj, bool expected)
    {
        complexNumber1.Equals(obj).ShouldBe(expected);
    }

    [Fact]
    public void Operator_Cast_Should_Cast_To_Double()
    {
        var result = (double)new GeneralComplexNumberDouble(TestValue2_2, TestValue0_0);
        result.ShouldBe(TestValue2_2);
    }

    [Fact]
    public void Operator_Cast_Should_Cast_To_DoubleNan()
    {
        var result = (double)new GeneralComplexNumberDouble(TestValue2_2, TestValue2_2);
        result.ShouldBe(double.NaN);
    }

    [Fact]
    public void Operator_Cast_Should_Cast_From_Double()
    {
        var result = (GeneralComplexNumberDouble)TestValue2_2;
        result.Real.ShouldBe(TestValue2_2);
        result.Imaginary.ShouldBe(TestValue0_0);
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Operator_Equals_Should_Return_True_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDouble complexNumber1, object? obj, bool expected)
    {
        if (obj is double doubleValue)
        {
            (complexNumber1 == doubleValue).ShouldBe(expected);
        }
        else
        {
            (complexNumber1 == (obj as GeneralComplexNumberDouble?)).ShouldBe(expected);
        }
    }

    [Theory]
    [MemberData(nameof(TestGeneralComplexNumberEqualityTheoryData))]
    public void Operator_Not_Equals_Should_Return_False_When_ComplexNumbers_Have_The_Same_Values(GeneralComplexNumberDouble complexNumber1, object? obj, bool expected)
    {
        if (obj is double doubleValue)
        {
            (complexNumber1 != doubleValue).ShouldBe(!expected);
        }
        else
        {
            (complexNumber1 != (obj as GeneralComplexNumberDouble?)).ShouldBe(!expected);
        }
    }

    [Fact]
    public void ComplexConjugate_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2);
        var result = complexNumber.ComplexConjugate();
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(complexNumber.Real);
        result.Imaginary.ShouldBe(-complexNumber.Imaginary);
    }

    [Fact]
    public void Abs_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2);
        var result = complexNumber.Abs();
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe(System.Math.Sqrt(System.Math.Pow(complexNumber.Real, 2.0) + System.Math.Pow(complexNumber.Imaginary, 2.0)));
    }

    [Fact]
    public void Argument_Should_Return_Correct_Value()
    {
        var complexNumber = new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2);
        var result = complexNumber.Argument();
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe(System.Math.Atan2(complexNumber.Imaginary, complexNumber.Real));
    }

    [Theory]
    [MemberData(nameof(InverseTestData))]
    public void Inverse_Should_Return_Correct_Value(GeneralComplexNumberDouble complexNumber, GeneralComplexNumberDouble expected)
    {
        var result = complexNumber.Inverse();
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(expected.Real);
        result.Imaginary.ShouldBe(expected.Imaginary);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Double_To_ComplexNumber(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (c + value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_ComplexNumber_To_Double(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (value + c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_ComplexNumbers(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2, GeneralComplexNumberDouble expected)
    {
        (c1 + c2)
            .ShouldBe(expected);
    }

    [Fact]
    public void Operator_Add_Should_Copy_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2);
        var result = +complexNumber;
        result.ShouldNotBeSameAs(complexNumber);
        result.ShouldBe(complexNumber);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Double_From_ComplexNumber(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (c - value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_ComplexNumber_From_Double(double value, GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        (value - c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_ComplexNumbers(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2, GeneralComplexNumberDouble expected)
    {
        (c1 - c2)
            .ShouldBe(expected);
    }

    [Fact]
    public void Operator_Subtract_Should_Copy_Negative_ComplexNumber()
    {
        var complexNumber = new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2);
        var result = -complexNumber;
        result.ShouldNotBeSameAs(complexNumber);
        result.Real.ShouldBe(-complexNumber.Real);
        result.Imaginary.ShouldBe(-complexNumber.Imaginary);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Double_With_ComplexNumber(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (c * value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_ComplexNumber_With_Double(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (value * c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_ComplexNumbers(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2, GeneralComplexNumberDouble expected)
    {
        (c1 * c2)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Double_Into_ComplexNumber(GeneralComplexNumberDouble c, double value, GeneralComplexNumberDouble expected)
    {
        (c / value)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_ComplexNumber_Into_Double(double value, GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        (value / c)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_ComplexNumbers(GeneralComplexNumberDouble c1, GeneralComplexNumberDouble c2, GeneralComplexNumberDouble expected)
    {
        (c1 / c2)
            .ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSqrTheoryData))]
    public void Sqr_Should_Raise_To_Power2(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Sqr();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestSqrtTheoryData))]
    public void Sqrt_Should_Raise_To_PowerHalf(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Sqrt();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestPowerTheoryData))]
    public void Pow_Should_Raise_To_Power(GeneralComplexNumberDouble c, double power, GeneralComplexNumberDouble expected)
    {
        var result = c.Pow(power);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexPowerTheoryData))]
    public void Pow_Should_Raise_To_Complex_Power(GeneralComplexNumberDouble c, GeneralComplexNumberDouble power, GeneralComplexNumberDouble expected)
    {
        var result = c.Pow(power);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexSinTheoryData))]
    public void Sin_Should_Get_Sin(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Sin();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCosTheoryData))]
    public void Cos_Should_Get_Cos(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Cos();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexTanTheoryData))]
    public void Tan_Should_Get_Tan(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Tan();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAsinTheoryData))]
    public void Asin_Should_Get_Asin(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Asin();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAcosTheoryData))]
    public void Acos_Should_Get_Acos(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Acos();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAtanTheoryData))]
    public void Atan_Should_Get_Atan(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Atan();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexSinhTheoryData))]
    public void Sinh_Should_Get_Sinh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Sinh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCoshTheoryData))]
    public void Cosh_Should_Get_Cosh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Cosh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexTanhTheoryData))]
    public void Tanh_Should_Get_Tanh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Tanh();
        result.Real.Round(14).ShouldBe(expected.Real.Round(14));
        result.Imaginary.Round(14).ShouldBe(expected.Imaginary.Round(14));
    }

    [Theory]
    [MemberData(nameof(TestComplexAsinhTheoryData))]
    public void Asinh_Should_Get_Asinh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Asinh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAcoshTheoryData))]
    public void Acosh_Should_Get_Acosh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Acosh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexAtanhTheoryData))]
    public void Atanh_Should_Get_Atanh(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Atanh();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCbrtTheoryData))]
    public void Cbrt_Should_Get_Cbrt(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Cbrt();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexCopySignTheoryData))]
    public void CopySign_Should_Get_CopySign(GeneralComplexNumberDouble c, GeneralComplexNumberDouble s, GeneralComplexNumberDouble expected)
    {
        var result = c.CopySign(s);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexScaleBTheoryData))]
    public void ScaleB_Should_Get_ScaleB(GeneralComplexNumberDouble c, int n, GeneralComplexNumberDouble expected)
    {
        var result = c.ScaleB(n);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexExpTheoryData))]
    public void Exp_Should_Get_Exp(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Exp();
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexLogTheoryData))]
    public void Log_Should_Get_Log(GeneralComplexNumberDouble c, GeneralComplexNumberDouble expected)
    {
        var result = c.Log();
        result.ShouldBe(expected);
    }

    #region Test Data

    public const double TestValue0_0 = 0.0;
    public const double TestValue1 = 1.0;
    public const double TestValue2 = 2.0;
    public const double TestValue3 = TestValue1 + TestValue2;
    public const double TestValue4 = 2 * TestValue2;
    public const double TestValue5 = TestValue1 + TestValue4;
    public const double TestValue6 = 2 * TestValue3;
    public const double TestValue7 = TestValue1 + TestValue6;
    public const double TestValue8 = 2 * TestValue4;
    public const double TestValue9 = TestValue1 + TestValue8;
    public const double TestValue10 = 2 * TestValue5;
    public const double TestValue12 = 2 * TestValue6;
    public const double TestValue46 = 4 * TestValue10 + TestValue6;
    public const double TestValue1_1 = 1.1;
    public const double TestValue2_2 = 2.2;
    public const double TestValue3_3 = TestValue1_1 + TestValue2_2;
    public const double TestValueNeg1_1 = -1.1;
    public const double TestValueNeg2_2 = -2.2;
    public const double TestValueNeg3_3 = TestValueNeg1_1 - TestValueNeg2_2;
    public const double Error1 = 0.00000000000001;
    public const double Error2 = 0.0000000000000002;
    public const double Error7 = 0.000000000000007;

    public static readonly TheoryData<GeneralComplexNumberDouble, string> ToStringTestData = new()
    {
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), "2.2i" },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValueNeg2_2), "-2.2i" },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), "1.1" },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue0_0), "-1.1" },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), "(1.1+2.2i)" },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), "(1.1-2.2i)" },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), "(-1.1+2.2i)" },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), "(-1.1-2.2i)" }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, int> GetHashCodeTestData = new()
    {
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), TestValue0_0.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValueNeg2_2), TestValue0_0.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), TestValue1_1.GetHashCode() * 16777619 + TestValue0_0.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue0_0), TestValueNeg1_1.GetHashCode() * 16777619 + TestValue0_0.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue1_1.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), TestValue1_1.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), TestValueNeg1_1.GetHashCode() * 16777619 + TestValue2_2.GetHashCode() },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), TestValueNeg1_1.GetHashCode() * 16777619 + TestValueNeg2_2.GetHashCode() }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, object?, bool> TestGeneralComplexNumberEqualityTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), null, false },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue1_1, false },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), TestValue1_1, true },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), TestValue2_2, false },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), false },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), false },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), true },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), true },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), true }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> InverseTestData = new()
    {
        { new GeneralComplexNumberDouble(TestValue2, TestValue4), new GeneralComplexNumberDouble(TestValue1 / TestValue10, -TestValue1 / TestValue5) },
        { new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(TestValue1 / TestValue5, -TestValue2 / TestValue5) },
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestScalarAdditionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValue3_3, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1 + TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), TestValue4, new GeneralComplexNumberDouble(TestValue6, TestValue3) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestAdditionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 + TestValue2_2, TestValue2_2 + TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 + TestValue2_2, TestValueNeg2_2 + TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 + TestValue2_2, TestValueNeg2_2 + TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(TestValue3, TestValue5) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestScalarSubtractionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValue1_1 - TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1 - TestValue2_2, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), TestValue1, new GeneralComplexNumberDouble(TestValue1, TestValue3) }
    };

    public static readonly TheoryData<double, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestSubtractionScalarTheoryData = new()
    {
        { TestValue2_2, new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2 - TestValue1_1, -TestValue2_2) },
        { TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2 - TestValueNeg1_1, -TestValue2_2) },
        { TestValue1, new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(-TestValue1, -TestValue3) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestSubtractionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 - TestValue2_2, TestValue2_2 - TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 - TestValue2_2, TestValueNeg2_2 - TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 - TestValue2_2, TestValueNeg2_2 - TestValue1_1) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(TestValue1, TestValue1) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestScalarMultiplicationTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValue1_1 * TestValue2_2, TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1 * TestValue2_2, TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), TestValue2, new GeneralComplexNumberDouble(TestValue4, TestValue6) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestMultiplicationTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 * TestValue2_2 - TestValue2_2 * TestValue1_1, TestValue1_1 * TestValue1_1 + TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 * TestValue2_2 - TestValue2_2 * TestValue1_1, TestValueNeg1_1 * TestValue1_1 + TestValue2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValue1_1 * TestValue2_2 - TestValueNeg2_2 * TestValue1_1, TestValue1_1 * TestValue1_1 + TestValueNeg2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValueNeg2_2), new GeneralComplexNumberDouble(TestValue2_2, TestValue1_1), new GeneralComplexNumberDouble(TestValueNeg1_1 * TestValue2_2 - TestValueNeg2_2 * TestValue1_1, TestValueNeg1_1 * TestValue1_1 + TestValueNeg2_2 * TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(-TestValue4, TestValue7) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestScalarDivisionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValue1_1 / TestValue2_2, TestValue2_2 / TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1 / TestValue2_2, TestValue2_2 / TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue4), TestValue2, new GeneralComplexNumberDouble(TestValue1, TestValue2) }
    };

    public static readonly TheoryData<double, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestDivisionScalarTheoryData = new()
    {
        { TestValue2_2, new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue2 / TestValue5, -TestValue4 / TestValue5) },
        { TestValue2_2, new GeneralComplexNumberDouble(TestValueNeg1_1, TestValue2_2), new GeneralComplexNumberDouble(-TestValue2 / TestValue5, -TestValue4 / TestValue5) },
        { TestValue2, new GeneralComplexNumberDouble(TestValue2, TestValue4), new GeneralComplexNumberDouble(TestValue1 / TestValue5, -TestValue2 / TestValue5) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestDivisionTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue2, TestValue4), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(TestValue2, TestValue0_0) },
        { new GeneralComplexNumberDouble(-TestValue2, TestValue4), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(Error2 + TestValue6 / TestValue5, TestValue8 / TestValue5) },
        { new GeneralComplexNumberDouble(TestValue2, -TestValue4), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(-Error2 -TestValue6 / TestValue5, -TestValue8 / TestValue5) },
        { new GeneralComplexNumberDouble(-TestValue2, -TestValue4), new GeneralComplexNumberDouble(TestValue1, TestValue2), new GeneralComplexNumberDouble(-TestValue2, TestValue0_0) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestSqrTheoryData = new()
    {
        { new GeneralComplexNumberDouble(-TestValue1, TestValue0_0), new GeneralComplexNumberDouble(TestValue1, TestValue0_0) },
        { new GeneralComplexNumberDouble(-TestValue1, TestValue1), new GeneralComplexNumberDouble(TestValue0_0, -TestValue2) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(-TestValue5, TestValue12) },
        { new GeneralComplexNumberDouble(-TestValue2, -TestValue3), new GeneralComplexNumberDouble(-TestValue5, TestValue12 ) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestSqrtTheoryData = new()
    {
        { new GeneralComplexNumberDouble(-TestValue1, TestValue0_0), new GeneralComplexNumberDouble(6.123233995736766E-17, TestValue1) },
        { new GeneralComplexNumberDouble(-TestValue1, TestValue1), new GeneralComplexNumberDouble(0.4550898605622274, 1.0986841134678098) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(1.6741492280355401, 0.895977476129838) },
        { new GeneralComplexNumberDouble(-TestValue2, -TestValue3), new GeneralComplexNumberDouble(0.8959774761298382, -1.67414922803554 ) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestPowerTheoryData = new()
    {
        { new GeneralComplexNumberDouble(-TestValue1, TestValue0_0), 1/TestValue2, new GeneralComplexNumberDouble(6.123233995736766E-17, TestValue1) },
        { new GeneralComplexNumberDouble(-TestValue1, TestValue1), 1/TestValue2, new GeneralComplexNumberDouble(0.4550898605622274, 1.0986841134678098) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), TestValue3, new GeneralComplexNumberDouble(Error1 -TestValue46, Error7 + TestValue9) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), 1/TestValue5, new GeneralComplexNumberDouble(1.2675064916851109046661051638234937044436136265775244534793767519, 0.25239838721931698566646353755954970269087802267183946002182012880 ) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexPowerTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue4, TestValue5), new GeneralComplexNumberDouble(-0.7530458367485594, -0.9864287886477446) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue1, TestValue1), new GeneralComplexNumberDouble(-0.8636068988831277, 1.0368893969147763) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexSinTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(0.8912073600614354, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, 4.457105170535894) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(4.070953522800033, 2.0217256181409677) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, -4.457105170535894) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(4.070953522800033, -2.0217256181409677) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-4.070953522800033, -2.0217256181409677) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexCosTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(0.4535961214255773, -TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(4.567908328898228, -TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(2.0719855010158263, -3.972204932549468) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(4.567908328898228, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(2.0719855010158263, 3.972204932549468) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(2.0719855010158263, -3.972204932549468) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexTanTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(1.9647596572486525, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, 0.9757431300314515) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(0.020140372070480916, 1.0143542521857927) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, -0.9757431300314515) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(0.020140372070480916, -1.0143542521857927) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-0.020140372070480916, -1.0143542521857927) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAsinTheoryData = new()
    {
        { new GeneralComplexNumberDouble(0.8912073600614353, TestValue0_0), new GeneralComplexNumberDouble(1.0999999999999999, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, 4.457105170535893), new GeneralComplexNumberDouble(TestValue0_0, 2.200000000000002) },
        { new GeneralComplexNumberDouble(4.070953522800032, 2.021725618140968), new GeneralComplexNumberDouble(1.1000000000000014, 2.199999999999999) },
        { new GeneralComplexNumberDouble(TestValue0_0, -4.457105170535893), new GeneralComplexNumberDouble(TestValue0_0, -2.1999999999999997) },
        { new GeneralComplexNumberDouble(4.070953522800032, -2.021725618140968), new GeneralComplexNumberDouble(1.0999999999999999, -2.1999999999999997) },
        { new GeneralComplexNumberDouble(-4.070953522800032, -2.021725618140968), new GeneralComplexNumberDouble(-1.0999999999999999, -2.1999999999999997) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAcosTheoryData = new()
    {
        { new GeneralComplexNumberDouble(0.45359612142557737, TestValue0_0), new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDouble(4.567908328898228, TestValue0_0), new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDouble(2.071985501015827, -3.972204932549467), new GeneralComplexNumberDouble(1.0999999999999999, 2.1999999999999997) },
        { new GeneralComplexNumberDouble(-4.567908328898228, TestValue0_0), new GeneralComplexNumberDouble(3.1415926535897905, -2.200000000000002) },
        { new GeneralComplexNumberDouble(2.071985501015827, 3.972204932549467), new GeneralComplexNumberDouble(1.0999999999999976, -2.200000000000006) },
        { new GeneralComplexNumberDouble(-2.071985501015827, 3.972204932549467), new GeneralComplexNumberDouble(2.0415926535897952, -2.200000000000006) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAtanTheoryData = new()
    {
        { new GeneralComplexNumberDouble(1.9647596572486519509309227818, TestValue0_0), new GeneralComplexNumberDouble(TestValue1_1, 5.551115123125783E-17) },
        { new GeneralComplexNumberDouble(TestValue0_0, 0.9757431300314515204143066680), new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDouble(0.0201403720704808721675947027, 1.0143542521857925809578028326), new GeneralComplexNumberDouble(1.1000000000000014, 2.199999999999999) },
        { new GeneralComplexNumberDouble(TestValue0_0, -0.9757431300314515204143066680), new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2) },
        { new GeneralComplexNumberDouble(0.0201403720704808721675947027, -1.0143542521857925809578028326), new GeneralComplexNumberDouble(1.1000000000000014, -2.199999999999999) },
        { new GeneralComplexNumberDouble(-0.0201403720704808721675947027, -1.0143542521857925809578028326), new GeneralComplexNumberDouble(-1.1000000000000014, -2.199999999999999) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexSinhTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(1.335647470124177, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(-TestValue0_0, 0.8084964038195901) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(-0.7860300284273543, 1.3489912504715575) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(-TestValue0_0, -0.8084964038195901) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-0.7860300284273543, -1.3489912504715575) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(0.7860300284273543, -1.3489912504715575) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexCoshTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(1.6685185538222564, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(-0.5885011172553458, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(-0.9819250330856718, 1.0798661763661304) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(-0.5885011172553458, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-0.9819250330856718, -1.0798661763661304) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-0.9819250330856718, 1.0798661763661304) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexTanhTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(0.8004990217606297, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, -1.3738230567687948) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(1.0461275040217015, -0.22335059736995666) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(TestValue0_0, 1.3738230567687948) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(1.0461275040217015, 0.22335059736995666) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-1.0461275040217015, 0.22335059736995666) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAsinhTheoryData = new()
    {
        { new GeneralComplexNumberDouble(1.335647470124177, TestValue0_0), new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, 0.8084964038195902), new GeneralComplexNumberDouble(TestValue0_0, 0.9415926535897933) },
        { new GeneralComplexNumberDouble(-0.7860300284273540, 1.3489912504715576), new GeneralComplexNumberDouble(-TestValue1_1, 0.9415926535897935) },
        { new GeneralComplexNumberDouble(TestValue0_0, -0.8084964038195902), new GeneralComplexNumberDouble(TestValue0_0, -0.9415926535897933) },
        { new GeneralComplexNumberDouble(-0.7860300284273540, -1.3489912504715576), new GeneralComplexNumberDouble(-TestValue1_1, -0.9415926535897935) },
        { new GeneralComplexNumberDouble(0.7860300284273540, -1.3489912504715576), new GeneralComplexNumberDouble(1.0999999999999999, -0.9415926535897932) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAcoshTheoryData = new()
    {
        { new GeneralComplexNumberDouble(1.6685185538222563, TestValue0_0), new GeneralComplexNumberDouble(1.0999999999999999, TestValue0_0) },
        { new GeneralComplexNumberDouble(-0.5885011172553457, TestValue0_0), new GeneralComplexNumberDouble(-1.1102230246251565E-16, TestValue2_2) },
        { new GeneralComplexNumberDouble(-0.9819250330856715, 1.0798661763661304), new GeneralComplexNumberDouble(1.0999999999999999, TestValue2_2) },
        { new GeneralComplexNumberDouble(0.5885011172553457, TestValue0_0), new GeneralComplexNumberDouble(-1.1102230246251565E-16, 0.9415926535897932) },
        { new GeneralComplexNumberDouble(-0.9819250330856715, -1.0798661763661304), new GeneralComplexNumberDouble(1.0999999999999999, -TestValue2_2) },
        { new GeneralComplexNumberDouble(0.9819250330856715, 1.0798661763661304), new GeneralComplexNumberDouble(1.0999999999999999, 0.9415926535897932) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexAtanhTheoryData = new()
    {
        { new GeneralComplexNumberDouble(0.8004990217606297, TestValue0_0), new GeneralComplexNumberDouble(1.0999999999999999, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, -1.3738230567687951), new GeneralComplexNumberDouble(TestValue0_0, -0.9415926535897932) },
        { new GeneralComplexNumberDouble(1.0461275040217014, -0.22335059736995658), new GeneralComplexNumberDouble(TestValue1_1, -0.9415926535897929) },
        { new GeneralComplexNumberDouble(TestValue0_0, 1.3738230567687951), new GeneralComplexNumberDouble(TestValue0_0, 0.9415926535897932) },
        { new GeneralComplexNumberDouble(1.0461275040217014, 0.22335059736995658), new GeneralComplexNumberDouble(TestValue1_1, 0.9415926535897929) },
        { new GeneralComplexNumberDouble(-1.0461275040217014, 0.22335059736995658), new GeneralComplexNumberDouble(-1.0999999999999999, 0.9415926535897929) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexCbrtTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(1.0322801154563672, 0.0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(1.12634523291806, 0.6502957234256933) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(1.2589858696615774, 0.4869381619756997) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(1.12634523291806, -0.6502957234256933) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(1.2589858696615774, -0.4869381619756997) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(1.0511937531738462, -0.84684466514472) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexCopySignTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(-TestValue9, -TestValue9), new GeneralComplexNumberDouble(-TestValue1_1, -TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(-TestValue9, -TestValue9), new GeneralComplexNumberDouble(-TestValue0_0, -TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(-TestValue9, -TestValue9), new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue0_0), new GeneralComplexNumberDouble(-TestValue9, TestValue9), new GeneralComplexNumberDouble(-TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDouble(-TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(-TestValue9, TestValue9), new GeneralComplexNumberDouble(-TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(-TestValue9, TestValue9), new GeneralComplexNumberDouble(-TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(TestValue9, TestValue9), new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(TestValue9, TestValue9), new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(TestValue9, TestValue9), new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue0_0), new GeneralComplexNumberDouble(TestValue9, -TestValue9), new GeneralComplexNumberDouble(TestValue1_1, -TestValue0_0) },
        { new GeneralComplexNumberDouble(-TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(TestValue9, -TestValue9), new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(TestValue9, -TestValue9), new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, int, GeneralComplexNumberDouble> TestComplexScaleBTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), 3, new GeneralComplexNumberDouble(8.8, -TestValue0_0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), 3, new GeneralComplexNumberDouble(TestValue0_0, 17.6) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), 3, new GeneralComplexNumberDouble(8.8, 17.6) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), 0, new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), 1, new GeneralComplexNumberDouble(TestValue2_2, 4.4) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), -1, new GeneralComplexNumberDouble(0.55, 1.1) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), 2, new GeneralComplexNumberDouble(4.4, 8.8) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), -2, new GeneralComplexNumberDouble(0.275, 0.55) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexExpTheoryData = new()
    {
        { new GeneralComplexNumberDouble(0.0953101798043249, 0.0), new GeneralComplexNumberDouble(1.1000000000000003, TestValue0_0) },
        { new GeneralComplexNumberDouble(0.7884573603642702, 1.5707963267948966), new GeneralComplexNumberDouble(1.6694137725215975E-16, 2.1999999999999993) },
        { new GeneralComplexNumberDouble(0.9000291360213750, 1.1071487177940905), new GeneralComplexNumberDouble(1.1000000000000005, 2.1999999999999997) },
        { new GeneralComplexNumberDouble(0.7884573603642702, -1.5707963267948966), new GeneralComplexNumberDouble(1.6694137725215975E-16, -2.1999999999999993) },
        { new GeneralComplexNumberDouble(0.9000291360213750, -1.1071487177940905), new GeneralComplexNumberDouble(1.1000000000000005, -2.1999999999999997) },
        { new GeneralComplexNumberDouble(0.9000291360213750, -2.0344439357957027), new GeneralComplexNumberDouble(-1.0999999999999999, -TestValue2_2) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexLogTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue0_0), new GeneralComplexNumberDouble(0.09531017980432493, 0.0) },
        { new GeneralComplexNumberDouble(TestValue0_0, TestValue2_2), new GeneralComplexNumberDouble(0.7884573603642703, 1.5707963267948966) },
        { new GeneralComplexNumberDouble(TestValue1_1, TestValue2_2), new GeneralComplexNumberDouble(0.9000291360213751, 1.1071487177940904) },
        { new GeneralComplexNumberDouble(TestValue0_0, -TestValue2_2), new GeneralComplexNumberDouble(0.7884573603642703, -1.5707963267948966) },
        { new GeneralComplexNumberDouble(TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(0.9000291360213751, -1.1071487177940904) },
        { new GeneralComplexNumberDouble(-TestValue1_1, -TestValue2_2), new GeneralComplexNumberDouble(0.9000291360213751, -2.0344439357957027) }
    };

    #endregion
}
