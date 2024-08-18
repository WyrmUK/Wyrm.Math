using Shouldly;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.UnitTests.TestHelpers;

namespace Wyrm.Math.UnitTests.Matrix;

public class GeneralMatrixDecimalTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void Constructor_Should_Throw_ArgumentOutOfRangeException_If_Parameter_Negative(int columns, int rows)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new GeneralMatrixDecimal(columns, rows));
    }

    [Fact]
    public void Constructor_Should_Create_Empty_Matrix()
    {
        Should.NotThrow(() => new GeneralMatrixDecimal(0, 0))
            .ShouldMatch(new GeneralMatrixDecimal(new GeneralMatrix<decimal>(0, [])));
    }

    [Theory]
    [InlineData(3, 2)]
    public void Constructor_Should_Create_Initialised_Matrix(int columns, int rows)
    {
        new GeneralMatrixDecimal(columns, rows)
            .ShouldMatch(new GeneralMatrixDecimal(new GeneralMatrix<decimal>(columns, new decimal[columns * rows])));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Populated_Matrix(IEnumerable<IEnumerable<decimal>> values, GeneralMatrixDecimal expected)
    {
        new GeneralMatrixDecimal(values)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Copy_Matrix(IEnumerable<IEnumerable<decimal>> values, GeneralMatrixDecimal expected)
    {
        new GeneralMatrixDecimal(new GeneralMatrixDecimal(values))
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Rows_Should_Get_Rows(IEnumerable<IEnumerable<decimal>> values, GeneralMatrixDecimal expected)
    {
        new GeneralMatrixDecimal(values).Rows
            .ShouldBe(expected.Rows);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Columns_Should_Get_Columns(IEnumerable<IEnumerable<decimal>> values, GeneralMatrixDecimal expected)
    {
        new GeneralMatrixDecimal(values).Columns
            .ShouldBe(expected.Columns);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void ArraySetter_Should_Set_ColumnValue(GeneralMatrixDecimal matrix)
    {
        matrix[2, 1] = TestValue;
        matrix[2, 1].ShouldBe(TestValue);
    }

    [Theory]
    [InlineData(2, 3, false)]
    [InlineData(3, 3, true)]
    public void IsSquare_Should_Return_Expected(int columns, int rows, bool expected)
    {
        new GeneralMatrixDouble(columns, rows).IsSquare.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void ToEnumerableOfEnumerable_Should_Get_Values(IEnumerable<IEnumerable<decimal>> expected, GeneralMatrixDecimal matrix)
    {
        matrix.ToEnumerableOfEnumerable()
            .SelectMany(v => v.Select(c => c))
            .ToArray()
            .ShouldBeEquivalentTo(expected
                .SelectMany(v => v.Select(c => c))
                .ToArray());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTransposeTheoryData))]
    public void Transpose_Should_Transpose_Matrix(GeneralMatrixDecimal matrix, GeneralMatrixDecimal expected)
    {
        matrix.Transpose()
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Trace_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDecimal(columns, rows).Trace());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTraceTheoryData))]
    public void Trace_Should_Return_Trace_Of_Matrix(GeneralMatrixDecimal matrix, decimal expected)
    {
        matrix.Trace().ShouldBe(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Determinant_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDecimal(columns, rows).Determinant());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixDeterminantTheoryData))]
    public void Determinant_Should_Return_Determinant_Of_Matrix(GeneralMatrixDecimal matrix, decimal expected)
    {
        System.Math.Round(matrix.Determinant(), 26).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Rank_Should_Return_Rank_Of_Matrix(GeneralMatrixDecimal matrix, int expected)
    {
        matrix.Rank().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Nullity_Should_Return_Nullity_Of_Matrix(GeneralMatrixDecimal matrix, int expectedRank)
    {
        matrix.Nullity().ShouldBe(matrix.Columns - expectedRank);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Decimal_To_Matrix(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (m + value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrix_To_Decimal(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (value + m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Add_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDecimal(columns1, rows1) + new GeneralMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrices(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2, GeneralMatrixDecimal expected)
    {
        (m1 + m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Copy_Should_Copy_Matrix(GeneralMatrixDecimal matrix)
    {
        (+matrix)
            .ShouldMatch(matrix);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Decimal_From_Matrix(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (m - value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_Matrix_From_Decimal(decimal value, GeneralMatrixDecimal m, GeneralMatrixDecimal expected)
    {
        (value - m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Subtract_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDecimal(columns1, rows1) - new GeneralMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Add_Matrices(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2, GeneralMatrixDecimal expected)
    {
        (m1 - m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Negate_Should_Negate_Matrix(GeneralMatrixDecimal matrix)
    {
        (-matrix)
            .ShouldMatch(new GeneralMatrixDecimal(new GeneralMatrix<decimal>(matrix.Columns, matrix.Matrix.Values.Select(v => -v).ToArray())));
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Decimal_From_Matrix(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (m * value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Matrix_From_Decimal(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (value * m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 3, 2)]
    [InlineData(3, 2, 3, 4)]
    public void Operator_Multiply_Should_Throw_ArgumentException_If_Rows_And_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDecimal(columns1, rows1) * new GeneralMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiple_Should_Multiple_Matrices(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2, GeneralMatrixDecimal expected)
    {
        (m1 * m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Decimal(GeneralMatrixDecimal m, decimal value, GeneralMatrixDecimal expected)
    {
        (m / value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_Decimal_By_Matrix(decimal value, GeneralMatrixDecimal m, GeneralMatrixDecimal expected)
    {
        (value / m)
            .ShouldMatch(expected);
    }

    #region Test Data

    public const decimal TestValue = 10.1M;
    public const decimal TestValueNeg4 = -4.0M;
    public const decimal TestValueNeg1 = -1.0M;
    public const decimal TestValue0 = 0.0M;
    public const decimal TestValue1_1 = 1.1M;
    public const decimal TestValue2 = 2.0M;
    public const decimal TestValue2_2 = 2.2M;
    public const decimal TestValue3 = 3.0M;
    public const decimal TestValue3_3 = 3.3M;
    public const decimal TestValue4 = 4.0M;
    public const decimal TestValue4_4 = 4.4M;
    public const decimal TestValue5 = 5.0M;
    public const decimal TestValue5_5 = 5.5M;
    public const decimal TestValue6_6 = 6.6M;
    public const decimal TestValue7_7 = 7.7M;
    public const decimal TestValue8_8 = 8.8M;
    public const decimal TestValue20_2 = 20.2M;

    public static readonly TheoryData<GeneralMatrixDecimal> TestGeneralMatrixTheoryData =
        new(new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]));

    public static readonly TheoryData<IEnumerable<IEnumerable<decimal>>, GeneralMatrixDecimal> TestMatrixTheoryData =
        new()
        {
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]], new GeneralMatrixDecimal(new GeneralMatrix<decimal>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue5_5, TestValue6_6])) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, GeneralMatrixDecimal> TestGeneralMatrixTransposeTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal> TestGeneralMatrixTraceTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1 + TestValue4_4 }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal> TestGeneralMatrixDeterminantTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), -2.42M },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 10.648M },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 0.0M },
            { new GeneralMatrixDecimal([[TestValue0, TestValue0, TestValue1_1], [TestValue3_3, TestValue2_2, TestValue1_1], [TestValue1_1, TestValue2_2, TestValue3_3]]), 5.324M },
            { new GeneralMatrixDecimal([[TestValue2, TestValue3, TestValue4, TestValue0], [TestValue5, TestValue2, TestValue4, TestValue3], [TestValue2, TestValue2, TestValue3, TestValue4], [TestValue5, TestValue3, TestValue2, TestValue4]]), -119.0M }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, int> TestGeneralMatrixRankTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), 2 },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 3 },
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 1 },
            { new GeneralMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue0], [-TestValue2_2, -TestValue3_3, TestValue0]]), 1 },
            { new GeneralMatrixDecimal([[TestValue2_2, -TestValue2_2], [TestValue3_3, -TestValue3_3], [TestValue0, TestValue0]]), 1 },
            { new GeneralMatrixDecimal([[TestValue2, TestValue4_4, TestValue0], [TestValue4, TestValue8_8, TestValue3]]), 2 },
            { new GeneralMatrixDecimal([[TestValue2, TestValue4], [TestValue4_4, TestValue8_8], [TestValue0, TestValue3]]), 2 }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal, GeneralMatrixDecimal> TestScalarAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1 + TestValue20_2, TestValue2_2 + TestValue20_2, TestValue3_3 + TestValue20_2], [TestValue4_4 + TestValue20_2, TestValue5_5 + TestValue20_2, TestValue6_6 + TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, GeneralMatrixDecimal, GeneralMatrixDecimal> TestAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new GeneralMatrixDecimal([[TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue3_3, TestValue3_3 + TestValue4_4], [TestValue4_4 + TestValue5_5, TestValue5_5 + TestValue6_6, TestValue6_6 + TestValue7_7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal, GeneralMatrixDecimal> TestScalarSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1 - TestValue20_2, TestValue2_2 - TestValue20_2, TestValue3_3 - TestValue20_2], [TestValue4_4 - TestValue20_2, TestValue5_5 - TestValue20_2, TestValue6_6 - TestValue20_2]]) }
        };

    public static readonly TheoryData<Decimal, GeneralMatrixDecimal, GeneralMatrixDecimal> TestSubtractionScalarTheoryData =
        new()
        {
            { TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue20_2 - TestValue1_1, TestValue20_2 - TestValue2_2, TestValue20_2 - TestValue3_3], [TestValue20_2 - TestValue4_4, TestValue20_2 - TestValue5_5, TestValue20_2 - TestValue6_6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, GeneralMatrixDecimal, GeneralMatrixDecimal> TestSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new GeneralMatrixDecimal([[TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue3_3, TestValue3_3 - TestValue4_4], [TestValue4_4 - TestValue5_5, TestValue5_5 - TestValue6_6, TestValue6_6 - TestValue7_7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal, GeneralMatrixDecimal> TestScalarMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1 * TestValue20_2, TestValue2_2 * TestValue20_2, TestValue3_3 * TestValue20_2], [TestValue4_4 * TestValue20_2, TestValue5_5 * TestValue20_2, TestValue6_6 * TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, GeneralMatrixDecimal, GeneralMatrixDecimal> TestMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDecimal([[TestValue5_5, TestValue6_6], [TestValue7_7, TestValue8_8]]), new GeneralMatrixDecimal([[TestValue1_1 * TestValue5_5 + TestValue2_2 * TestValue7_7, TestValue1_1 * TestValue6_6 + TestValue2_2 * TestValue8_8], [TestValue3_3 * TestValue5_5 + TestValue4_4 * TestValue7_7, TestValue3_3 * TestValue6_6 + TestValue4_4 * TestValue8_8]]) },
            { new GeneralMatrixDecimal([[TestValueNeg1, TestValue2, TestValue3], [TestValue4, TestValue0, TestValue5]]), new GeneralMatrixDecimal([[TestValue5, TestValueNeg1], [TestValueNeg4, TestValue0], [TestValue2, TestValue3]]), new GeneralMatrixDecimal([[TestValueNeg1 * TestValue5 + TestValue2 * TestValueNeg4 + TestValue3 * TestValue2, TestValueNeg1 * TestValueNeg1 + TestValue2 * TestValue0 + TestValue3 * TestValue3], [TestValue4 * TestValue5 + TestValue0 * TestValueNeg4 + TestValue5 * TestValue2, TestValue4 * TestValueNeg1 + TestValue0 * TestValue0 + TestValue5 * TestValue3]]) }
        };

    public static readonly TheoryData<GeneralMatrixDecimal, decimal, GeneralMatrixDecimal> TestScalarDivisionTheoryData =
        new()
        {
            { new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1 / TestValue20_2, TestValue2_2 / TestValue20_2, TestValue3_3 / TestValue20_2], [TestValue4_4 / TestValue20_2, TestValue5_5 / TestValue20_2, TestValue6_6 / TestValue20_2]]) }
        };

    public static readonly TheoryData<decimal, GeneralMatrixDecimal, GeneralMatrixDecimal> TestDivisionScalarTheoryData =
        new()
        {
            { TestValue20_2, new GeneralMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDecimal([[TestValue20_2 / TestValue1_1, TestValue20_2 / TestValue2_2, TestValue20_2 / TestValue3_3], [TestValue20_2 / TestValue4_4, TestValue20_2 / TestValue5_5, TestValue20_2 / TestValue6_6]]) }
        };

    #endregion
}
