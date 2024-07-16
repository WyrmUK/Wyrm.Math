namespace Wyrm.Math.Matrix;

/// <summary>
/// An interface for a general matrix for a specific type.
/// </summary>
/// <typeparam name="T">The numeric type for the matrix elements.</typeparam>
public interface IGeneralMatrix<T> where T : struct
{
    /// <summary>
    /// Gets the number of rows for the matrix.
    /// </summary>
    int Rows { get; }
    /// <summary>
    /// Gets the number of columns for the matrix.
    /// </summary>
    int Columns { get; }
    /// <summary>
    /// Gets the values as an <see cref="IEnumerable{T}"/> set of rows of <see cref="IEnumerable{T}"/>.
    /// </summary>
    IEnumerable<IEnumerable<T>> Values { get; }
    /// <summary>
    /// Gets and sets the value at a particular column and row.
    /// </summary>
    /// <param name="column">The column to fetch from.</param>
    /// <param name="row">The row to fetch from.</param>
    /// <returns>The value at the column and row.</returns>
    T this[int column, int row] { get; set; }
}
