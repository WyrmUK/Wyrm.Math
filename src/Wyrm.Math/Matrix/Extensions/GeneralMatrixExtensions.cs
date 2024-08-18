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

    public static T Trace<T>(this GeneralMatrix<T> matrix, Func<T, T, T> additionFunc) where T : struct
    {
        if (matrix.Columns != matrix.Rows) throw new ArgumentException("Matrix isn't square.");

        return Enumerable.Range(0, matrix.Columns)
            .Select(index => matrix.Values[index * matrix.Columns + index])
            .Aggregate(additionFunc);
    }

    public static double Determinant<T>(this GeneralMatrix<T> matrix, Func<T, double> castFunc) where T : struct
    {
        if (matrix.Columns != matrix.Rows) throw new ArgumentException("Matrix isn't square.");

        var negate = false;
        var triangularForm = matrix.TriangularForm(castFunc, () => negate = !negate);

        var determinantAbs = Enumerable.Range(0, triangularForm.Columns)
            .Select(index => triangularForm.Values[index * triangularForm.Columns + index])
            .Aggregate((v1, v2) => v1 * v2);
        return negate ? - determinantAbs : determinantAbs;
    }

    public static int Rank<T>(this GeneralMatrix<T> matrix, Func<T, double> castFunc) where T : struct
    {
        var triangularForm = matrix.TriangularForm(castFunc, () => {});

        return triangularForm.ToEnumerableOfEnumerable()
            .Where(r => r.Any(c => c != 0.0))
            .Count();
    }

    public static int Nullity<T>(this GeneralMatrix<T> matrix, Func<T, double> castFunc) where T : struct
    {
        return matrix.Columns - matrix.Rank(castFunc);
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

    public static GeneralMatrix<T> PerformMultiplyOperation<T>(this GeneralMatrix<T> matrix1, GeneralMatrix<T> matrix2, Func<T, T, T> multiplyFunc, Func<T, T, T> additionFunc) where T : struct
    {
        if (matrix1.Columns != matrix2.Rows) throw new ArgumentException("Columns on left hand matrix mismatch with Rows on right hand matrix.");

        var newMatrix = new T[matrix1.Rows * matrix2.Columns];
        Parallel.For(0, newMatrix.Length, index => newMatrix[index] =
            Enumerable.Range(0, matrix1.Columns)
                .Select(columnIndex => multiplyFunc(matrix1.Values[columnIndex + index / matrix2.Columns * matrix1.Columns], matrix2.Values[(index % matrix2.Columns) + columnIndex * matrix2.Columns]))
                .Aggregate(additionFunc));
        return new GeneralMatrix<T>(matrix2.Columns, newMatrix);
    }
}
