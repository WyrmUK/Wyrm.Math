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
}
