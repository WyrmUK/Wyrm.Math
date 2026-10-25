using Wyrm.Math.ComplexNumbers;

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

    /// <summary>
    /// Creates a new <see cref="ComplexMatrixDecimal"/> from <see cref="GeneralComplexNumberDecimal"/> values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/>.</returns>
    public static ComplexMatrixDecimal AsMatrix(this IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values) =>
        new(values);

    /// <summary>
    /// Creates a new <see cref="ComplexMatrixDouble"/> from <see cref="GeneralComplexNumberDouble"/> values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <returns>A <see cref="ComplexMatrixDouble"/>.</returns>
    public static ComplexMatrixDouble AsMatrix(this IEnumerable<IEnumerable<GeneralComplexNumberDouble>> values) =>
        new(values);
}
