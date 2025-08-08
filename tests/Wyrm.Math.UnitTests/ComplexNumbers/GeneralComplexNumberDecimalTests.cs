using Shouldly;
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
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(4.0709535228000319552577517667M, 2.0217256181409679681515773407M) }
    };

    public static readonly TheoryData<GeneralComplexNumberDecimal, GeneralComplexNumberDecimal> TestComplexCosTheoryData = new()
    {
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue0_0), new GeneralComplexNumberDecimal(0.4535961214255773877713700517M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue0_0, TestValue2_2), new GeneralComplexNumberDecimal(4.5679083288982274049029607963M, 0.0M) },
        { new GeneralComplexNumberDecimal(TestValue1_1, TestValue2_2), new GeneralComplexNumberDecimal(2.0719855010158266488314712904M, -3.9722049325494672219512575164M) }
    };

    #endregion
}
