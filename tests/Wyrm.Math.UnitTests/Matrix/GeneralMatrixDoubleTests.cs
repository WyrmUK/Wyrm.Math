﻿using Shouldly;
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
