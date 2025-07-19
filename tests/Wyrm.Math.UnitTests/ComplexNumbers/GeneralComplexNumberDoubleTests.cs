using Shouldly;
using Wyrm.Math.ComplexNumbers;

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

    public static readonly TheoryData<GeneralComplexNumberDouble, double, GeneralComplexNumberDouble> TestPowerTheoryData = new()
    {
        { new GeneralComplexNumberDouble(-TestValue1, TestValue0_0), 1/TestValue2, new GeneralComplexNumberDouble(6.123233995736766E-17, TestValue1) },
        { new GeneralComplexNumberDouble(-TestValue1, TestValue1), 1/TestValue2, new GeneralComplexNumberDouble(0.4550898605622274, 1.0986841134678098 ) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), TestValue3, new GeneralComplexNumberDouble(Error1 -TestValue46, Error7 + TestValue9) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), 1/TestValue5, new GeneralComplexNumberDouble(1.2675064916851109046661051638234937044436136265775244534793767519, 0.25239838721931698566646353755954970269087802267183946002182012880 ) }
    };

    public static readonly TheoryData<GeneralComplexNumberDouble, GeneralComplexNumberDouble, GeneralComplexNumberDouble> TestComplexPowerTheoryData = new()
    {
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue4, TestValue5), new GeneralComplexNumberDouble(-0.7530458367485597, -0.986428788647745) },
        { new GeneralComplexNumberDouble(TestValue2, TestValue3), new GeneralComplexNumberDouble(TestValue1, TestValue1), new GeneralComplexNumberDouble(-0.8636068988831277, 1.0368893969147763) }
    };

    #endregion
}
