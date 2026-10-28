using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.Matrix.Extensions;

namespace Wyrm.Math.Matrix;

/// <summary>
/// A complex matrix struct for <see cref="decimal"/> values.
/// </summary>
[JsonConverter(typeof(ComplexMatrixDecimalJsonConverter))]
public readonly struct ComplexMatrixDecimal :
    IParsable<ComplexMatrixDecimal>,
    ISpanParsable<ComplexMatrixDecimal>,
    IFormattable,
    IEquatable<object>,
    IEquatable<ComplexMatrixDecimal>,
    IComparable,
    IComparable<ComplexMatrixDecimal>,
    IAdditionOperators<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>,
    IAdditionOperators<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal>,
    ISubtractionOperators<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>,
    ISubtractionOperators<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal>,
    IMultiplyOperators<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>,
    IMultiplyOperators<ComplexMatrixDecimal, ComplexMatrixDecimal, ComplexMatrixDecimal>,
    IDivisionOperators<ComplexMatrixDecimal, GeneralComplexNumberDecimal, ComplexMatrixDecimal>
{
    internal GeneralMatrix<GeneralComplexNumberDecimal> Matrix { get; }

    #region Constructors

    /// <summary>
    /// Creates a new empty <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="columns">The number of columns.</param>
    /// <param name="rows">The number of rows.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0 or negative.</exception>
    public ComplexMatrixDecimal(int columns, int rows)
    {
        Matrix = new GeneralMatrix<GeneralComplexNumberDecimal>(columns, rows);
    }

    /// <summary>
    /// Creates a new populated <see cref="ComplexMatrixDecimal"/>.
    /// The rows and columns are set from the values.
    /// Accepts column values of different lengths - the number of columns will be the largest length.
    /// </summary>
    /// <param name="values">The values to populate it with (an <see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> column values).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the columns or rows are 0.</exception>
    public ComplexMatrixDecimal(IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> values)
    {
        Matrix = new GeneralMatrix<GeneralComplexNumberDecimal>(values);
    }

    /// <summary>
    /// Creates a new populated <see cref="ComplexMatrixDecimal"/>.
    /// This will be a copy of the source matrix.
    /// </summary>
    /// <param name="matrix">The matrix to copy from.</param>
    public ComplexMatrixDecimal(ComplexMatrixDecimal matrix)
    {
        Matrix = new GeneralMatrix<GeneralComplexNumberDecimal>(matrix.Matrix);
    }

    internal ComplexMatrixDecimal(GeneralMatrix<GeneralComplexNumberDecimal> matrix)
    {
        Matrix = matrix;
    }

    #endregion

    #region ToString

    /// <summary>
    /// Returns a human-readable representation of this matrix.
    /// </summary>
    /// <returns>The human-readable representation.</returns>
    public override string ToString() => Matrix.ToString();

    /// <summary>
    /// Returns a human-readable representation of this matrix using the specified format.
    /// </summary>
    /// <param name="format">The format to use on the values.</param>
    /// <returns>The human-readable representation.</returns>
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format) =>
        ToString(format, null);

    /// <summary>
    /// Returns a human-readable representation of this matrix using the specified culture-specific format.
    /// </summary>
    /// <param name="formatProvider">The culture-specific format to use on the values.</param>
    /// <returns>The human-readable representation.</returns>
    public string ToString(IFormatProvider? formatProvider) =>
        ToString(null, formatProvider);

    /// <summary>
    /// Returns a human-readable representation of this matrix using the specified format and culture-specific format.
    /// </summary>
    /// <param name="format">The format to use on the real and imaginary parts.</param>
    /// <param name="formatProvider">The culture-specific format to use on the values.</param>
    /// <returns>The human-readable representation.</returns>
    public string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider) =>
        Matrix.ToString(x => x.ToString(format, formatProvider));

    #endregion

    #region Parse

    /// <summary>
    /// Parses a string into a value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>The result of parsing s.</returns>
    public static ComplexMatrixDecimal Parse(string s) => Parse(s, null);

    /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
    public static ComplexMatrixDecimal Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var value)) return value;
        throw new FormatException();
    }

    /// <summary>
    /// Tries to parse a string into a value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">When this method returns, contains the result of successfully parsing s or an undefined value on failure.</param>
    /// <returns><see langword="true"/> if s was successfully parsed; otherwise <see langword="false"/>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out ComplexMatrixDecimal result) =>
        TryParse(s, null, out result);

    /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ComplexMatrixDecimal result)
    {
        if (s == null)
        {
            result = default;
            return false;
        }
        return TryParse(s.AsSpan(), provider, out result);
    }

    /// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{char}, IFormatProvider?)"/>
    public static ComplexMatrixDecimal Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var value)) return value;
        throw new FormatException();
    }

    /// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{char}, IFormatProvider?, out TSelf)"/>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out ComplexMatrixDecimal result)
    {
        var parsed = GeneralMatrix<GeneralComplexNumberDecimal>.TryParse(s, GeneralComplexNumberDecimalTryParse, out var matrix);
        result = new ComplexMatrixDecimal(matrix);
        return parsed;

        bool GeneralComplexNumberDecimalTryParse(ReadOnlySpan<char> source, NumberStyles numberStyles, out GeneralComplexNumberDecimal value) =>
            GeneralComplexNumberDecimal.TryParse(source, provider, out value);
    }

    #endregion

    #region GetHashCode Equals

    /// <inheritdoc cref="GeneralMatrix{T}.GetHashCode()"/>
    public override int GetHashCode() => Matrix.GetHashCode();

    /// <inheritdoc cref="GeneralMatrix{T}.Equals(object?)"/>
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        Matrix.Equals((obj as ComplexMatrixDecimal?)?.Matrix);

    /// <summary>
    /// Indicates whether the current <see cref="ComplexMatrixDecimal"/> is equal to another <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="matrix">The <see cref="ComplexMatrixDecimal"/> to compare with this <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns><see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(ComplexMatrixDecimal matrix) =>
        Matrix.Equals(matrix.Matrix);

    #endregion

    #region operator == !=

    /// <summary>
    /// Indicates whether two <see cref="ComplexMatrixDecimal"/>s are equal.
    /// </summary>
    /// <param name="left">A <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="right">A <see cref="ComplexMatrixDecimal"/> to compare.</param>
    /// <returns>True if both instances are equal.</returns>
    public static bool operator ==(ComplexMatrixDecimal left, ComplexMatrixDecimal? right) => left.Equals(right);

    /// <summary>
    /// Indicates whether two <see cref="ComplexMatrixDecimal"/>s are not equal.
    /// </summary>
    /// <param name="left">A <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="right">A <see cref="ComplexMatrixDecimal"/> to compare.</param>
    /// <returns>True if both instances are not equal.</returns>
    public static bool operator !=(ComplexMatrixDecimal left, ComplexMatrixDecimal? right) => !(left == right);

    #endregion

    #region Comparable

    /// <inheritdoc/>
    public int CompareTo(object? obj) =>
        obj is not ComplexMatrixDecimal value
            ? throw new ArgumentException("Wrong type for compare.", nameof(obj))
            : CompareTo(value);

    /// <inheritdoc/>
    public int CompareTo(ComplexMatrixDecimal obj) =>
        Matrix.CompareTo(obj.Matrix, (v1, v2) => v1.CompareTo(v2));

    #endregion

    #region Casts

    /// <summary>
    /// Casts a <see cref="ComplexMatrixDecimal"/> to a <see cref="GeneralMatrixDecimal"/>.
    /// </summary>
    /// <param name="complexMatrix">The <see cref="ComplexMatrixDecimal"/> to convert.</param>
    /// <exception cref="ArgumentException">Thrown if an imaginary part is non zero.</exception>
    public static implicit operator GeneralMatrixDecimal(ComplexMatrixDecimal complexMatrix) =>
        new(new GeneralMatrix<decimal>(complexMatrix.Columns, complexMatrix.Matrix.Values.Select(x => (decimal)x).ToArray()));

    /// <summary>
    /// Casts a <see cref="GeneralMatrixDecimal"/> to a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="matrix">The <see cref="GeneralMatrixDecimal"/> to convert.</param>
    public static explicit operator ComplexMatrixDecimal(GeneralMatrixDecimal matrix) =>
        new(new GeneralMatrix<GeneralComplexNumberDecimal>(matrix.Columns, matrix.Matrix.Values.Select(x => (GeneralComplexNumberDecimal)x).ToArray()));

    /// <summary>
    /// Casts a <see cref="ComplexMatrixDecimal"/> to a <see cref="GeneralMatrixDouble"/>.
    /// If there is an imaginary part to an item then the item is NaN.
    /// </summary>
    /// <param name="complexMatrix">The <see cref="ComplexMatrixDecimal"/> to convert.</param>
    public static implicit operator GeneralMatrixDouble(ComplexMatrixDecimal complexMatrix) =>
        new(new GeneralMatrix<double>(complexMatrix.Columns, complexMatrix.Matrix.Values.Select(x => (double)x).ToArray()));

    /// <summary>
    /// Casts a <see cref="GeneralMatrixDouble"/> to a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="matrix">The <see cref="GeneralMatrixDouble"/> to convert.</param>
    public static explicit operator ComplexMatrixDecimal(GeneralMatrixDouble matrix) =>
        new(new GeneralMatrix<GeneralComplexNumberDecimal>(matrix.Columns, matrix.Matrix.Values.Select(x => (GeneralComplexNumberDecimal)x).ToArray()));

    /// <summary>
    /// Casts a <see cref="ComplexMatrixDecimal"/> to a <see cref="ComplexMatrixDouble"/>.
    /// </summary>
    /// <param name="matrix">The <see cref="ComplexMatrixDecimal"/> to convert.</param>
    public static implicit operator ComplexMatrixDouble(ComplexMatrixDecimal matrix) =>
        new(new GeneralMatrix<GeneralComplexNumberDouble>(matrix.Columns, matrix.Matrix.Values.Select(x => (GeneralComplexNumberDouble)x).ToArray()));

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of rows for the matrix.
    /// </summary>
    public int Rows => Matrix.Rows;

    /// <summary>
    /// Gets the number of columns for the matrix.
    /// </summary>
    public int Columns => Matrix.Columns;

    /// <summary>
    /// Gets specific values of the matrix.
    /// </summary>
    /// <param name="column">The column to get/set the value in.</param>
    /// <param name="row">The row to get/set the value in.</param>
    /// <returns>The value.</returns>
    public GeneralComplexNumberDecimal this[int column, int row]
    {
        get => Matrix[column, row];
    }

    #endregion

    #region Matrix Methods

    /// <summary>
    /// Returns true only if the matrix is square (rows == columns).
    /// </summary>
    public bool IsSquare => Matrix.IsSquare();

    /// <summary>
    /// Gets the values of the matrix as an enumeration of enumerations of the values.
    /// </summary>
    /// <returns>An enumeration of rows of enumeration of column values.</returns>
    public IEnumerable<IEnumerable<GeneralComplexNumberDecimal>> ToEnumerableOfEnumerable() => Matrix.ToEnumerableOfEnumerable();

    /// <summary>
    /// Transposes the matrix.
    /// </summary>
    /// <returns>A new <see cref="ComplexMatrixDecimal"/> with transposed values.</returns>
    public ComplexMatrixDecimal Transpose() => new(Matrix.Transpose());

    /// <summary>
    /// Returns the Trace of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The Trace.</returns>
    /// <exception cref="ArgumentException">Throw if the matrix isn't square.</exception>
    public GeneralComplexNumberDecimal Trace() => Matrix.Trace(TypeFunctions);

    /// <summary>
    /// Returns the Determinant of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The Determinant as a <see cref="GeneralComplexNumberDecimal"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the matrix isn't square.</exception>
    public GeneralComplexNumberDecimal Determinant() => Matrix.Determinant(TypeFunctions);

    /// <summary>
    /// Returns the rank of a matrix.
    /// </summary>
    /// <returns>The rank.</returns>
    public int Rank() => Matrix.Rank(TypeFunctions);

    /// <summary>
    /// Returns the nullity of a matrix.
    /// </summary>
    /// <returns>The nullity.</returns>
    public int Nullity() => Matrix.Nullity(TypeFunctions);

    /// <summary>
    /// Returns the inverse of a square matrix (same number of columns as rows).
    /// </summary>
    /// <returns>The inverse as a <see cref="ComplexMatrixDecimal"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the matrix isn't square or invertible.</exception>
    public ComplexMatrixDecimal Inverse() => new(Matrix.Inverse(TypeFunctions));

    #endregion

    #region Addition/Subtraction

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the sum of the two operands.</returns>
    public static ComplexMatrixDecimal operator +(ComplexMatrixDecimal m, GeneralComplexNumberDecimal scalar) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Add(v, scalar)));

    /// <summary>
    /// Adds a scalar value to each value of a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="m">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the sum of the two operands.</returns>
    public static ComplexMatrixDecimal operator +(GeneralComplexNumberDecimal scalar, ComplexMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Add(v, scalar)));

    /// <summary>
    /// Adds two <see cref="ComplexMatrixDecimal"/>s together.
    /// </summary>
    /// <param name="m1">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the sum of the two operands.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static ComplexMatrixDecimal operator +(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2) =>
        new(m1.Matrix.PerformOperation(m2.Matrix, TypeFunctions.Add));

    /// <summary>
    /// Generates a new <see cref="ComplexMatrixDecimal"/> from an existing one.
    /// </summary>
    /// <param name="m">The <see cref="ComplexMatrixDecimal"/> to copy from.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> with the values of the operand.</returns>
    public static ComplexMatrixDecimal operator +(ComplexMatrixDecimal m) =>
        new(m.Matrix);

    /// <summary>
    /// Subtracts a scalar value from each value of a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static ComplexMatrixDecimal operator -(ComplexMatrixDecimal m, GeneralComplexNumberDecimal scalar) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Sub(v, scalar)));

    /// <summary>
    /// Subtracts each value of a <see cref="ComplexMatrixDecimal"/> from a scalar.
    /// </summary>
    /// <param name="scalar">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="m">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand minus the right hand value.</returns>
    public static ComplexMatrixDecimal operator -(GeneralComplexNumberDecimal scalar, ComplexMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Sub(scalar, v)));

    /// <summary>
    /// Subtracts one <see cref="ComplexMatrixDecimal"/> from another.
    /// </summary>
    /// <param name="m1">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the left hand operand minus the right hand operand.</returns>
    /// <exception cref="ArgumentException">Thrown when the shapes of the two matrices differ.</exception>
    public static ComplexMatrixDecimal operator -(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2) =>
        new(m1.Matrix.PerformOperation(m2.Matrix, TypeFunctions.Sub));

    /// <summary>
    /// Negates all values in a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="m">The <see cref="ComplexMatrixDecimal"/> to negate.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> which is the negated operand.</returns>
    public static ComplexMatrixDecimal operator -(ComplexMatrixDecimal m) =>
        new(m.Matrix.PerformOperation(TypeFunctions.Neg));

    #endregion

    #region Multiplication/Division

    /// <summary>
    /// Multiplies each value of a <see cref="ComplexMatrixDecimal"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static ComplexMatrixDecimal operator *(ComplexMatrixDecimal m, GeneralComplexNumberDecimal scalar) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Mul(v, scalar)));

    /// <summary>
    /// Multiplies a scalar with each value of a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static ComplexMatrixDecimal operator *(GeneralComplexNumberDecimal m1, ComplexMatrixDecimal m2) =>
        new(m2.Matrix.PerformOperation(v => TypeFunctions.Mul(m1, v)));

    /// <summary>
    /// Multiplies each value of a <see cref="ComplexMatrixDecimal"/> with another <see cref="ComplexMatrixDecimal"/>.
    /// This uses the naive method.
    /// </summary>
    /// <param name="m1">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand multiplied by the right hand value.</returns>
    public static ComplexMatrixDecimal operator *(ComplexMatrixDecimal m1, ComplexMatrixDecimal m2) =>
        new(m1.Matrix.PerformMultiplyOperation(m2.Matrix, TypeFunctions));

    /// <summary>
    /// Divides a each value of a <see cref="ComplexMatrixDecimal"/> with a scalar.
    /// </summary>
    /// <param name="m">Left hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <param name="scalar">Right hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand divided by the right hand value.</returns>
    public static ComplexMatrixDecimal operator /(ComplexMatrixDecimal m, GeneralComplexNumberDecimal scalar) =>
        new(m.Matrix.PerformOperation(v => TypeFunctions.Div(v, scalar)));

    /// <summary>
    /// Divides a scalar with each value of a <see cref="ComplexMatrixDecimal"/>.
    /// </summary>
    /// <param name="m1">Left hand <see cref="GeneralComplexNumberDecimal"/>.</param>
    /// <param name="m2">Right hand <see cref="ComplexMatrixDecimal"/>.</param>
    /// <returns>A <see cref="ComplexMatrixDecimal"/> of the of the left hand operand divided by the right hand value.</returns>
    public static ComplexMatrixDecimal operator /(GeneralComplexNumberDecimal m1, ComplexMatrixDecimal m2) =>
        new(m2.Matrix.PerformOperation(v => TypeFunctions.Div(m1, v)));

    #endregion

    private static readonly GeneralComplexNumberDecimalFunctions TypeFunctions = new();
}
