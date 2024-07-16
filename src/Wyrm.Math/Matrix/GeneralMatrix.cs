namespace Wyrm.Math.Matrix;

internal class GeneralMatrix<T> : IGeneralMatrix<T> where T : struct
{
    private readonly int _columns;
    private readonly T[] _matrix;

    public GeneralMatrix(int columns, int rows)
    {
        if (columns < 0) throw new ArgumentOutOfRangeException(nameof(columns));
        if (rows < 0) throw new ArgumentOutOfRangeException(nameof(rows));
        _columns = columns;
        _matrix = new T[rows * columns];
    }

    public GeneralMatrix(IEnumerable<IEnumerable<T>> values) : this(values.Max(v => v.Count()), values.Count())
    {
        var rowIndex = 0;
        foreach (var row in values)
        {
            Array.Copy(row.ToArray(), 0, _matrix, rowIndex++ * _columns, _columns);
        }
    }

    public GeneralMatrix(GeneralMatrix<T> matrix) : this(matrix.Columns, matrix.Rows)
    {
        Array.Copy(matrix._matrix, _matrix, _matrix.Length);
    }

    private GeneralMatrix(int columns, T[] values)
    {
        _columns = columns;
        _matrix = values;
    }

    public int Rows => _matrix.Length / _columns;

    public int Columns => _columns;

    public IEnumerable<IEnumerable<T>> Values =>
        Enumerable.Range(0, Rows)
            .Select(row => row * _columns)
            .Select(rowIndex => _matrix[rowIndex..(rowIndex + _columns)]);

    public T this[int column, int row]
    {
        get => _matrix[row * _columns + column];
        set => _matrix[row * _columns + column] = value;
    }

    internal GeneralMatrix<T> Transpose()
    {
        var newMatrix = new T[_matrix.Length];
        Parallel.For(0, _matrix.Length, index => newMatrix[index % Columns * Rows + (index / Columns)] = _matrix[index]);
        return new GeneralMatrix<T>(Rows, newMatrix);
    }

    internal T Trace(Func<T, T, T> operationFunc)
    {
        if (Columns != Rows) throw new ArgumentException("Matrix isn't square.");

        return Enumerable.Range(0, Columns)
            .Select(index => _matrix[index * Columns + index])
            .Aggregate(operationFunc);
    }

    internal GeneralMatrix<T> PerformOperation(GeneralMatrix<T> matrix, Func<T, T, T> operationFunc)
    {
        if (Rows != matrix.Rows) throw new ArgumentException("Rows mismatch.");
        if (Columns != matrix.Columns) throw new ArgumentException("Columns mismatch.");

        var newMatrix = new T[Rows * Columns];
        Parallel.For(0, _matrix.Length, index => newMatrix[index] = operationFunc(_matrix[index], matrix._matrix[index]));
        return new GeneralMatrix<T>(Columns, newMatrix);
    }

    internal GeneralMatrix<T> PerformOperation(Func<T, T> operationFunc)
    {
        var newMatrix = new T[Rows * Columns];
        Parallel.For(0, _matrix.Length, index => newMatrix[index] = operationFunc(_matrix[index]));
        return new GeneralMatrix<T>(Columns, newMatrix);
    }

    internal GeneralMatrix<T> PerformMultiplyOperation(GeneralMatrix<T> matrix, Func<T, T, T> multiplyFunc, Func<T, T, T> addFunc)
    {
        if (Columns != matrix.Rows) throw new ArgumentException("Columns on left hand matrix mismatch with Rows on right hand matrix.");

        var newMatrix = new T[Rows * matrix.Columns];
        Parallel.For(0, newMatrix.Length, index => newMatrix[index] =
            Enumerable.Range(0, Columns)
                .Select(columnIndex => multiplyFunc(_matrix[columnIndex + index / matrix.Columns * Columns], matrix._matrix[(index % matrix.Columns) + columnIndex * matrix.Columns]))
                .Aggregate(addFunc));
        return new GeneralMatrix<T>(matrix.Columns, newMatrix);
    }
}
