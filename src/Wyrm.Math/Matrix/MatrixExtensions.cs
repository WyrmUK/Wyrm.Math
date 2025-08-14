namespace Wyrm.Math.Matrix;

/// <summary>
/// Extension methods for matrices.
/// </summary>
public static class MatrixExtensions
{
    /// <summary>
    /// Creates a new <see cref="GeneralMatrixDecimal"/> from decimal values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <returns>A <see cref="GeneralMatrixDecimal"/>.</returns>
    public static GeneralMatrixDecimal AsMatrix(this IEnumerable<IEnumerable<decimal>> values) =>
        new(values);

    /// <summary>
    /// Creates a new <see cref="GeneralMatrixDouble"/> from double values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <returns>A <see cref="GeneralMatrixDouble"/>.</returns>
    public static GeneralMatrixDouble AsMatrix(this IEnumerable<IEnumerable<double>> values) =>
        new(values);
}
