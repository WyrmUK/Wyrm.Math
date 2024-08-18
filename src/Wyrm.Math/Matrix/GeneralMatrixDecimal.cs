using Wyrm.Math.Matrix.Base;
using Wyrm.Math.Matrix.Extensions;

namespace Wyrm.Math.Matrix;

/// <summary>
/// A general matrix struct for <see cref="decimal"/> values.
/// </summary>
public readonly struct GeneralMatrixDecimal
{
    internal GeneralMatrix<decimal> Matrix { get; }

    /// <summary>
    /// Creates a new empty <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="columns">The number of columns.</param>
    /// <param name="rows">The number of rows.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0 or negative.</exception>
    public GeneralMatrixDecimal(int columns, int rows)
    {
        Matrix = new GeneralMatrix<decimal>(columns, rows);
    }

    /// <summary>
    /// Creates a new populated <see cref="GeneralMatrixDecimal"/>.
    /// The rows and columns are set from the values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0.</exception>
    public GeneralMatrixDecimal(IEnumerable<IEnumerable<decimal>> values)
    {
        Matrix = new GeneralMatrix<decimal>(values);
    }

    /// <summary>
    /// Creates a new populated <see cref="GeneralMatrixDecimal"/>.
    /// This will be a copy of the source matrix.
    /// </summary>
    /// <param name="matrix">The matrix to copy from.</param>
    public GeneralMatrixDecimal(GeneralMatrixDecimal matrix)
    {
        Matrix = new GeneralMatrix<decimal>(matrix.Matrix);
    }

    internal GeneralMatrixDecimal(GeneralMatrix<decimal> matrix)
    {
        Matrix = matrix;
    }

    /// <summary>
    /// Gets the number of rows for the matrix.
    /// </summary>
    public int Rows => Matrix.Rows;

    /// <summary>
    /// Gets the number of columns for the matrix.
    /// </summary>
    public int Columns => Matrix.Columns;

    /// <summary>
    /// Gets and sets specific values of the matrix.
    /// </summary>
    /// <param name="column">The column to get/set the value in.</param>
    /// <param name="row">The row to get/set the value in.</param>
    /// <returns>The value.</returns>
    public decimal this[int column, int row]
    {
        get => Matrix[column, row];
        set => Matrix.Set(column, row, value);
    }

    /// <summary>
    /// Gets the values of the matrix as an enumeration of enumerations of the values.
    /// </summary>
    /// <returns>An enumeration of rows of enumeration of column values.</returns>
    public IEnumerable<IEnumerable<decimal>> ToEnumerableOfEnumerable() => Matrix.ToEnumerableOfEnumerable();

    /// <summary>
    /// Transposes the matrix.
    /// </summary>
    /// <returns>A new <see cref="GeneralMatrixDecimal"/> with transposed values.</returns>
    public GeneralMatrixDecimal Transpose() => new(Matrix.Transpose());

    /// <summary>
    /// Returns the Trace of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The Trace.</returns>
    /// <exception cref="ArgumentException">Throw if the matrix isn't square.</exception>
    public decimal Trace() => Matrix.Trace((v1, v2) => v1 + v2);

    /// <summary>
    /// Returns the Determinant of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The Determinant as a <see cref="double"/>.</returns>
    /// <exception cref="ArgumentException">Throw if the matrix isn't square.</exception>
    public double Determinant() => Matrix.Determinant(v => (double)v);

    /// <summary>
    /// Returns the rank of a matrix.
    /// </summary>
    /// <returns>The rank.</returns>
    public int Rank() => Matrix.Rank(v => (double)v);

    /// <summary>
    /// Returns the nullity of a matrix.
    /// </summary>
    /// <returns>The nullity.</returns>
    public int Nullity() => Matrix.Nullity(v => (double)v);

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the sum of the two operands.</returns>
    public static GeneralMatrixDecimal operator +(GeneralMatrixDecimal m, decimal scalar) =>
        new(m.Matrix.PerformOperation(v => v + scalar));

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="m">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the sum of the two operands.</returns>
    public static GeneralMatrixDecimal operator +(decimal scalar, GeneralMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(v => v + scalar));

    /// <summary>
    /// Adds two <see cref="GeneralMatrixDecimal"/>s together.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the sum of the two operands.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static GeneralMatrixDecimal operator +(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2) =>
        new(m1.Matrix.PerformOperation(m2.Matrix, (v1, v2) => v1 + v2));

    /// <summary>
    /// Generates a new <see cref="GeneralMatrixDecimal"/> from an existing one.
    /// </summary>
    /// <param name="m">The <see cref="GeneralMatrixDecimal"/> to copy from.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> with the values of the operand.</returns>
    public static GeneralMatrixDecimal operator +(GeneralMatrixDecimal m) =>
        new(m.Matrix);

    /// <summary>
    /// Subtracts a single value from each value of a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralMatrixDecimal operator -(GeneralMatrixDecimal m, decimal scalar) =>
        new(m.Matrix.PerformOperation(v => v - scalar));

    /// <summary>
    /// Subtracts each value of a <see cref="GeneralMatrixDecimal"/> from a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="decimal"/>.</param>
    /// <param name="m">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralMatrixDecimal operator -(decimal scalar, GeneralMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(v => scalar - v));

    /// <summary>
    /// Subtracts one <see cref="GeneralMatrixDecimal"/> from another.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the left hand operand minus the right hand operand.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static GeneralMatrixDecimal operator -(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2) =>
        new(m1.Matrix.PerformOperation(m2.Matrix, (v1, v2) => v1 - v2));

    /// <summary>
    /// Negates all values in a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">The <see cref="GeneralMatrixDecimal"/> to negate.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> which is the negated operand.</returns>
    public static GeneralMatrixDecimal operator -(GeneralMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(v => -v));

    /// <summary>
    /// Multiplies each value of a <see cref="GeneralMatrixDecimal"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDecimal operator *(GeneralMatrixDecimal m, decimal scalar) =>
        new(m.Matrix.PerformOperation(v => v * scalar));

    /// <summary>
    /// Multiplies a scalar with each value of a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="decimal"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDecimal operator *(decimal m1, GeneralMatrixDecimal m2) =>
        new(m2.Matrix.PerformOperation(v => m1 * v));

    /// <summary>
    /// Multiplies each value of a <see cref="GeneralMatrixDecimal"/> with another <see cref="GeneralMatrixDecimal"/>.
    /// This uses the naive method.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDecimal operator *(GeneralMatrixDecimal m1, GeneralMatrixDecimal m2) =>
        new(m1.Matrix.PerformMultiplyOperation(m2.Matrix, (v1, v2) => v1 * v2, (v1, v2) => v1 + v2));

    /// <summary>
    /// Divides a each value of a <see cref="GeneralMatrixDecimal"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="decimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand divided by the right hand value.</returns>
    public static GeneralMatrixDecimal operator /(GeneralMatrixDecimal m, decimal scalar) =>
        new(m.Matrix.PerformOperation(v => v / scalar));

    /// <summary>
    /// Divides a scalar with each value of a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="decimal"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDecimal"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/> of the of the left hand operand divided by the right hand value.</returns>
    public static GeneralMatrixDecimal operator /(decimal m1, GeneralMatrixDecimal m2) =>
        new(m2.Matrix.PerformOperation(v => m1 / v));
}
