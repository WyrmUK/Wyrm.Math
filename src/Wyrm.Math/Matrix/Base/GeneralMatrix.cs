using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Wyrm.Math.ComplexNumbers.Base;

namespace Wyrm.Math.Matrix.Base;

internal readonly struct GeneralMatrix<T> where T : struct
{
    public int Columns { get; }
    public int Rows => Values.Length == 0 ? 0 : Values.Length / Columns;
    public T[] Values { get; }

    public T this[int column, int row]
    {
        get => Values[row * Columns + column];
        set => Values[row * Columns + column] = value;
    }

    public GeneralMatrix(int columns, int rows)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(columns);
        ArgumentOutOfRangeException.ThrowIfNegative(rows);

        Columns = columns;
        Values = new T[rows * columns];
    }

    public GeneralMatrix(IEnumerable<IEnumerable<T>> values) : this(values.Max(v => (int?)v.Count()) ?? 0, values.Count())
    {
        var rowIndex = 0;
        foreach (var row in values)
        {
            var columnValues = row.ToArray();
            Array.Copy(columnValues, 0, Values, rowIndex++ * Columns, columnValues.Length);
        }
    }

    public GeneralMatrix(GeneralMatrix<T> matrix) : this(matrix.Columns, matrix.Rows)
    {
        Array.Copy(matrix.Values, Values, Values.Length);
    }

    public GeneralMatrix(int columns, T[] values)
    {
        Columns = columns;
        Values = new T[values.Length];
        Array.Copy(values, Values, values.Length);
    }

    public void Set(int column, int row, T value) => this[column, row] = value;

    public override int GetHashCode()
    {
        var hash = Columns.GetHashCode();
        foreach (var value in Values)
        {
            hash = hash * 16777619 + value.GetHashCode();
        }
        return hash;
    }

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        (obj is GeneralMatrix<T> compare) &&
        Equals(compare);

    public bool Equals(GeneralMatrix<T> matrix) =>
        (Columns == matrix.Columns) &&
        Values.SequenceEqual(matrix.Values);

    private const char MatrixStartChar = '(';
    private const char MatrixEndChar = ')';
    private const char MatrixRowStartChar = '(';
    private const char MatrixRowEndChar = ')';
    private const char MatrixRowValueSeparatorChar = '|';

    public override string ToString() => ToString(null);

    public string ToString(Func<T, string>? formatter)
    {
        var builder = new StringBuilder($"{MatrixStartChar}");
        for (var row = 0; row < Rows; ++row)
        {
            builder.Append(MatrixRowStartChar);
            for (var column = 0; column < Columns; ++column)
            {
                if (column > 0)
                {
                    builder.Append(MatrixRowValueSeparatorChar);
                }
                var value = this[column, row];
                builder.Append(formatter?.Invoke(value) ?? value.ToString());
            }
            builder.Append(MatrixRowEndChar);
        }
        builder.Append(MatrixEndChar);
        return builder.ToString();
    }

    internal delegate bool TryParseFunc(ReadOnlySpan<char> source, NumberStyles numberStyles, out T value);

    internal static bool TryParse(ReadOnlySpan<char> source, TryParseFunc tryParseFunc, out GeneralMatrix<T> matrix)
    {
        const char plusChar = '+';
        const NumberStyles numberStyles = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;

        matrix = new GeneralMatrix<T>([]);
        if (source.Length == 0) return false;

        if (source.Count(GeneralComplexNumber<T>.ComplexStartChar) == 1 && source.Count(GeneralComplexNumber<T>.ComplexEndChar) == 1 && source.Count(GeneralComplexNumber<T>.ImaginaryIdentifier) == 1)
        {
            if (!tryParseFunc(source, numberStyles, out var value)) return false;
            matrix = new GeneralMatrix<T>([[value]]);
            return true;
        }

        var values = new List<List<T>>();
        if (source[0] == MatrixStartChar && source[^1] == MatrixEndChar)
        {
            source = source[1..^1].Trim();

            while (source.Length > 0)
            {
                if (source[0] != MatrixRowStartChar || source[^1] != MatrixRowEndChar) return false;

                var end = EndOfRowPosition(source);
                if (end == 1) return false;

                var rowSource = source[1..end].Trim();
                if (rowSource.Length == 0) return false;

                var rowValues = new List<T>();

                while (rowSource.Length > 0)
                {
                    var numEnd = rowSource.IndexOf(MatrixRowValueSeparatorChar);
                    if (numEnd == 0 || numEnd == rowSource.Length - 1) return false;

                    var valueSource = (numEnd < 0
                        ? rowSource
                        : rowSource[0..numEnd]).Trim().TrimStart(plusChar);

                    if (!tryParseFunc(valueSource, numberStyles, out var value)) return false;
                    rowValues.Add(value);

                    if (numEnd < 0) break;
                    rowSource = rowSource[(numEnd + 1)..].TrimStart();
                }

                values.Add(rowValues);

                if (source.Length == end + 1) break;
                source = source[(end + 1)..].TrimStart();
            }
        }
        else
        {
            if (!tryParseFunc(source.TrimStart(plusChar), numberStyles, out var value)) return false;
            values.Add([value]);
        }

        matrix = new GeneralMatrix<T>(values);
        return true;
    }

    private static int EndOfRowPosition(ReadOnlySpan<char> source)
    {
        var end = 1;
        var inParenthesis = false;
        while (end < source.Length - 1)
        {
            if (source[end] == GeneralComplexNumber<T>.ComplexStartChar)
            {
                inParenthesis = true;
            }
            else if (source[end] == GeneralComplexNumber<T>.ComplexEndChar && inParenthesis)
            {
                inParenthesis = false;
            }
            else if (source[end] == MatrixRowEndChar) break;
            ++end;
        }
        return end;
    }

    public int CompareTo(GeneralMatrix<T> obj, Func<T, T, int> compareTo)
    {
        if (Columns != obj.Columns) return Columns.CompareTo(obj.Columns);
        if (Rows != obj.Rows) return Rows.CompareTo(obj.Rows);

        for (var index = 0; index < Values.Length; ++index)
        {
            var compare = compareTo(Values[index], obj.Values[index]);
            if (compare != 0) return compare;
        }

        return 0;
    }
}
