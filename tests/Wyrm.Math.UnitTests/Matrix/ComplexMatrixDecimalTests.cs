using Shouldly;
using System.Globalization;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.UnitTests.TestHelpers;

namespace Wyrm.Math.UnitTests.Matrix;

public class ComplexMatrixDecimalTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void Constructor_Should_Throw_ArgumentOutOfRangeException_If_Parameter_Negative(int columns, int rows)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new ComplexMatrixDecimal(columns, rows));
    }

    [Fact]
    public void Constructor_Should_Create_Empty_Matrix()
    {
        Should.NotThrow(() => new ComplexMatrixDecimal(0, 0))
            .ShouldMatch(new ComplexMatrixDecimal(new GeneralMatrix<GeneralComplexNumberDecimal>(0, [])));
    }

    [Theory]
    [InlineData(3, 2)]
    public void Constructor_Should_Create_Initialised_Matrix(int columns, int rows)
    {
        new ComplexMatrixDecimal(columns, rows)
            .ShouldMatch(new ComplexMatrixDecimal(new GeneralMatrix<GeneralComplexNumberDecimal>(columns, new GeneralComplexNumberDecimal[columns * rows])));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Populated_Matrix(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values, ComplexMatrixDecimal expected)
    {
        new ComplexMatrixDecimal(values)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Copy_Matrix(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values, ComplexMatrixDecimal expected)
    {
        new ComplexMatrixDecimal(new ComplexMatrixDecimal(values))
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringTheoryData))]
    public void ToString_Should_Return_String_Representation(ComplexMatrixDecimal matrix, string expected)
    {
        matrix.ToString().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringFormatTheoryData))]
    public void ToString_Format_Should_Return_String_Representation(ComplexMatrixDecimal matrix, string? format, string expected)
    {
        matrix.ToString(format).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringProviderTheoryData))]
    public void ToString_Provider_Should_Return_String_Representation(ComplexMatrixDecimal matrix, IFormatProvider? provider, string expected)
    {
        matrix.ToString(provider).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringFormatProviderTheoryData))]
    public void ToString_Format_Provider_Should_Return_String_Representation(ComplexMatrixDecimal matrix, string? format, IFormatProvider? provider, string expected)
    {
        matrix.ToString(format, provider).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseTestData))]
    public void Parse_Should_Return_Expected(string matrix, ComplexMatrixDecimal expected)
    {
        var result = ComplexMatrixDecimal.Parse(matrix);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseProviderTestData))]
    public void Parse_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, ComplexMatrixDecimal expected)
    {
        var result = ComplexMatrixDecimal.Parse(matrix, provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TryParseTestData))]
    public void TryParse_Should_Return_Expected(string matrix, bool expected, ComplexMatrixDecimal expectedNumber)
    {
        var result = ComplexMatrixDecimal.TryParse(matrix, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(TryParseProviderTestData))]
    public void TryParse_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, bool expected, ComplexMatrixDecimal expectedNumber)
    {
        var result = ComplexMatrixDecimal.TryParse(matrix, provider, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(ParseProviderTestData))]
    public void Parse_Span_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, ComplexMatrixDecimal expected)
    {
        var result = ComplexMatrixDecimal.Parse(matrix.AsSpan(), provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TryParseProviderTestData))]
    public void TryParse_Span_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, bool expected, ComplexMatrixDecimal expectedNumber)
    {
        var result = ComplexMatrixDecimal.TryParse(matrix.AsSpan(), provider, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Fact]
    public void ToString_Should_Parse()
    {
        var matrix = new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2], [-TestValue2, TestValue]]);
        var str = matrix.ToString();
        var result = ComplexMatrixDecimal.Parse(str);
        result.ShouldBe(matrix);
    }

    [Fact]
    public void GetHashCode_Should_Return_Unique_Value()
    {
        var hashes = TestGeneralMatrixRankTheoryData.ToDictionary(x => x.Data.Item1.GetHashCode(), x => x);
        hashes.Count.ShouldBe(TestGeneralMatrixRankTheoryData.Count);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(ComplexMatrixDecimal matrix1, object? obj, bool expected)
    {
        matrix1.Equals(obj).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexMatrixDecimalEqualityTheoryData))]
    public void Equals_Should_Return_True_When_Complex_Complex_Decimal_Matrices_Have_The_Same_Values(ComplexMatrixDecimal matrix1, ComplexMatrixDecimal matrix2, bool expected)
    {
        matrix1.Equals(matrix2).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(ComplexMatrixDecimal matrix1, object? obj, bool expected)
    {
        (matrix1 == (obj as ComplexMatrixDecimal?)).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Not_Equals_Should_Return_False_When_Matrices_Have_The_Same_Values(ComplexMatrixDecimal matrix1, object? obj, bool expected)
    {
        (matrix1 != (obj as ComplexMatrixDecimal?)).ShouldBe(!expected);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CompareTo_Should_Throw_ArgumentException_If_Wrong_Type_Or_Null(bool isNull)
    {
        var obj = isNull ? null : new object();
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal().CompareTo(obj));
    }

    [Theory]
    [InlineData(2, 3, -1, 1)]
    [InlineData(4, 3, -1, -1)]
    [InlineData(3, 2, -1, 1)]
    [InlineData(3, 4, -1, -1)]
    [InlineData(3, 3, 0, -1)]
    [InlineData(3, 3, 8, -1)]
    public void CompareTo_Should_Return_Expected(int rows, int columns, int different, int expected)
    {
        var obj = new ComplexMatrixDecimal(columns, rows);
        if (different >= 0)
        {
            obj.Matrix.Values[different] = (GeneralComplexNumberDecimal)1M;
        }
        var zero = (GeneralComplexNumberDecimal)0M;
        new ComplexMatrixDecimal([[zero, zero, zero], [zero, zero, zero], [zero, zero, zero]]).CompareTo(obj).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Rows_Should_Get_Rows(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values, ComplexMatrixDecimal expected)
    {
        new ComplexMatrixDecimal(values).Rows
            .ShouldBe(expected.Rows);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Columns_Should_Get_Columns(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values, ComplexMatrixDecimal expected)
    {
        new ComplexMatrixDecimal(values).Columns
            .ShouldBe(expected.Columns);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void ArraySetter_Should_Set_ColumnValue(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values, ComplexMatrixDecimal expected)
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
    public void ToEnumerableOfEnumerable_Should_Get_Values(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> expected, ComplexMatrixDecimal matrix)
    {
        var columnCount = expected.Max(v => v.Count());
        matrix.ToEnumerableOfEnumerable()
            .SelectMany(v => v.Select(c => c))
            .ToArray()
            .ShouldBeEquivalentTo(expected
                .SelectMany(v => v.Select(c => c).Concat(new GeneralComplexNumberDecimal[columnCount - v.Count()]))
                .ToArray());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTransposeTheoryData))]
    public void Transpose_Should_Transpose_Matrix(ComplexMatrixDecimal matrix, ComplexMatrixDecimal expected)
    {
        matrix.Transpose()
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Trace_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal(columns, rows).Trace());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTraceTheoryData))]
    public void Trace_Should_Return_Trace_Of_Matrix(ComplexMatrixDecimal matrix, GeneralComplexNumberDecimal expected)
    {
        matrix.Trace().ShouldBe(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Determinant_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal(columns, rows).Determinant());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixDeterminantTheoryData))]
    public void Determinant_Should_Return_Determinant_Of_Matrix(ComplexMatrixDecimal matrix, GeneralComplexNumberDecimal expected)
    {
        matrix.Determinant().Round(26).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Rank_Should_Return_Rank_Of_Matrix(ComplexMatrixDecimal matrix, int expected)
    {
        matrix.Rank().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Nullity_Should_Return_Nullity_Of_Matrix(ComplexMatrixDecimal matrix, int expectedRank)
    {
        matrix.Nullity().ShouldBe(matrix.Columns - expectedRank);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixInverseTheoryData))]
    public void Inverse_Should_Return_Inverse_Of_Matrix(ComplexMatrixDecimal m, ComplexMatrixDecimal expected)
    {
        m.Inverse()
            .ShouldMatch(expected);
    }

    [Fact]
    public void Inverse_Should_Throw_Exception()
    {
        Should.Throw<ArgumentException>(() =>
            new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(-2M, 0M), new GeneralComplexNumberDecimal(3M, 0M)], [new GeneralComplexNumberDecimal(2M, 0M), new GeneralComplexNumberDecimal(-3M, 0M), new GeneralComplexNumberDecimal(5M, 0M)], [new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(1M, 0M), new GeneralComplexNumberDecimal(0M, 0M)]])
                .Inverse());
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Complex_Decimal_To_Matrix(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (m + value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrix_To_Complex_Decimal(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (value + m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Add_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal(columns1, rows1) + new ComplexMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrices(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2, ComplexMatrixDecimal expected)
    {
        (m1 + m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Copy_Should_Copy_Matrix(ComplexMatrixDecimal matrix)
    {
        (+matrix)
            .ShouldMatch(matrix);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Complex_Decimal_From_Matrix(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (m - value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_Matrix_From_Complex_Decimal(GeneralComplexNumberDecimal value, ComplexMatrixDecimal m, ComplexMatrixDecimal expected)
    {
        (value - m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Subtract_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal(columns1, rows1) - new ComplexMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Add_Matrices(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2, ComplexMatrixDecimal expected)
    {
        (m1 - m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Negate_Should_Negate_Matrix(ComplexMatrixDecimal matrix)
    {
        (-matrix)
            .ShouldMatch(new ComplexMatrixDecimal(new GeneralMatrix<GeneralComplexNumberDecimal>(matrix.Columns, [.. matrix.Matrix.Values.Select(v => -v)])));
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Complex_Decimal_From_Matrix(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (m * value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Matrix_From_Complex_Decimal(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (value * m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 3, 2)]
    [InlineData(3, 2, 3, 4)]
    public void Operator_Multiply_Should_Throw_ArgumentException_If_Rows_And_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDecimal(columns1, rows1) * new ComplexMatrixDecimal(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiple_Should_Multiple_Matrices(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2, ComplexMatrixDecimal expected)
    {
        (m1 * m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Complex_Decimal(ComplexMatrixDecimal m, GeneralComplexNumberDecimal value, ComplexMatrixDecimal expected)
    {
        (m / value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_Complex_Decimal_By_Matrix(GeneralComplexNumberDecimal value, ComplexMatrixDecimal m, ComplexMatrixDecimal expected)
    {
        (value / m)
            .ShouldMatch(expected);
    }

    #region Test Data

    public static readonly GeneralComplexNumberDecimal TestValue = new(10.1M, 1.01M);
    public static readonly GeneralComplexNumberDecimal TestValueNeg4 = new(-4.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValueNeg1 = new(-1.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue0 = new(0.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue1_1 = new(1M, 1M);
    public static readonly GeneralComplexNumberDecimal TestValue2 = new(2.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue2_2 = new(2M, 2M);
    public static readonly GeneralComplexNumberDecimal TestValue3 = new(3.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue3_3 = new(3.0M, 3.0M);
    public static readonly GeneralComplexNumberDecimal TestValue4 = new(4.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue4_4 = new(4.0M, 4.0M);
    public static readonly GeneralComplexNumberDecimal TestValue5 = new(5.0M, 0.0M);
    public static readonly GeneralComplexNumberDecimal TestValue5_5 = new(5.0M, 5.0M);
    public static readonly GeneralComplexNumberDecimal TestValue6_6 = new(6.0M, 6.0M);
    public static readonly GeneralComplexNumberDecimal TestValue7_7 = new(7.0M, 7.0M);
    public static readonly GeneralComplexNumberDecimal TestValue8_8 = new(8.0M, 8.0M);
    public static readonly GeneralComplexNumberDecimal TestValue20_2 = new(20.0M, 2M);

    private static readonly IFormatProvider FormatProvider = new CultureInfo("it-IT");

    public static readonly TheoryData<ComplexMatrixDecimal> TestGeneralMatrixTheoryData =
        new(new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]));

    public static readonly TheoryData<ComplexMatrixDecimal, string> TestToStringTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "(((1+1i)|(10.1+1.01i))(-1.0|2.0))" },
            { new ComplexMatrixDecimal([[TestValue1_1], [TestValueNeg1]]), "(((1+1i))(-1.0))" },
            { new ComplexMatrixDecimal([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "((0.0|-4.0)(2.0|2.0))" }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, string?, string> TestToStringFormatTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "#0.00", "(((1.00+1.00i)|(10.10+1.01i))(-1.00|2.00))" },
            { new ComplexMatrixDecimal([[TestValue1_1], [TestValueNeg1]]), "#0.00", "(((1.00+1.00i))(-1.00))" },
            { new ComplexMatrixDecimal([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "#0.00", "((0.00|-4.00)(2.00|2.00))" }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, IFormatProvider?, string> TestToStringProviderTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), FormatProvider, "(((1+1i)|(10,1+1,01i))(-1,0|2,0))" },
            { new ComplexMatrixDecimal([[TestValue1_1], [TestValueNeg1]]), FormatProvider, "(((1+1i))(-1,0))" },
            { new ComplexMatrixDecimal([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), FormatProvider, "((0,0|-4,0)(2,0|2,0))" }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, string?, IFormatProvider?, string> TestToStringFormatProviderTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "#0.00", FormatProvider, "(((1,00+1,00i)|(10,10+1,01i))(-1,00|2,00))" },
            { new ComplexMatrixDecimal([[TestValue1_1], [TestValueNeg1]]), "#0.00", FormatProvider, "(((1,00+1,00i))(-1,00))" },
            { new ComplexMatrixDecimal([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "#0.00", FormatProvider, "((0,00|-4,00)(2,00|2,00))" }
        };

    public static readonly TheoryData<string, ComplexMatrixDecimal> ParseTestData = new()
    {
        { "()", new ComplexMatrixDecimal([]) },
        { "2", new ComplexMatrixDecimal([[TestValue2]]) },
        { "(2+2i)", new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "-1.0", new ComplexMatrixDecimal([[TestValueNeg1]]) },
        { "(((2+2i)))", new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", new ComplexMatrixDecimal([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) }
    };

    public static readonly TheoryData<string, IFormatProvider?, ComplexMatrixDecimal> ParseProviderTestData = new()
    {
        { "()", FormatProvider, new ComplexMatrixDecimal([]) },
        { "2", FormatProvider, new ComplexMatrixDecimal([[TestValue2]]) },
        { "(2+2i)", FormatProvider, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "-1,0", FormatProvider, new ComplexMatrixDecimal([[TestValueNeg1]]) },
        { "(((2+2i)))", FormatProvider, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", FormatProvider, new ComplexMatrixDecimal([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", FormatProvider, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", FormatProvider, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) }
    };

    public static readonly TheoryData<string, bool, ComplexMatrixDecimal> TryParseTestData = new()
    {
        { "()", true, new ComplexMatrixDecimal([]) },
        { "( )", true, new ComplexMatrixDecimal([]) },
        { "2", true, new ComplexMatrixDecimal([[TestValue2]]) },
        { "(2+2i)", true, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "-1.0", true, new ComplexMatrixDecimal([[TestValueNeg1]]) },
        { "(((2+2i)))", true, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", true, new ComplexMatrixDecimal([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", true, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", true, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "", false, new ComplexMatrixDecimal([]) },
        { "(2", false, new ComplexMatrixDecimal([]) },
        { "2)", false, new ComplexMatrixDecimal([]) },
        { "(2)", false, new ComplexMatrixDecimal([]) },
        { "(()((2+2i)))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))( ))", false, new ComplexMatrixDecimal([]) },
        { "((1+1i))((2+2i)))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)((2+2i)))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))(2+2i)))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))((2+2i))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)|)((2+2i)|(-1-1i)))", false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)|(-2-2i))(|(-1-1i)))", false, new ComplexMatrixDecimal([]) },
        { "((|(-2-2i))(|(-1-1i)))", false, new ComplexMatrixDecimal([]) }
    };

    public static readonly TheoryData<string, IFormatProvider?, bool, ComplexMatrixDecimal> TryParseProviderTestData = new()
    {
        { "()", FormatProvider, true, new ComplexMatrixDecimal([]) },
        { "( )", FormatProvider, true, new ComplexMatrixDecimal([]) },
        { "2", FormatProvider, true, new ComplexMatrixDecimal([[TestValue2]]) },
        { "(2+2i)", FormatProvider, true, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "-1,0", FormatProvider, true, new ComplexMatrixDecimal([[TestValueNeg1]]) },
        { "(2+2i)", FormatProvider, true, new ComplexMatrixDecimal([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", FormatProvider, true, new ComplexMatrixDecimal([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", FormatProvider, true, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", FormatProvider, true, new ComplexMatrixDecimal([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(2", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "2)", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(2)", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(()((2+2i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))( ))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "((1+1i))((2+2i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)((2+2i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))(2+2i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i))((2+2i))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)|)((2+2i)|(-1-1i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "(((1+1i)|(-2-2i))(|(-1-1i)))", FormatProvider, false, new ComplexMatrixDecimal([]) },
        { "((|(-2-2i))(|(-1-1i)))", FormatProvider, false, new ComplexMatrixDecimal([]) }
    };

    public static readonly TheoryData<ComplexMatrixDecimal, object?, bool> TestGeneralMatrixEqualityTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), null, false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1, false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[(GeneralComplexNumberDouble)TestValue1_1, (GeneralComplexNumberDouble)TestValue2_2], [(GeneralComplexNumberDouble)TestValue3_3, (GeneralComplexNumberDouble)TestValue4_4]]), false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue], [TestValue3_3, TestValue4_4, TestValue]]), false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue5_5]]), false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), true }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal, bool> TestComplexMatrixDecimalEqualityTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue], [TestValue3_3, TestValue4_4, TestValue]]), false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue5_5]]), false },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), true }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<GeneralComplexNumberDecimal>>, ComplexMatrixDecimal> TestMatrixTheoryData =
        new()
        {
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]], new ComplexMatrixDecimal(new GeneralMatrix<GeneralComplexNumberDecimal>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue5_5, TestValue6_6])) },
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4], [TestValue5_5, TestValue6_6]], new ComplexMatrixDecimal(new GeneralMatrix<GeneralComplexNumberDecimal>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue0, TestValue0, TestValue5_5, TestValue6_6, TestValue0])) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal> TestGeneralMatrixTransposeTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal> TestGeneralMatrixTraceTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1 + TestValue4_4 }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal> TestGeneralMatrixDeterminantTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralComplexNumberDecimal(0M, -4M) },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), new GeneralComplexNumberDecimal(-16.00000000000000000000000002M, 16.00000000000000000000000002M) },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), new GeneralComplexNumberDecimal(0.0M, 0.0M) },
            { new ComplexMatrixDecimal([[TestValue0, TestValue0, TestValue1_1], [TestValue3_3, TestValue2_2, TestValue1_1], [TestValue1_1, TestValue2_2, TestValue3_3]]), new GeneralComplexNumberDecimal(-8M, +8M) },
            { new ComplexMatrixDecimal([[TestValue2, TestValue3, TestValue4, TestValue0], [TestValue5, TestValue2, TestValue4, TestValue3], [TestValue2, TestValue2, TestValue3, TestValue4], [TestValue5, TestValue3, TestValue2, TestValue4]]), new GeneralComplexNumberDecimal(-119.00000000000000000000000001M, 0.0M) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, int> TestGeneralMatrixRankTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), 2 },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 3 },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 1 },
            { new ComplexMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue0], [-TestValue2_2, -TestValue3_3, TestValue0]]), 1 },
            { new ComplexMatrixDecimal([[TestValue2_2, -TestValue2_2], [TestValue3_3, -TestValue3_3], [TestValue0, TestValue0]]), 1 },
            { new ComplexMatrixDecimal([[TestValue2, TestValue4_4, TestValue0], [TestValue4, TestValue8_8, TestValue3]]), 2 },
            { new ComplexMatrixDecimal([[TestValue2, TestValue4], [TestValue4_4, TestValue8_8], [TestValue0, TestValue3]]), 2 }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal> TestGeneralMatrixInverseTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(-1M, 1M), new GeneralComplexNumberDecimal(0.5M, -0.5M)], [new GeneralComplexNumberDecimal(0.75M, -0.75M), new GeneralComplexNumberDecimal(-0.25M, 0.25M)]]) },
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), new ComplexMatrixDecimal([[new GeneralComplexNumberDecimal(-0.1875000000000000000000000006M, 0.1875000000000000000000000006M), new GeneralComplexNumberDecimal(0.1249999999999999999999999998M, -0.1249999999999999999999999998M), new GeneralComplexNumberDecimal(0.0625000000000000000000000004M, -0.0625000000000000000000000004M)], [new GeneralComplexNumberDecimal(-0.8749999999999999999999999992M, 0.8749999999999999999999999992M), new GeneralComplexNumberDecimal(-0.2499999999999999999999999998M, 0.2499999999999999999999999998M), new GeneralComplexNumberDecimal(0.6249999999999999999999999994M, -0.6249999999999999999999999994M)], [new GeneralComplexNumberDecimal(0.8124999999999999999999999995M, -0.8124999999999999999999999995M), new GeneralComplexNumberDecimal(0.1249999999999999999999999998M, -0.1249999999999999999999999998M), new GeneralComplexNumberDecimal(-0.4374999999999999999999999996M, 0.4374999999999999999999999996M)]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal> TestScalarAdditionTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1 + TestValue20_2, TestValue2_2 + TestValue20_2, TestValue3_3 + TestValue20_2], [TestValue4_4 + TestValue20_2, TestValue5_5 + TestValue20_2, TestValue6_6 + TestValue20_2]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal> TestAdditionTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new ComplexMatrixDecimal([[TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue3_3, TestValue3_3 + TestValue4_4], [TestValue4_4 + TestValue5_5, TestValue5_5 + TestValue6_6, TestValue6_6 + TestValue7_7]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal> TestScalarSubtractionTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1 - TestValue20_2, TestValue2_2 - TestValue20_2, TestValue3_3 - TestValue20_2], [TestValue4_4 - TestValue20_2, TestValue5_5 - TestValue20_2, TestValue6_6 - TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralComplexNumberDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal> TestSubtractionScalarTheoryData =
        new()
        {
            { TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue20_2 - TestValue1_1, TestValue20_2 - TestValue2_2, TestValue20_2 - TestValue3_3], [TestValue20_2 - TestValue4_4, TestValue20_2 - TestValue5_5, TestValue20_2 - TestValue6_6]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal> TestSubtractionTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new ComplexMatrixDecimal([[TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue3_3, TestValue3_3 - TestValue4_4], [TestValue4_4 - TestValue5_5, TestValue5_5 - TestValue6_6, TestValue6_6 - TestValue7_7]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal> TestScalarMultiplicationTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1 * TestValue20_2, TestValue2_2 * TestValue20_2, TestValue3_3 * TestValue20_2], [TestValue4_4 * TestValue20_2, TestValue5_5 * TestValue20_2, TestValue6_6 * TestValue20_2]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal> TestMultiplicationTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[TestValue5_5, TestValue6_6], [TestValue7_7, TestValue8_8]]), new ComplexMatrixDecimal([[TestValue1_1 * TestValue5_5 + TestValue2_2 * TestValue7_7, TestValue1_1 * TestValue6_6 + TestValue2_2 * TestValue8_8], [TestValue3_3 * TestValue5_5 + TestValue4_4 * TestValue7_7, TestValue3_3 * TestValue6_6 + TestValue4_4 * TestValue8_8]]) },
            { new ComplexMatrixDecimal([[TestValueNeg1, TestValue2, TestValue3], [TestValue4, TestValue0, TestValue5]]), new ComplexMatrixDecimal([[TestValue5, TestValueNeg1], [TestValueNeg4, TestValue0], [TestValue2, TestValue3]]), new ComplexMatrixDecimal([[TestValueNeg1 * TestValue5 + TestValue2 * TestValueNeg4 + TestValue3 * TestValue2, TestValueNeg1 * TestValueNeg1 + TestValue2 * TestValue0 + TestValue3 * TestValue3], [TestValue4 * TestValue5 + TestValue0 * TestValueNeg4 + TestValue5 * TestValue2, TestValue4 * TestValueNeg1 + TestValue0 * TestValue0 + TestValue5 * TestValue3]]) }
        };

    public static readonly TheoryData<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal> TestScalarDivisionTheoryData =
        new()
        {
            { new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1 / TestValue20_2, TestValue2_2 / TestValue20_2, TestValue3_3 / TestValue20_2], [TestValue4_4 / TestValue20_2, TestValue5_5 / TestValue20_2, TestValue6_6 / TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralComplexNumberDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal> TestDivisionScalarTheoryData =
        new()
        {
            { TestValue20_2, new ComplexMatrixDecimal([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDecimal([[TestValue20_2 / TestValue1_1, TestValue20_2 / TestValue2_2, TestValue20_2 / TestValue3_3], [TestValue20_2 / TestValue4_4, TestValue20_2 / TestValue5_5, TestValue20_2 / TestValue6_6]]) }
        };

    #endregion
}
