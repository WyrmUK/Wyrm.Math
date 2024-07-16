using Shouldly;
using Wyrm.Math.Matrix;

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
        Should.NotThrow(() => new GeneralMatrixDouble(0, 0));
    }

    [Theory]
    [InlineData(3, 2)]
    public void Constructor_Should_Create_Initialised_Matrix(int columns, int rows)
    {
        new GeneralMatrixDouble(columns, rows).Values
            .Sum(v => v.Where(d => d == 0.0).Count()).ShouldBe(columns * rows);
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Populated_Matrix(IEnumerable<IEnumerable<double>> values)
    {
        new GeneralMatrixDouble(values).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(values.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Constructor_Should_Create_Copy_Matrix(IEnumerable<IEnumerable<double>> values)
    {
        new GeneralMatrixDouble(new GeneralMatrixDouble(values)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(values.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Rows_Should_Get_Rows(IEnumerable<IEnumerable<double>> values)
    {
        new GeneralMatrixDouble(values).Rows
            .ShouldBe(values.Count());
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Columns_Should_Get_Columns(IEnumerable<IEnumerable<double>> values)
    {
        new GeneralMatrixDouble(values).Columns
            .ShouldBe(values.Max(v => v.Count()));
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTheoryData))]
    public void ArraySetter_Should_Set_ColumnValue(GeneralMatrixDouble matrix)
    {
        matrix[2, 1] = TestValue;
        matrix[2, 1].ShouldBe(TestValue);
    }

    [Theory]
    [MemberData(nameof(TestGeneralMatrixTransposeTheoryData))]
    public void Transpose_Should_Transpose_Matrix(GeneralMatrixDouble matrix, GeneralMatrixDouble expected)
    {
        matrix.Transpose().Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.Values.SelectMany(v => v.Select(d => d)));
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
    public void Operator_Add_Should_Add_Double_To_Matrix(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m) + value).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestDoubleAdditionTheoryData))]
    public void Operator_Add_Should_Add_Matrix_To_Double(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (value + new GeneralMatrixDouble(m)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
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
    public void Operator_Add_Should_Add_Matrices(IEnumerable<IEnumerable<double>> m1, IEnumerable<IEnumerable<double>> m2, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m1) + new GeneralMatrixDouble(m2)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Operator_Copy_Should_Copy_Matrix(IEnumerable<IEnumerable<double>> values)
    {
        (+new GeneralMatrixDouble(values)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(values.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestDoubleSubtractionTheoryData))]
    public void Operator_Subtract_Should_Subtract_Double_From_Matrix(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m) - value).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestSubtractionDoubleTheoryData))]
    public void Operator_Subtract_Should_Subtract_Matrix_From_Double(double value, IEnumerable<IEnumerable<double>> m, IEnumerable<IEnumerable<double>> expected)
    {
        (value - new GeneralMatrixDouble(m)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
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
    public void Operator_Subtract_Should_Add_Matrices(IEnumerable<IEnumerable<double>> m1, IEnumerable<IEnumerable<double>> m2, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m1) - new GeneralMatrixDouble(m2)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestMatrixTheoryData))]
    public void Operator_Negate_Should_Negate_Matrix(IEnumerable<IEnumerable<double>> values)
    {
        (-new GeneralMatrixDouble(values)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(values.SelectMany(v => v.Select(d => -d)));
    }

    [Theory]
    [MemberData(nameof(TestDoubleMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Double_From_Matrix(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m) * value).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestDoubleMultiplicationTheoryData))]
    public void Operator_Multiply_Should_Multiply_Matrix_From_Double(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (value * new GeneralMatrixDouble(m)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
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
    public void Operator_Multiple_Should_Multiple_Matrices(IEnumerable<IEnumerable<double>> m1, IEnumerable<IEnumerable<double>> m2, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m1) * new GeneralMatrixDouble(m2)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestDoubleDivisionTheoryData))]
    public void Operator_Divide_Should_Divide_Matrix_By_Double(IEnumerable<IEnumerable<double>> m, double value, IEnumerable<IEnumerable<double>> expected)
    {
        (new GeneralMatrixDouble(m) / value).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    [Theory]
    [MemberData(nameof(TestDivisionDoubleTheoryData))]
    public void Operator_Divide_Should_Divide_Double_By_Matrix(double value, IEnumerable<IEnumerable<double>> m, IEnumerable<IEnumerable<double>> expected)
    {
        (value / new GeneralMatrixDouble(m)).Values
            .SelectMany(v => v.Select(d => d))
            .ShouldBeEquivalentTo(expected.SelectMany(v => v.Select(d => d)));
    }

    #region Test Data

    public const double TestValue = 10.1;

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>> TestMatrixTheoryData =
        new([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]);

    public static readonly TheoryData<GeneralMatrixDouble> TestGeneralMatrixTheoryData =
        new(new GeneralMatrixDouble([[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]]));

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

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, double, IEnumerable<IEnumerable<double>>> TestDoubleAdditionTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], 20.2, [[1.1 + 20.2, 2.2 + 20.2, 3.3 + 20.2], [4.4 + 20.2, 5.5 + 20.2, 6.6 + 20.2]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>> TestAdditionTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], [[2.2, 3.3, 4.4], [5.5, 6.6, 7.7]], [[1.1 + 2.2, 2.2 + 3.3, 3.3 + 4.4], [4.4 + 5.5, 5.5 + 6.6, 6.6 + 7.7]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, double, IEnumerable<IEnumerable<double>>> TestDoubleSubtractionTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], 20.2, [[1.1 - 20.2, 2.2 - 20.2, 3.3 - 20.2], [4.4 - 20.2, 5.5 - 20.2, 6.6 - 20.2]] }
        };

    public static readonly TheoryData<double, IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>> TestSubtractionDoubleTheoryData =
        new()
        {
            { 20.2, [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], [[20.2 - 1.1, 20.2 - 2.2, 20.2 - 3.3], [20.2 - 4.4, 20.2 - 5.5, 20.2 - 6.6]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>> TestSubtractionTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], [[2.2, 3.3, 4.4], [5.5, 6.6, 7.7]], [[1.1 - 2.2, 2.2 - 3.3, 3.3 - 4.4], [4.4 - 5.5, 5.5 - 6.6, 6.6 - 7.7]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, double, IEnumerable<IEnumerable<double>>> TestDoubleMultiplicationTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], 20.2, [[1.1 * 20.2, 2.2 * 20.2, 3.3 * 20.2], [4.4 * 20.2, 5.5 * 20.2, 6.6 * 20.2]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>> TestMultiplicationTheoryData =
        new()
        {
            { [[1.1, 2.2], [3.3, 4.4]], [[5.5, 6.6], [7.7, 8.8]], [[1.1 * 5.5 + 2.2 * 7.7, 1.1 * 6.6 + 2.2 * 8.8], [3.3 * 5.5 + 4.4 * 7.7, 3.3 * 6.6 + 4.4 * 8.8]] },
            { [[-1.0, 2.0, 3.0], [4.0, 0.0, 5.0]], [[5.0, -1.0], [-4.0, 0.0], [2.0, 3.0]], [[-1.0 * 5.0 + 2.0 * -4.0 + 3.0 * 2.0, -1.0 * -1.0 + 2.0 * 0.0 + 3.0 * 3.0], [4.0 * 5.0 + 0.0 * -4.0 + 5.0 * 2.0, 4.0 * -1.0 + 0.0 * 0.0 + 5.0 * 3.0]] }
        };

    public static readonly TheoryData<IEnumerable<IEnumerable<double>>, double, IEnumerable<IEnumerable<double>>> TestDoubleDivisionTheoryData =
        new()
        {
            { [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], 20.2, [[1.1 / 20.2, 2.2 / 20.2, 3.3 / 20.2], [4.4 / 20.2, 5.5 / 20.2, 6.6 / 20.2]] }
        };

    public static readonly TheoryData<double, IEnumerable<IEnumerable<double>>, IEnumerable<IEnumerable<double>>> TestDivisionDoubleTheoryData =
        new()
        {
            { 20.2, [[1.1, 2.2, 3.3], [4.4, 5.5, 6.6]], [[20.2 / 1.1, 20.2 / 2.2, 20.2 / 3.3], [20.2 / 4.4, 20.2 / 5.5, 20.2 / 6.6]] }
        };

    #endregion
}
