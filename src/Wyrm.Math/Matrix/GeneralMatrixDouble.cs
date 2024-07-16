namespace Wyrm.Math.Matrix;

/// <summary>
/// A <see cref="GeneralMatrix{T}"/> for <see cref="double"/> values.
/// </summary>
public sealed class GeneralMatrixDouble : IGeneralMatrix<double>
{
    private readonly GeneralMatrix<double> _matrix;

    /// <summary>
    /// Creates a new empty <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="columns">The number of columns.</param>
    /// <param name="rows">The number of rows.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0 or negative.</exception>
    public GeneralMatrixDouble(int columns, int rows)
    {
        _matrix = new GeneralMatrix<double>(columns, rows);
    }

    /// <summary>
    /// Creates a new populated <see cref="GeneralMatrixDouble"/>.
    /// The rows and columns are set from the values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0.</exception>
    public GeneralMatrixDouble(IEnumerable<IEnumerable<double>> values)
    {
        _matrix = new GeneralMatrix<double>(values);
    }

    /// <summary>
    /// Creates a new populated <see cref="GeneralMatrixDouble"/>.
    /// This will be a copy of the source matrix.
    /// </summary>
    /// <param name="matrix">The matrix to copy from.</param>
    public GeneralMatrixDouble(IGeneralMatrix<double> matrix)
    {
        _matrix = new GeneralMatrix<double>(matrix.Values);
    }

    /// <inheritdoc/>
    public int Rows => _matrix.Rows;

    /// <inheritdoc/>
    public int Columns => _matrix.Columns;

    /// <inheritdoc/>
    public IEnumerable<IEnumerable<double>> Values => _matrix.Values;

    /// <inheritdoc/>
    public double this[int column, int row]
    {
        get => _matrix[column, row];
        set => _matrix[column, row] = value;
    }

    /// <summary>
    /// Transposes the matrix.
    /// </summary>
    /// <returns>A new <see cref="GeneralMatrixDouble"/> with transposed values.</returns>
    public GeneralMatrixDouble Transpose() => new(_matrix.Transpose());

    /// <summary>
    /// Returns the Trace of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The Trace.</returns>
    /// <exception cref="ArgumentException">Throw if the matrix isn't square.</exception>
    public double Trace() => _matrix.Trace((v1, v2) => v1 + v2);

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the sum of the two operands.</returns>
    public static GeneralMatrixDouble operator +(GeneralMatrixDouble m, double scalar) =>
        new(m._matrix.PerformOperation(v => v + scalar));

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="m">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the sum of the two operands.</returns>
    public static GeneralMatrixDouble operator +(double scalar, GeneralMatrixDouble m) =>
        new(m._matrix.PerformOperation(v => v + scalar));

    /// <summary>
    /// Adds two <see cref="GeneralMatrixDouble"/>s together.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the sum of the two operands.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static GeneralMatrixDouble operator +(GeneralMatrixDouble m1, GeneralMatrixDouble m2) =>
        new(m1._matrix.PerformOperation(m2._matrix, (v1, v2) => v1 + v2));

    /// <summary>
    /// Generates a new <see cref="GeneralMatrixDouble"/> from an existing one.
    /// </summary>
    /// <param name="m">The <see cref="GeneralMatrixDouble"/> to copy from.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> with the values of the operand.</returns>
    public static GeneralMatrixDouble operator +(GeneralMatrixDouble m) =>
        new(m._matrix);

    /// <summary>
    /// Subtracts a single value from each value of a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralMatrixDouble operator -(GeneralMatrixDouble m, double scalar) =>
        new(m._matrix.PerformOperation(v => v - scalar));

    /// <summary>
    /// Subtracts each value of a <see cref="GeneralMatrixDouble"/> from a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="double"/>.</param>
    /// <param name="m">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand minus the right hand value.</returns>
    public static GeneralMatrixDouble operator -(double scalar, GeneralMatrixDouble m) =>
        new(m._matrix.PerformOperation(v => scalar - v));

    /// <summary>
    /// Subtracts one <see cref="GeneralMatrixDouble"/> from another.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the left hand operand minus the right hand operand.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static GeneralMatrixDouble operator -(GeneralMatrixDouble m1, GeneralMatrixDouble m2) =>
        new(m1._matrix.PerformOperation(m2._matrix, (v1, v2) => v1 - v2));

    /// <summary>
    /// Negates all values in a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m">The <see cref="GeneralMatrixDouble"/> to negate.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> which is the negated operand.</returns>
    public static GeneralMatrixDouble operator -(GeneralMatrixDouble m) =>
        new(m._matrix.PerformOperation(v => -v));

    /// <summary>
    /// Multiplies each value of a <see cref="GeneralMatrixDouble"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDouble operator *(GeneralMatrixDouble m, double scalar) =>
        new(m._matrix.PerformOperation(v => v * scalar));

    /// <summary>
    /// Multiplies a scalar with each value of a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="double"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDouble operator *(double m1, GeneralMatrixDouble m2) =>
        new(m2._matrix.PerformOperation(v => m1 * v));

    /// <summary>
    /// Multiplies each value of a <see cref="GeneralMatrixDouble"/> with another <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static GeneralMatrixDouble operator *(GeneralMatrixDouble m1, GeneralMatrixDouble m2) =>
        new(m1._matrix.PerformMultiplyOperation(m2._matrix, (v1, v2) => v1 * v2, (v1, v2) => v1 + v2));

    /// <summary>
    /// Divides a each value of a <see cref="GeneralMatrixDouble"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <param name="scalar">Right hand <see cref="double"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand divided by the right hand value.</returns>
    public static GeneralMatrixDouble operator /(GeneralMatrixDouble m, double scalar) =>
        new(m._matrix.PerformOperation(v => v / scalar));

    /// <summary>
    /// Divides a scalar with each value of a <see cref="GeneralMatrixDouble"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="double"/>.</param>
    /// <param name="m2">Right hand <see cref="GeneralMatrixDouble"/>.</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/> of the of the left hand operand divided by the right hand value.</returns>
    public static GeneralMatrixDouble operator /(double m1, GeneralMatrixDouble m2) =>
        new(m2._matrix.PerformOperation(v => m1 / v));
}
