using Shouldly;
using System.Globalization;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.UnitTests.TestHelpers;

namespace Wyrm.Math.UnitTests.Matrix;

public class ComplexMatrixDoubleTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void Constructor_Should_Throw_ArgumentOutOfRangeException_If_Parameter_Negative(int columns, int rows)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new ComplexMatrixDouble(columns, rows));
    }

    [Fact]
    public void Constructor_Should_Create_Empty_Matrix()
    {
        Should.NotThrow(() => new ComplexMatrixDouble(0, 0))
            .ShouldMatch(new ComplexMatrixDouble(new GeneralMatrix<GeneralComplexNumberDouble>(0, [])));
    }

    [Theory]
    [InlineData(3, 2)]
    public void Constructor_Should_Create_Initialised_Matrix(int columns, int rows)
    {
        new ComplexMatrixDouble(columns, rows)
            .ShouldMatch(new ComplexMatrixDouble(new GeneralMatrix<GeneralComplexNumberDouble>(columns, new GeneralComplexNumberDouble[columns * rows])));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Populated_Matrix(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values, ComplexMatrixDouble expected)
    {
        new ComplexMatrixDouble(values)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Copy_Matrix(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values, ComplexMatrixDouble expected)
    {
        new ComplexMatrixDouble(new ComplexMatrixDouble(values))
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringTheoryData))]
    public void ToString_Should_Return_String_Representation(ComplexMatrixDouble matrix, string expected)
    {
        matrix.ToString().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringFormatTheoryData))]
    public void ToString_Format_Should_Return_String_Representation(ComplexMatrixDouble matrix, string? format, string expected)
    {
        matrix.ToString(format).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringProviderTheoryData))]
    public void ToString_Provider_Should_Return_String_Representation(ComplexMatrixDouble matrix, IFormatProvider? provider, string expected)
    {
        matrix.ToString(provider).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestToStringFormatProviderTheoryData))]
    public void ToString_Format_Provider_Should_Return_String_Representation(ComplexMatrixDouble matrix, string? format, IFormatProvider? provider, string expected)
    {
        matrix.ToString(format, provider).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseTestData))]
    public void Parse_Should_Return_Expected(string matrix, ComplexMatrixDouble expected)
    {
        var result = ComplexMatrixDouble.Parse(matrix);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ParseProviderTestData))]
    public void Parse_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, ComplexMatrixDouble expected)
    {
        var result = ComplexMatrixDouble.Parse(matrix, provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TryParseTestData))]
    public void TryParse_Should_Return_Expected(string matrix, bool expected, ComplexMatrixDouble expectedNumber)
    {
        var result = ComplexMatrixDouble.TryParse(matrix, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(TryParseProviderTestData))]
    public void TryParse_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, bool expected, ComplexMatrixDouble expectedNumber)
    {
        var result = ComplexMatrixDouble.TryParse(matrix, provider, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Theory]
    [MemberData(nameof(ParseProviderTestData))]
    public void Parse_Span_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, ComplexMatrixDouble expected)
    {
        var result = ComplexMatrixDouble.Parse(matrix.AsSpan(), provider);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TryParseProviderTestData))]
    public void TryParse_Span_Provider_Should_Return_Expected(string matrix, IFormatProvider? provider, bool expected, ComplexMatrixDouble expectedNumber)
    {
        var result = ComplexMatrixDouble.TryParse(matrix.AsSpan(), provider, out var actual);
        result.ShouldBe(expected);
        if (result) actual.ShouldBe(expectedNumber);
    }

    [Fact]
    public void ToString_Should_Parse()
    {
        var matrix = new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2], [-TestValue2, TestValue]]);
        var str = matrix.ToString();
        var result = ComplexMatrixDouble.Parse(str);
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
    public void Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(ComplexMatrixDouble matrix1, object? obj, bool expected)
    {
        matrix1.Equals(obj).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestComplexMatrixDoubleEqualityTheoryData))]
    public void Equals_Should_Return_True_When_Complex_Complex_Double_Matrices_Have_The_Same_Values(ComplexMatrixDouble matrix1, ComplexMatrixDouble matrix2, bool expected)
    {
        matrix1.Equals(matrix2).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Equals_Should_Return_True_When_Matrices_Have_The_Same_Values(ComplexMatrixDouble matrix1, object? obj, bool expected)
    {
        (matrix1 == (obj as ComplexMatrixDouble?)).ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixEqualityTheoryData))]
    public void Operator_Not_Equals_Should_Return_False_When_Matrices_Have_The_Same_Values(ComplexMatrixDouble matrix1, object? obj, bool expected)
    {
        (matrix1 != (obj as ComplexMatrixDouble?)).ShouldBe(!expected);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CompareTo_Should_Throw_ArgumentException_If_Wrong_Type_Or_Null(bool isNull)
    {
        var obj = isNull ? null : new object();
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble().CompareTo(obj));
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
        var obj = new ComplexMatrixDouble(columns, rows);
        if (different >= 0)
        {
            obj.Matrix.Values[different] = (GeneralComplexNumberDouble)1D;
        }
        var zero = (GeneralComplexNumberDouble)0D;
        new ComplexMatrixDouble([[zero, zero, zero], [zero, zero, zero], [zero, zero, zero]]).CompareTo(obj).ShouldBe(expected);
    }

    [Fact]
    public void Cast_Should_Cast_To_GeneralMatrixDouble()
    {
        var matrix = new ComplexMatrixDecimal([[TestValue2, TestValue3], [TestValue4, TestValue5], [TestValue3, TestValue4]]);
        var result = (GeneralMatrixDouble)matrix;
        result.ShouldMatch(new GeneralMatrixDouble([[(double)TestValue2, (double)TestValue3], [(double)TestValue4, (double)TestValue5], [(double)TestValue3, (double)TestValue4]]));
    }

    [Fact]
    public void Cast_Should_Cast_From_GeneralMatrixDouble()
    {
        var matrix = new GeneralMatrixDouble([[(double)TestValue2, (double)TestValue3], [(double)TestValue4, (double)TestValue5], [(double)TestValue3, (double)TestValue4]]);
        var result = (ComplexMatrixDecimal)matrix;
        result.ShouldMatch(new ComplexMatrixDecimal([[TestValue2, TestValue3], [TestValue4, TestValue5], [TestValue3, TestValue4]]));
    }

    [Fact]
    public void Cast_Should_Cast_To_GeneralMatrixDecimal()
    {
        var matrix = new ComplexMatrixDecimal([[TestValue2, TestValue3], [TestValue4, TestValue5], [TestValue3, TestValue4]]);
        var result = (GeneralMatrixDecimal)matrix;
        result.ShouldMatch(new GeneralMatrixDecimal([[(decimal)TestValue2, (decimal)TestValue3], [(decimal)TestValue4, (decimal)TestValue5], [(decimal)TestValue3, (decimal)TestValue4]]));
    }

    [Fact]
    public void Cast_Should_Cast_From_GeneralMatrixDecimal()
    {
        var matrix = new GeneralMatrixDecimal([[(decimal)TestValue2, (decimal)TestValue3], [(decimal)TestValue4, (decimal)TestValue5], [(decimal)TestValue3, (decimal)TestValue4]]);
        var result = (ComplexMatrixDecimal)matrix;
        result.ShouldMatch(new ComplexMatrixDecimal([[TestValue2, TestValue3], [TestValue4, TestValue5], [TestValue3, TestValue4]]));
    }

    [Fact]
    public void Cast_Should_Cast_To_ComplexMatrixDecimal()
    {
        var matrix = new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6]]);
        var result = (ComplexMatrixDecimal)matrix;
        result.ShouldMatch(new ComplexMatrixDecimal([[(GeneralComplexNumberDecimal)TestValue1_1, (GeneralComplexNumberDecimal)TestValue2_2], [(GeneralComplexNumberDecimal)TestValue3_3, (GeneralComplexNumberDecimal)TestValue4_4], [(GeneralComplexNumberDecimal)TestValue5_5, (GeneralComplexNumberDecimal)TestValue6_6]]));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Rows_Should_Get_Rows(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values, ComplexMatrixDouble expected)
    {
        new ComplexMatrixDouble(values).Rows
            .ShouldBe(expected.Rows);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Columns_Should_Get_Columns(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values, ComplexMatrixDouble expected)
    {
        new ComplexMatrixDouble(values).Columns
            .ShouldBe(expected.Columns);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void ArraySetter_Should_Set_ColumnValue(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values, ComplexMatrixDouble expected)
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
    public void ToEnumerableOfEnumerable_Should_Get_Values(IEnumerable<IEnumerable<GeneralComplexNumberDouble>> expected, ComplexMatrixDouble matrix)
    {
        var columnCount = expected.Max(v => v.Count());
        matrix.ToEnumerableOfEnumerable()
            .SelectMany(v => v.Select(c => c))
            .ToArray()
            .ShouldBeEquivalentTo(expected
                .SelectMany(v => v.Select(c => c).Concat(new GeneralComplexNumberDouble[columnCount - v.Count()]))
                .ToArray());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTransposeTheoryData))]
    public void Transpose_Should_Transpose_Matrix(ComplexMatrixDouble matrix, ComplexMatrixDouble expected)
    {
        matrix.Transpose()
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Trace_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble(columns, rows).Trace());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTraceTheoryData))]
    public void Trace_Should_Return_Trace_Of_Matrix(ComplexMatrixDouble matrix, GeneralComplexNumberDouble expected)
    {
        matrix.Trace().ShouldBe(expected);
    }

    [Theory]
    [InlineData(2, 3)]
    public void Determinant_Should_Throw_ArgumentException_If_Matrix_Not_Square(int columns, int rows)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble(columns, rows).Determinant());
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixDeterminantTheoryData))]
    public void Determinant_Should_Return_Determinant_Of_Matrix(ComplexMatrixDouble matrix, GeneralComplexNumberDouble expected)
    {
        matrix.Determinant().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Rank_Should_Return_Rank_Of_Matrix(ComplexMatrixDouble matrix, int expected)
    {
        matrix.Rank().ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixRankTheoryData))]
    public void Nullity_Should_Return_Nullity_Of_Matrix(ComplexMatrixDouble matrix, int expectedRank)
    {
        matrix.Nullity().ShouldBe(matrix.Columns - expectedRank);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixInverseTheoryData))]
    public void Inverse_Should_Return_Inverse_Of_Matrix(ComplexMatrixDouble m, ComplexMatrixDouble expected)
    {
        m.Inverse()
            .ShouldMatch(expected);
    }

    [Fact]
    public void Inverse_Should_Throw_Exception()
    {
        Should.Throw<ArgumentException>(() =>
            new ComplexMatrixDouble([[new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(-2D, 0D), new GeneralComplexNumberDouble(3D, 0D)], [new GeneralComplexNumberDouble(2D, 0D), new GeneralComplexNumberDouble(-3D, 0D), new GeneralComplexNumberDouble(5D, 0D)], [new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(1D, 0D), new GeneralComplexNumberDouble(0D, 0D)]])
                .Inverse());
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Complex_Double_To_Matrix(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (m + value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrix_To_Complex_Double(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (value + m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Add_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble(columns1, rows1) + new ComplexMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrices(ComplexMatrixDouble m1, ComplexMatrixDouble m2, ComplexMatrixDouble expected)
    {
        (m1 + m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Copy_Should_Copy_Matrix(ComplexMatrixDouble matrix)
    {
        (+matrix)
            .ShouldMatch(matrix);
    }

    [Theory]
    [MemberData(nameof(TestScalarSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Complex_Double_From_Matrix(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (m - value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionScalarTheoryData))]
    public void Operator_Subtract_Should_Subtract_Matrix_From_Complex_Double(GeneralComplexNumberDouble value, ComplexMatrixDouble m, ComplexMatrixDouble expected)
    {
        (value - m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 2, 2)]
    [InlineData(3, 2, 3, 3)]
    public void Operator_Subtract_Should_Throw_ArgumentException_If_Rows_Or_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble(columns1, rows1) - new ComplexMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestSubtractionTheoryData))]
    public void Operator_Subtract_Should_Add_Matrices(ComplexMatrixDouble m1, ComplexMatrixDouble m2, ComplexMatrixDouble expected)
    {
        (m1 - m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void Operator_Negate_Should_Negate_Matrix(ComplexMatrixDouble matrix)
    {
        (-matrix)
            .ShouldMatch(new ComplexMatrixDouble(new GeneralMatrix<GeneralComplexNumberDouble>(matrix.Columns, [.. matrix.Matrix.Values.Select(v => -v)])));
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Complex_Double_From_Matrix(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (m * value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Matrix_From_Complex_Double(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (value * m)
            .ShouldMatch(expected);
    }

    [Theory]
    [InlineData(3, 2, 3, 2)]
    [InlineData(3, 2, 3, 4)]
    public void Operator_Multiply_Should_Throw_ArgumentException_If_Rows_And_Columns_Mismatch(int columns1, int rows1, int columns2, int rows2)
    {
        Should.Throw<ArgumentException>(() => new ComplexMatrixDouble(columns1, rows1) * new ComplexMatrixDouble(columns2, rows2));
    }

    [Theory]
    [MemberData(nameof(TestMultiplicationTheoryData))]
    public void Operator_Multiple_Should_Multiple_Matrices(ComplexMatrixDouble m1, ComplexMatrixDouble m2, ComplexMatrixDouble expected)
    {
        (m1 * m2)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestScalarDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Complex_Double(ComplexMatrixDouble m, GeneralComplexNumberDouble value, ComplexMatrixDouble expected)
    {
        (m / value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionScalarTheoryData))]
    public void Operator_Divide_Should_Divide_Complex_Double_By_Matrix(GeneralComplexNumberDouble value, ComplexMatrixDouble m, ComplexMatrixDouble expected)
    {
        (value / m)
            .ShouldMatch(expected);
    }

    #region Test Data

    public static readonly GeneralComplexNumberDouble TestValue = new(10.1, 1.01);
    public static readonly GeneralComplexNumberDouble TestValueNeg4 = new(-4.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValueNeg1 = new(-1.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue0 = new(0.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue1_1 = new(1D, 1D);
    public static readonly GeneralComplexNumberDouble TestValue2 = new(2.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue2_2 = new(2D, 2D);
    public static readonly GeneralComplexNumberDouble TestValue3 = new(3.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue3_3 = new(3.0, 3.0);
    public static readonly GeneralComplexNumberDouble TestValue4 = new(4.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue4_4 = new(4.0, 4.0);
    public static readonly GeneralComplexNumberDouble TestValue5 = new(5.0, 0.0);
    public static readonly GeneralComplexNumberDouble TestValue5_5 = new(5.0, 5.0);
    public static readonly GeneralComplexNumberDouble TestValue6_6 = new(6.0, 6.0);
    public static readonly GeneralComplexNumberDouble TestValue7_7 = new(7.0, 7.0);
    public static readonly GeneralComplexNumberDouble TestValue8_8 = new(8.0, 8.0);
    public static readonly GeneralComplexNumberDouble TestValue20_2 = new(20.0, 2D);

    private static readonly IFormatProvider FormatProvider = new CultureInfo("it-IT");

    public static readonly TheoryData<ComplexMatrixDouble> TestGeneralMatrixTheoryData =
        new(new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]));

    public static readonly TheoryData<ComplexMatrixDouble, string> TestToStringTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "(((1+1i)|(10.1+1.01i))(-1|2))" },
            { new ComplexMatrixDouble([[TestValue1_1], [TestValueNeg1]]), "(((1+1i))(-1))" },
            { new ComplexMatrixDouble([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "((0|-4)(2|2))" }
        };

    public static readonly TheoryData<ComplexMatrixDouble, string?, string> TestToStringFormatTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "#0.00", "(((1.00+1.00i)|(10.10+1.01i))(-1.00|2.00))" },
            { new ComplexMatrixDouble([[TestValue1_1], [TestValueNeg1]]), "#0.00", "(((1.00+1.00i))(-1.00))" },
            { new ComplexMatrixDouble([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "#0.00", "((0.00|-4.00)(2.00|2.00))" }
        };

    public static readonly TheoryData<ComplexMatrixDouble, IFormatProvider?, string> TestToStringProviderTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), FormatProvider, "(((1+1i)|(10,1+1,01i))(-1|2))" },
            { new ComplexMatrixDouble([[TestValue1_1], [TestValueNeg1]]), FormatProvider, "(((1+1i))(-1))" },
            { new ComplexMatrixDouble([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), FormatProvider, "((0|-4)(2|2))" }
        };

    public static readonly TheoryData<ComplexMatrixDouble, string?, IFormatProvider?, string> TestToStringFormatProviderTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue], [TestValueNeg1, TestValue2]]), "#0.00", FormatProvider, "(((1,00+1,00i)|(10,10+1,01i))(-1,00|2,00))" },
            { new ComplexMatrixDouble([[TestValue1_1], [TestValueNeg1]]), "#0.00", FormatProvider, "(((1,00+1,00i))(-1,00))" },
            { new ComplexMatrixDouble([[TestValue0, TestValueNeg4], [TestValue2, TestValue2]]), "#0.00", FormatProvider, "((0,00|-4,00)(2,00|2,00))" }
        };

    public static readonly TheoryData<string, ComplexMatrixDouble> ParseTestData = new()
    {
        { "()", new ComplexMatrixDouble([]) },
        { "2", new ComplexMatrixDouble([[TestValue2]]) },
        { "(2+2i)", new ComplexMatrixDouble([[TestValue2_2]]) },
        { "-1.0", new ComplexMatrixDouble([[TestValueNeg1]]) },
        { "(((2+2i)))", new ComplexMatrixDouble([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", new ComplexMatrixDouble([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) }
    };

    public static readonly TheoryData<string, IFormatProvider?, ComplexMatrixDouble> ParseProviderTestData = new()
    {
        { "()", FormatProvider, new ComplexMatrixDouble([]) },
        { "2", FormatProvider, new ComplexMatrixDouble([[TestValue2]]) },
        { "(2+2i)", FormatProvider, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "-1,0", FormatProvider, new ComplexMatrixDouble([[TestValueNeg1]]) },
        { "(((2+2i)))", FormatProvider, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", FormatProvider, new ComplexMatrixDouble([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", FormatProvider, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", FormatProvider, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) }
    };

    public static readonly TheoryData<string, bool, ComplexMatrixDouble> TryParseTestData = new()
    {
        { "()", true, new ComplexMatrixDouble([]) },
        { "( )", true, new ComplexMatrixDouble([]) },
        { "2", true, new ComplexMatrixDouble([[TestValue2]]) },
        { "(2+2i)", true, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "-1.0", true, new ComplexMatrixDouble([[TestValueNeg1]]) },
        { "(((2+2i)))", true, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", true, new ComplexMatrixDouble([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", true, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", true, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "", false, new ComplexMatrixDouble([]) },
        { "(2", false, new ComplexMatrixDouble([]) },
        { "2)", false, new ComplexMatrixDouble([]) },
        { "(2)", false, new ComplexMatrixDouble([]) },
        { "(()((2+2i)))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i))( ))", false, new ComplexMatrixDouble([]) },
        { "((1+1i))((2+2i)))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i)((2+2i)))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i))(2+2i)))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i))((2+2i))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i)|)((2+2i)|(-1-1i)))", false, new ComplexMatrixDouble([]) },
        { "(((1+1i)|(-2-2i))(|(-1-1i)))", false, new ComplexMatrixDouble([]) },
        { "((|(-2-2i))(|(-1-1i)))", false, new ComplexMatrixDouble([]) }
    };

    public static readonly TheoryData<string, IFormatProvider?, bool, ComplexMatrixDouble> TryParseProviderTestData = new()
    {
        { "()", FormatProvider, true, new ComplexMatrixDouble([]) },
        { "( )", FormatProvider, true, new ComplexMatrixDouble([]) },
        { "2", FormatProvider, true, new ComplexMatrixDouble([[TestValue2]]) },
        { "(2+2i)", FormatProvider, true, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "-1,0", FormatProvider, true, new ComplexMatrixDouble([[TestValueNeg1]]) },
        { "(2+2i)", FormatProvider, true, new ComplexMatrixDouble([[TestValue2_2]]) },
        { "(((1+1i))((2+2i)))", FormatProvider, true, new ComplexMatrixDouble([[TestValue1_1],[TestValue2_2]]) },
        { "(((1+1i)|(-2-2i))((2+2i)|(-1-1i)))", FormatProvider, true, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "( ( (1+1i) | (-2-2i) ) ( (2+2i) | (-1-1i) ) )", FormatProvider, true, new ComplexMatrixDouble([[TestValue1_1, -TestValue2_2],[TestValue2_2, -TestValue1_1]]) },
        { "", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(2", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "2)", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(2)", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(()((2+2i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i))( ))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "((1+1i))((2+2i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i)((2+2i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i))(2+2i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i))((2+2i))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i)|)((2+2i)|(-1-1i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "(((1+1i)|(-2-2i))(|(-1-1i)))", FormatProvider, false, new ComplexMatrixDouble([]) },
        { "((|(-2-2i))(|(-1-1i)))", FormatProvider, false, new ComplexMatrixDouble([]) }
    };

    public static readonly TheoryData<ComplexMatrixDouble, object?, bool> TestGeneralMatrixEqualityTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), null, false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1, false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDecimal([[(GeneralComplexNumberDecimal)TestValue1_1, (GeneralComplexNumberDecimal)TestValue2_2], [(GeneralComplexNumberDecimal)TestValue3_3, (GeneralComplexNumberDecimal)TestValue4_4]]), false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue], [TestValue3_3, TestValue4_4, TestValue]]), false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue5_5]]), false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), true }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble, bool> TestComplexMatrixDoubleEqualityTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue], [TestValue3_3, TestValue4_4, TestValue]]), false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue5_5]]), false },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), true }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<GeneralComplexNumberDouble>>, ComplexMatrixDouble> TestMatrixTheoryData =
        new()
        {
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]], new ComplexMatrixDouble(new GeneralMatrix<GeneralComplexNumberDouble>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue5_5, TestValue6_6])) },
            { [[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4], [TestValue5_5, TestValue6_6]], new ComplexMatrixDouble(new GeneralMatrix<GeneralComplexNumberDouble>(3, [TestValue1_1, TestValue2_2, TestValue3_3, TestValue4_4, TestValue0, TestValue0, TestValue5_5, TestValue6_6, TestValue0])) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble> TestGeneralMatrixTransposeTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]) },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue4_4], [TestValue2_2, TestValue5_5], [TestValue3_3, TestValue6_6]]), new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble> TestGeneralMatrixTraceTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), TestValue1_1 + TestValue4_4 }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble> TestGeneralMatrixDeterminantTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new GeneralComplexNumberDouble(0D, -4D) },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), new GeneralComplexNumberDouble(-16.000000000000007, 16.000000000000007) },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), new GeneralComplexNumberDouble(0.0, 0.0) },
            { new ComplexMatrixDouble([[TestValue0, TestValue0, TestValue1_1], [TestValue3_3, TestValue2_2, TestValue1_1], [TestValue1_1, TestValue2_2, TestValue3_3]]), new GeneralComplexNumberDouble(-8D, +8D) },
            { new ComplexMatrixDouble([[TestValue2, TestValue3, TestValue4, TestValue0], [TestValue5, TestValue2, TestValue4, TestValue3], [TestValue2, TestValue2, TestValue3, TestValue4], [TestValue5, TestValue3, TestValue2, TestValue4]]), new GeneralComplexNumberDouble(-119D, 0D) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, int> TestGeneralMatrixRankTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), 2 },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), 3 },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1], [TestValue1_1, TestValue1_1, TestValue1_1]]), 1 },
            { new ComplexMatrixDouble([[TestValue2_2, TestValue3_3, TestValue0], [-TestValue2_2, -TestValue3_3, TestValue0]]), 1 },
            { new ComplexMatrixDouble([[TestValue2_2, -TestValue2_2], [TestValue3_3, -TestValue3_3], [TestValue0, TestValue0]]), 1 },
            { new ComplexMatrixDouble([[TestValue2, TestValue4_4, TestValue0], [TestValue4, TestValue8_8, TestValue3]]), 2 },
            { new ComplexMatrixDouble([[TestValue2, TestValue4], [TestValue4_4, TestValue8_8], [TestValue0, TestValue3]]), 2 }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble> TestGeneralMatrixInverseTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[new GeneralComplexNumberDouble(-1D, 1D), new GeneralComplexNumberDouble(0.5, -0.5)], [new GeneralComplexNumberDouble(0.75, -0.75), new GeneralComplexNumberDouble(-0.25D, 0.25D)]]) },
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4, TestValue5_5]]), new ComplexMatrixDouble([[new GeneralComplexNumberDouble(-0.18749999999999978, 0.18749999999999978), new GeneralComplexNumberDouble(0.12500000000000006, -0.12500000000000006), new GeneralComplexNumberDouble(0.06249999999999978, -0.06249999999999978)], [new GeneralComplexNumberDouble(-0.8749999999999997, 0.8749999999999997), new GeneralComplexNumberDouble(-0.24999999999999994, 0.24999999999999994), new GeneralComplexNumberDouble(0.6249999999999999, -0.6249999999999999)], [new GeneralComplexNumberDouble(0.8124999999999998, -0.8124999999999998), new GeneralComplexNumberDouble(0.12499999999999996, -0.12499999999999996), new GeneralComplexNumberDouble(-0.4374999999999999, 0.4374999999999999)]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble, ComplexMatrixDouble> TestScalarAdditionTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDouble([[TestValue1_1 + TestValue20_2, TestValue2_2 + TestValue20_2, TestValue3_3 + TestValue20_2], [TestValue4_4 + TestValue20_2, TestValue5_5 + TestValue20_2, TestValue6_6 + TestValue20_2]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble, ComplexMatrixDouble> TestAdditionTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new ComplexMatrixDouble([[TestValue1_1 + TestValue2_2, TestValue2_2 + TestValue3_3, TestValue3_3 + TestValue4_4], [TestValue4_4 + TestValue5_5, TestValue5_5 + TestValue6_6, TestValue6_6 + TestValue7_7]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble, ComplexMatrixDouble> TestScalarSubtractionTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDouble([[TestValue1_1 - TestValue20_2, TestValue2_2 - TestValue20_2, TestValue3_3 - TestValue20_2], [TestValue4_4 - TestValue20_2, TestValue5_5 - TestValue20_2, TestValue6_6 - TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralComplexNumberDouble, ComplexMatrixDouble, ComplexMatrixDouble> TestSubtractionScalarTheoryData =
        new()
        {
            { TestValue20_2, new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue20_2 - TestValue1_1, TestValue20_2 - TestValue2_2, TestValue20_2 - TestValue3_3], [TestValue20_2 - TestValue4_4, TestValue20_2 - TestValue5_5, TestValue20_2 - TestValue6_6]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble, ComplexMatrixDouble> TestSubtractionTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue2_2, TestValue3_3, TestValue4_4], [TestValue5_5, TestValue6_6, TestValue7_7]]), new ComplexMatrixDouble([[TestValue1_1 - TestValue2_2, TestValue2_2 - TestValue3_3, TestValue3_3 - TestValue4_4], [TestValue4_4 - TestValue5_5, TestValue5_5 - TestValue6_6, TestValue6_6 - TestValue7_7]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble, ComplexMatrixDouble> TestScalarMultiplicationTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDouble([[TestValue1_1 * TestValue20_2, TestValue2_2 * TestValue20_2, TestValue3_3 * TestValue20_2], [TestValue4_4 * TestValue20_2, TestValue5_5 * TestValue20_2, TestValue6_6 * TestValue20_2]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, ComplexMatrixDouble, ComplexMatrixDouble> TestMultiplicationTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2], [TestValue3_3, TestValue4_4]]), new ComplexMatrixDouble([[TestValue5_5, TestValue6_6], [TestValue7_7, TestValue8_8]]), new ComplexMatrixDouble([[TestValue1_1 * TestValue5_5 + TestValue2_2 * TestValue7_7, TestValue1_1 * TestValue6_6 + TestValue2_2 * TestValue8_8], [TestValue3_3 * TestValue5_5 + TestValue4_4 * TestValue7_7, TestValue3_3 * TestValue6_6 + TestValue4_4 * TestValue8_8]]) },
            { new ComplexMatrixDouble([[TestValueNeg1, TestValue2, TestValue3], [TestValue4, TestValue0, TestValue5]]), new ComplexMatrixDouble([[TestValue5, TestValueNeg1], [TestValueNeg4, TestValue0], [TestValue2, TestValue3]]), new ComplexMatrixDouble([[TestValueNeg1 * TestValue5 + TestValue2 * TestValueNeg4 + TestValue3 * TestValue2, TestValueNeg1 * TestValueNeg1 + TestValue2 * TestValue0 + TestValue3 * TestValue3], [TestValue4 * TestValue5 + TestValue0 * TestValueNeg4 + TestValue5 * TestValue2, TestValue4 * TestValueNeg1 + TestValue0 * TestValue0 + TestValue5 * TestValue3]]) }
        };

    public static readonly TheoryData<ComplexMatrixDouble, GeneralComplexNumberDouble, ComplexMatrixDouble> TestScalarDivisionTheoryData =
        new()
        {
            { new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), TestValue20_2, new ComplexMatrixDouble([[TestValue1_1 / TestValue20_2, TestValue2_2 / TestValue20_2, TestValue3_3 / TestValue20_2], [TestValue4_4 / TestValue20_2, TestValue5_5 / TestValue20_2, TestValue6_6 / TestValue20_2]]) }
        };

    public static readonly TheoryData<GeneralComplexNumberDouble, ComplexMatrixDouble, ComplexMatrixDouble> TestDivisionScalarTheoryData =
        new()
        {
            { TestValue20_2, new ComplexMatrixDouble([[TestValue1_1, TestValue2_2, TestValue3_3], [TestValue4_4, TestValue5_5, TestValue6_6]]), new ComplexMatrixDouble([[TestValue20_2 / TestValue1_1, TestValue20_2 / TestValue2_2, TestValue20_2 / TestValue3_3], [TestValue20_2 / TestValue4_4, TestValue20_2 / TestValue5_5, TestValue20_2 / TestValue6_6]]) }
        };

    #endregion
}
