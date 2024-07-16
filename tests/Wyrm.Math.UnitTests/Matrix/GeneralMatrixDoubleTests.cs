using Shouldly;
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
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void ArraySetter_Should_Set_ColumnValue(GeneralMatrixDouble matrix)
    {
        matrix[2, 1] = TestValue;
        matrix[2, 1].ShouldBe(TestValue);
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
    [MemberData(nameof(TestDoubleAdditionTheoryData))]
    public void Operator_Add_Should_Add_Double_To_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m + value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDoubleAdditionTheoryData))]
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
    [MemberData(nameof(TestDoubleSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Double_From_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m - value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestSubtractionDoubleTheoryData))]
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
    [MemberData(nameof(TestDoubleMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Double_From_Matrix(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m * value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDoubleMultiplicationTheoryData))]
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
    [MemberData(nameof(TestDoubleDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Double(GeneralMatrixDouble m, double value, GeneralMatrixDouble expected)
    {
        (m / value)
            .ShouldMatch(expected);
    }

    [Theory]
    [MemberData(nameof(TestDivisionDoubleTheoryData))]
    public void Operator_Divide_Should_Divide_Double_By_Matrix(double value, GeneralMatrixDouble m, GeneralMatrixDouble expected)
    {
        (value / m)
            .ShouldMatch(expected);
    }

    #region Test Data

    public const double TestValue = 10.1;

    public static readonly TheoryData<GeneralMatrixDouble> TestGeneralMatrixTheoryData =
        new(new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]));

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, GeneralMatrixDouble> TestMatrixTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], new GeneralMatrixDouble(new GeneralMatrix<double>(3, [1.1, 2.2, 3.3, 4.4, 5.5, 6.6])) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble> TestGeneralMatrixTransposeTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), new GeneralMatrixDouble([[1.1, 4.4], [2.2, 5.5], [3.3, 6.6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double> TestGeneralMatrixTraceTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2], [3.3, 4.4]]), 1.1 + 4.4 }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestDoubleAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), 20.2, new GeneralMatrixDouble([[1.1 + 20.2, 2.2 + 20.2, 3.3 + 20.2], [4.4 + 20.2, 5.5 + 20.2, 6.6 + 20.2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestAdditionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), new GeneralMatrixDouble([[2.2, 3.3, 4.4], [5.5, 6.6, 7.7]]), new GeneralMatrixDouble([[1.1 + 2.2, 2.2 + 3.3, 3.3 + 4.4], [4.4 + 5.5, 5.5 + 6.6, 6.6 + 7.7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestDoubleSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), 20.2, new GeneralMatrixDouble([[1.1 - 20.2, 2.2 - 20.2, 3.3 - 20.2], [4.4 - 20.2, 5.5 - 20.2, 6.6 - 20.2]]) }
        };

    public static readonly TheoryData<double, GeneralMatrixDouble, GeneralMatrixDouble> TestSubtractionDoubleTheoryData =
        new()
        {
            { 20.2, new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), new GeneralMatrixDouble([[20.2 - 1.1, 20.2 - 2.2, 20.2 - 3.3], [20.2 - 4.4, 20.2 - 5.5, 20.2 - 6.6]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestSubtractionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), new GeneralMatrixDouble([[2.2, 3.3, 4.4], [5.5, 6.6, 7.7]]), new GeneralMatrixDouble([[1.1 - 2.2, 2.2 - 3.3, 3.3 - 4.4], [4.4 - 5.5, 5.5 - 6.6, 6.6 - 7.7]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestDoubleMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), 20.2, new GeneralMatrixDouble([[1.1 * 20.2, 2.2 * 20.2, 3.3 * 20.2], [4.4 * 20.2, 5.5 * 20.2, 6.6 * 20.2]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, GeneralMatrixDouble, GeneralMatrixDouble> TestMultiplicationTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2], [3.3, 4.4]]), new GeneralMatrixDouble([[5.5, 6.6], [7.7, 8.8]]), new GeneralMatrixDouble([[1.1 * 5.5 + 2.2 * 7.7, 1.1 * 6.6 + 2.2 * 8.8], [3.3 * 5.5 + 4.4 * 7.7, 3.3 * 6.6 + 4.4 * 8.8]]) },
            { new GeneralMatrixDouble([[-1.0, 2.0, 3.0], [4.0, 0.0, 5.0]]), new GeneralMatrixDouble([[5.0, -1.0], [-4.0, 0.0], [2.0, 3.0]]), new GeneralMatrixDouble([[-1.0 * 5.0 + 2.0 * -4.0 + 3.0 * 2.0, -1.0 * -1.0 + 2.0 * 0.0 + 3.0 * 3.0], [4.0 * 5.0 + 0.0 * -4.0 + 5.0 * 2.0, 4.0 * -1.0 + 0.0 * 0.0 + 5.0 * 3.0]]) }
        };

    public static readonly TheoryData<GeneralMatrixDouble, double, GeneralMatrixDouble> TestDoubleDivisionTheoryData =
        new()
        {
            { new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), 20.2, new GeneralMatrixDouble([[1.1 / 20.2, 2.2 / 20.2, 3.3 / 20.2], [4.4 / 20.2, 5.5 / 20.2, 6.6 / 20.2]]) }
        };

    public static readonly TheoryData<double, GeneralMatrixDouble, GeneralMatrixDouble> TestDivisionDoubleTheoryData =
        new()
        {
            { 20.2, new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]), new GeneralMatrixDouble([[20.2 / 1.1, 20.2 / 2.2, 20.2 / 3.3], [20.2 / 4.4, 20.2 / 5.5, 20.2 / 6.6]]) }
        };

    #endregion
}
