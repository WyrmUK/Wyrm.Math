using System.Diagnostics.CodeAnalysis;
using System.Text;

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
        if (columns < 0) throw new ArgumentOutOfRangeException(nameof(columns));
        if (rows < 0) throw new ArgumentOutOfRangeException(nameof(rows));

        Columns = columns;
        Values = new T[rows * columns];
    }

    public GeneralMatrix(IEnumerable<IEnumerable<T>> values) : this(values.Max(v => v.Count()), values.Count())
    {
        var rowIndex = 0;
        foreach (var row in values)
        {
            Array.Copy(row.ToArray(), 0, Values, rowIndex++ * Columns, Columns);
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
        (Columns == compare.Columns) &&
        Values.SequenceEqual(compare.Values);

    public override string ToString()
    {
        var stringValues = StringValues(out var leftWidths, out var rightWidths);
        var builder = new StringBuilder();
        for (var index = 0; index < stringValues.Count; ++index)
        {
            var column = index % Columns;
            builder.Append(column == 0 ? (index == 0 ? "( " : "\r\n( ") : string.Empty);
            builder.Append(stringValues[index][0].PadLeft(leftWidths[column]));
            if (rightWidths[column] > 0)
            {
                builder.Append('.');
                builder.Append((stringValues[index].Length > 1
                    ? stringValues[index][1]
                    : "0").PadRight(rightWidths[column]));
            }
            builder.Append(column == (Columns - 1) ? " )" : ", ");
        }
        return builder.ToString();
    }

    private static readonly char[] DecimalPointSplit = new[] { '.' };

    private List<string[]> StringValues(out List<int> leftWidths, out List<int> rightWidths)
    {
        leftWidths = new int[Columns].ToList();
        rightWidths = new int[Columns].ToList();
        var stringList = new List<string[]>(Values.Length);
        for (var index = 0; index < Values.Length; ++index)
        {
            stringList.Add((Values[index].ToString() ?? string.Empty).Split(DecimalPointSplit, 2));
            var parts = stringList[index].Select(p => p.Length).ToArray();
            if (parts[0] > leftWidths[index % Columns])
            {
                leftWidths[index % Columns] = parts[0];
            }
            if (parts.Length > 1 && parts[1] > rightWidths[index % Columns])
            {
                rightWidths[index % Columns] = parts[1];
            }
        }
        return stringList;
    }
}
