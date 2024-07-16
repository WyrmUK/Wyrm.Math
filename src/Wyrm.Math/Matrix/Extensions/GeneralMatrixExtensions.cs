using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class GeneralMatrixExtensions
{
    public static IEnumerable<IEnumerable<T>> ToEnumerableOfEnumerable<T>(this GeneralMatrix<T> matrix) where T : struct =>
        Enumerable.Range(0, matrix.Rows)
            .Select(row => row * matrix.Columns)
            .Select(rowIndex => matrix.Values[rowIndex..(rowIndex + matrix.Columns)]);

    public static GeneralMatrix<T> Transpose<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var newMatrix = new T[matrix.Values.Length];
        Parallel.For(0, matrix.Values.Length, index => newMatrix[index % matrix.Columns * matrix.Rows + (index / matrix.Columns)] = matrix.Values[index]);
        return new GeneralMatrix<T>(matrix.Rows, newMatrix);
    }

    public static T Trace<T>(this GeneralMatrix<T> matrix, Func<T, T, T> operationFunc) where T : struct
    {
        if (matrix.Columns != matrix.Rows) throw new ArgumentException("Matrix isn't square.");

        return Enumerable.Range(0, matrix.Columns)
            .Select(index => matrix.Values[index * matrix.Columns + index])
            .Aggregate(operationFunc);
    }

    public static GeneralMatrix<T> PerformOperation<T>(this GeneralMatrix<T> matrix1, GeneralMatrix<T> matrix2, Func<T, T, T> operationFunc) where T : struct
    {
        if (matrix1.Rows != matrix2.Rows) throw new ArgumentException("Rows mismatch.");
        if (matrix1.Columns != matrix2.Columns) throw new ArgumentException("Columns mismatch.");

        var newMatrix = new T[matrix1.Values.Length];
        Parallel.For(0, matrix1.Values.Length, index => newMatrix[index] = operationFunc(matrix1.Values[index], matrix2.Values[index]));
        return new GeneralMatrix<T>(matrix1.Columns, newMatrix);
    }

    public static GeneralMatrix<T> PerformOperation<T>(this GeneralMatrix<T> matrix, Func<T, T> operationFunc) where T : struct
    {
        var newMatrix = new T[matrix.Values.Length];
        Parallel.For(0, matrix.Values.Length, index => newMatrix[index] = operationFunc(matrix.Values[index]));
        return new GeneralMatrix<T>(matrix.Columns, newMatrix);
    }

    public static GeneralMatrix<T> PerformMultiplyOperation<T>(this GeneralMatrix<T> matrix1, GeneralMatrix<T> matrix2, Func<T, T, T> multiplyFunc, Func<T, T, T> addFunc) where T : struct
    {
        if (matrix1.Columns != matrix2.Rows) throw new ArgumentException("Columns on left hand matrix mismatch with Rows on right hand matrix.");

        var newMatrix = new T[matrix1.Rows * matrix2.Columns];
        Parallel.For(0, newMatrix.Length, index => newMatrix[index] =
            Enumerable.Range(0, matrix1.Columns)
                .Select(columnIndex => multiplyFunc(matrix1.Values[columnIndex + index / matrix2.Columns * matrix1.Columns], matrix2.Values[(index % matrix2.Columns) + columnIndex * matrix2.Columns]))
                .Aggregate(addFunc));
        return new GeneralMatrix<T>(matrix2.Columns, newMatrix);
    }
}
