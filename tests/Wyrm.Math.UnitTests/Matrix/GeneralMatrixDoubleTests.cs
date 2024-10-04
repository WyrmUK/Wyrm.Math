using Shouldly;
using System.Numerics;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.UnitTests.TestHelpers;

namespace Wyrm.Math.UnitTests.Matrix;

public class GeneralMatrixDoubleTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void Constructor_Should_Throw_ArgumentOutOfRangeException_If_Parameter_Negative(int columns, int rows)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new GeneralMatrixDouble(columns, rows));
    }

    [Fact]
    public void Constructor_Should_Create_Empty_Matrix()
    {
        Should.NotThrow(() => new GeneralMatrixDouble(0, 0))
            .ShouldMatch(new GeneralMatrixDouble(new GeneralMatrix<double>(0, [])));
    }

    [Theory]
    [InlineData(3, 2)]
    public void Constructor_Should_Create_Initialised_Matrix(int columns, int rows)
    {
        new GeneralMatrixDouble(columns, rows)
            .ShouldMatch(new GeneralMatrixDouble(new GeneralMatrix<double>(columns, new double[columns * rows])));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Populated_Matrix(IEnumerable<IEnumerable<double>> values, GeneralMatrixDouble expected)
    {
        new GeneralMatrixDouble(values)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Copy_Matrix(IEnumerable<IEnumerable<double>> values, GeneralMatrixDouble expected)
    {
        new GeneralMatrixDouble(new GeneralMatrixDouble(values))
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringTheoryData))]
    public void ToString_Should_Return_String_Representation(GeneralMatrixDouble matrix, string expected)
    {
        matrix.ToString().ShouldBe(expected);
    }

    [Fact]
    public void GetHashCode_Should_Return_Unique_Value()
    {
        var hashes = TestGeneralMatrixRankTheoryData.ToDictionary(x => ((GeneralMatrixDouble)x[0]).GetHashCode(), x => x);
        hashes.Count.ShouldBe(TestGeneralMatrixRankTheoryData.Count);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(GeneralMatrixDouble matrix1, object? obj, bool expected)
    {
        matrix1.Equals(obj).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(GeneralMatrixDouble matrix1, object? obj, bool expected)
    {
        (matrix1 == (obj as GeneralMatrixDouble?)).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Not_Equals_Should_Return_False_When_Matrices_Have_The_Same_Values(GeneralMatrixDouble matrix1, object? obj, bool expected)
    {
        (matrix1 != (obj as GeneralMatrixDouble?)).ShouldBe(!expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Rows_Should_Get_Rows(IEnumerable<IEnumerable<double>> values, GeneralMatrixDouble expected)
    {
        new GeneralMatrixDouble(values).Rows
            .ShouldBe(expected.Rows);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Columns_Should_Get_Columns(IEnumerable<IEnumerable<double>> values, GeneralMatrixDouble expected)
    {
        new GeneralMatrixDouble(values).Columns
            .ShouldBe(expected.Columns);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void ArrayGetter_Should_Get_ColumnValue(IEnumerable<IEnumerable<double>> values, GeneralMatrixDouble expected)
    {
        var rowIndex = 0;
        foreach (var row in values)
        {
            var colIndex = 0;
            foreach (var value in row)
            {
                expected[colIndex, rowIndex].ShouldBe(value);
                ++colIndex;
            }
            ++rowIndex;
        }
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
    public void ToEnumerableOfEnumerable_Should_Get_Values(IEnumerable<IEnumerable<double>> expected, GeneralMatrixDouble matrix)
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
    public void Transpose_Should_Transpose_Matrix(GeneralMatrixDouble matrix, GeneralMatrixDouble expected)
    {
        matrix.Transpose()
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Trace_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDouble(columns, rows).Trace());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTraceTheoryData))]
    public void Trace_Should_Return_Trace_Of_Matrix(GeneralMatrixDouble matrix, double expected)
    {
        matrix.Trace().ShouldBe(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Determinant_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDouble(columns, rows).Determinant());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixDeterminantTheoryData))]
    public void Determinant_Should_Return_Determinant_Of_Matrix(GeneralMatrixDouble matrix, double expected)
    {
        System.Math.Round(matrix.Determinant(), 14).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Rank_Should_Return_Rank_Of_Matrix(GeneralMatrixDouble matrix, int expected)
    {
        matrix.Rank().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Nullity_Should_Return_Nullity_Of_Matrix(GeneralMatrixDouble matrix, int expectedRank)
    {
        matrix.Nullity().ShouldBe(matrix.Columns - expectedRank);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixInverseTheoryData))]
    public void Inverse_Should_Return_Inverse_Of_Matrix(GeneralMatrixDouble m, GeneralMatrixDouble expected)
    {
        m.Inverse()
            .ShouldMatch(expected);
    }

    [Fact]
    public void Inverse_Should_Throw_Exception()
    {
        Should.Throw<ArgumentException>(() =>
            new GeneralMatrixDouble([[1D, -2D, 3D], [2D, -3D, 5D], [1D, 1D, 0D]])
                .Inverse());
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Double_To_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m + value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrix_To_Double(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (value + m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Add_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDouble(columns1, rows1) + new GeneralMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrices(GeneralMatrixDouble m1, GeneralMatrixDouble m2, GeneralMatrixDouble expected)
    {
        (m1 + m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Copy_Should_Copy_Matrix(GeneralMatrixDouble matrix)
    {
        (+matrix)
            .ShouldMatch(matrix);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Double_From_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m - value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_Matrix_From_Double(double value, GeneralMatrixDouble m, GeneralMatrixDouble expected)
    {
        (value - m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Subtract_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDouble(columns1, rows1) - new GeneralMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Add_Matrices(GeneralMatrixDouble m1, GeneralMatrixDouble m2, GeneralMatrixDouble expected)
    {
        (m1 - m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Negate_Should_Negate_Matrix(GeneralMatrixDouble matrix)
    {
        (-matrix)
            .ShouldMatch(new GeneralMatrixDouble(new GeneralMatrix<double>(matrix.Columns, matrix.Matrix.Values.Select(v => -v).ToArray())));
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Double_From_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m * value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Matrix_From_Double(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (value * m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 3, 2)]
    [InlineData(3, 2, 3, 4)]
    public void Operator_Multiply_Should_Throw_ArgumentException_If_Rows_And_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new GeneralMatrixDouble(columns1, rows1) * new GeneralMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiple_Should_Multiple_Matrices(GeneralMatrixDouble m1, GeneralMatrixDouble m2, GeneralMatrixDouble expected)
    {
        (m1 * m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Double(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m / value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_Double_By_Matrix(double value, GeneralMatrixDouble m, GeneralMatrixDouble expected)
    {
        (value / m)
            .ShouldMatch(expected);
    }

    #region Test Data

    public const double TestValue = 10.1;
    public const double TestValueNeg4 = -4.0;
    public const double TestValueNeg1 = -1.0;
    public const double TestValue0 = 0.0;
    public const double TestValue1_1 = 1.1;
    public const double TestValue2 = 2.0;
    public const double TestValue2_2 = 2.2;
    public const double TestValue3 = 3.0;
    public const double TestValue3_3 = 3.3;
    public const double TestValue4 = 4.0;
    public const double TestValue4_4 = 4.4;
    public const double TestValue5 = 5.0;
    public const double TestValue5_5 = 5.5;
    public const double TestValue6_6 = 6.6;
    public const double TestValue7_7 = 7.7;
    public const double TestValue8_8 = 8.8;
    public const double TestValue20_2 = 20.2;

    public static readonly TheoryData<GeneralMatrixDouble> TestGeneralMatrixTheoryData =
        new(new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]));

    public static readonly TheoryData<GeneralMatrixDouble, string> TestToStringTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "(  1.1, 10.1 )\r\n( -1.0,  2.0 )" },
            { new GeneralMatrixDouble([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "( 0, -4 )\r\n( 2,  2 )" }
        };

    public static readonly TheoryData<GeneralMatrixDouble, object?, bool> TestGeneralMatrixEqualityTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), null, false },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDecimal([[(decimal)TestValue1_1, (decimal)TestValue2_2], [(decimal)TestValue3_3, (decimal)TestValue4_4]]), false },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue], [TestValue3_3, TestValue4_4, TestValue]]), false },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue5_5]]), false },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), true }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, GeneralMatrixDouble> TestMatrixTheoryData =
        new()
        {
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]], new GeneralMatrixDouble(new GeneralMatrix<double>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue5_5, TestValue6_6])) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble> TestGeneralMatrixTransposeTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]), new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double> TestGeneralMatrixTraceTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1 + TestValue4_4 }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double> TestGeneralMatrixDeterminantTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), -2.42 },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 10.648 },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 0.0 },
            { new GeneralMatrixDouble([[TestValue0, TestValue0, TestValue1_1], [TestValue3_3, TestValue2_2, TestValue1_1], [TestValue1_1, TestValue2_2, TestValue3_3]]), 5.324 },
            { new GeneralMatrixDouble([[TestValue2, TestValue3, TestValue4, TestValue0], [TestValue5, TestValue2, TestValue4, TestValue3], [TestValue2, TestValue2, TestValue3, TestValue4], [TestValue5, TestValue3, TestValue2, TestValue4]]), -119.0 }
        };

    public static readonly TheoryData<GeneralMatrixDouble, int> TestGeneralMatrixRankTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), 2 },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 3 },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 1 },
            { new GeneralMatrixDouble([[TestValue2_2, TestValue3_3, TestValue0], [-TestValue2_2, -TestValue3_3, TestValue0]]), 1 },
            { new GeneralMatrixDouble([[TestValue2_2, -TestValue2_2], [TestValue3_3, -TestValue3_3], [TestValue0, TestValue0]]), 1 },
            { new GeneralMatrixDouble([[TestValue2, TestValue4_4, TestValue0], [TestValue4, TestValue8_8, TestValue3]]), 2 },
            { new GeneralMatrixDouble([[TestValue2, TestValue4], [TestValue4_4, TestValue8_8], [TestValue0, TestValue3]]), 2 }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble> TestGeneralMatrixInverseTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDouble([[-1.818181818181819, 0.9090909090909095], [1.363636363636364, -0.4545454545454547]]) },
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), new GeneralMatrixDouble([[-0.34090909090909088, 0.22727272727272727, 0.11363636363636363], [-1.5909090909090908, -0.45454545454545436, 1.1363636363636362], [1.4772727272727275, 0.22727272727272718, -0.79545454545454553]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestScalarAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDouble([[TestValue1_1 + TestValue20_2, TestValue2_2 + TestValue20_2, TestValue3_3 + TestValue20_2], [TestValue4_4 + TestValue20_2, TestValue5_5 + TestValue20_2, TestValue6_6 + TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new GeneralMatrixDouble([[TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue3_3, TestValue3_3 + TestValue4_4], [TestValue4_4 + TestValue5_5, TestValue5_5 + TestValue6_6, TestValue6_6 + TestValue7_7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestScalarSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDouble([[TestValue1_1 - TestValue20_2, TestValue2_2 - TestValue20_2, TestValue3_3 - TestValue20_2], [TestValue4_4 - TestValue20_2, TestValue5_5 - TestValue20_2, TestValue6_6 - TestValue20_2]]) }
        };

    public static readonly TheoryData<double, GeneralMatrixDouble, GeneralMatrixDouble> TestSubtractionScalarTheoryData =
        new()
        {
            { TestValue20_2, new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue20_2 - TestValue1_1, TestValue20_2 - TestValue2_2, TestValue20_2 - TestValue3_3], [TestValue20_2 - TestValue4_4, TestValue20_2 - TestValue5_5, TestValue20_2 - TestValue6_6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new GeneralMatrixDouble([[TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue3_3, TestValue3_3 - TestValue4_4], [TestValue4_4 - TestValue5_5, TestValue5_5 - TestValue6_6, TestValue6_6 - TestValue7_7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestScalarMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDouble([[TestValue1_1 * TestValue20_2, TestValue2_2 * TestValue20_2, TestValue3_3 * TestValue20_2], [TestValue4_4 * TestValue20_2, TestValue5_5 * TestValue20_2, TestValue6_6 * TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralMatrixDouble([[TestValue5_5, TestValue6_6], [TestValue7_7, TestValue8_8]]), new GeneralMatrixDouble([[TestValue1_1 * TestValue5_5 + TestValue2_2 * TestValue7_7, TestValue1_1 * TestValue6_6 + TestValue2_2 * TestValue8_8], [TestValue3_3 * TestValue5_5 + TestValue4_4 * TestValue7_7, TestValue3_3 * TestValue6_6 + TestValue4_4 * TestValue8_8]]) },
            { new GeneralMatrixDouble([[TestValueNeg1, TestValue2, TestValue3], [TestValue4, TestValue0, TestValue5]]), new GeneralMatrixDouble([[TestValue5, TestValueNeg1], [TestValueNeg4, TestValue0], [TestValue2, TestValue3]]), new GeneralMatrixDouble([[TestValueNeg1 * TestValue5 + TestValue2 * TestValueNeg4 + TestValue3 * TestValue2, TestValueNeg1 * TestValueNeg1 + TestValue2 * TestValue0 + TestValue3 * TestValue3], [TestValue4 * TestValue5 + TestValue0 * TestValueNeg4 + TestValue5 * TestValue2, TestValue4 * TestValueNeg1 + TestValue0 * TestValue0 + TestValue5 * TestValue3]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestScalarDivisionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new GeneralMatrixDouble([[TestValue1_1 / TestValue20_2, TestValue2_2 / TestValue20_2, TestValue3_3 / TestValue20_2], [TestValue4_4 / TestValue20_2, TestValue5_5 / TestValue20_2, TestValue6_6 / TestValue20_2]]) }
        };

    public static readonly TheoryData<double, GeneralMatrixDouble, GeneralMatrixDouble> TestDivisionScalarTheoryData =
        new()
        {
            { TestValue20_2, new GeneralMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new GeneralMatrixDouble([[TestValue20_2 / TestValue1_1, TestValue20_2 / TestValue2_2, TestValue20_2 / TestValue3_3], [TestValue20_2 / TestValue4_4, TestValue20_2 / TestValue5_5, TestValue20_2 / TestValue6_6]]) }
        };

    #endregion
}
