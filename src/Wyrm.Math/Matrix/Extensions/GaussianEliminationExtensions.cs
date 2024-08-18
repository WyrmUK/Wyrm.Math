using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class GaussianEliminationExtensions
{
    internal static GeneralMatrix<T> TriangularForm<T>(this GeneralMatrix<T> matrix, Action swapFunc) where T : struct
    {
        if (typeof(T) != typeof(double) && typeof(T) != typeof(decimal)) throw new ArgumentException("Matrix type must be a floating point type.");

        var rowEchelonMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Values);

        while (!rowEchelonMatrix.IsInTriangularForm())
        {
            if (rowEchelonMatrix.SwapRows())
            {
                swapFunc();
                continue;
            }
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstant()) continue;
            throw new ArgumentException("Matrix can't be made triangular.");
        }

        return rowEchelonMatrix;
    }

    private static bool IsInTriangularForm<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        for (var row = 1; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < row; ++col)
            {
                if (matrix[col, row].IsNotZero()) return false;
            }
        }
        return true;
    }

    private static bool SwapRows<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var leadingZeroes = matrix.LeadingZeroes();
        var swapWith = -1;
        for (var row = 0; row < leadingZeroes.Count; ++row)
        {
            if (leadingZeroes[row] != row)
            {
                if (swapWith == -1)
                {
                    swapWith = row;
                }
                else if (leadingZeroes[row] < leadingZeroes[swapWith])
                {
                    var swapValues = new T[matrix.Columns];
                    Array.Copy(matrix.Values, swapWith * matrix.Columns, swapValues, 0, matrix.Columns);
                    Array.Copy(matrix.Values, row * matrix.Columns, matrix.Values, swapWith * matrix.Columns, matrix.Columns);
                    Array.Copy(swapValues, 0, matrix.Values, row * matrix.Columns, matrix.Columns);
                    return true;
                }
            }
        }
        return false;
    }

    private static bool AddProductOfRowMultipliedByConstant<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var leadingZeroes = matrix.LeadingZeroes();
        for (var row = 1; row < leadingZeroes.Count; ++row)
        {
            if (leadingZeroes[row] < row)
            {
                var factor = matrix[leadingZeroes[row], row].DividedBy(matrix[leadingZeroes[row], leadingZeroes[row]]);
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    var product = col == leadingZeroes[row]
                        ? matrix[col, row]
                        : factor.MultipliedBy(matrix[col, leadingZeroes[row]]);
                    matrix[col, row] = matrix[col, row].Subtract(product);
                }
                return true;
            }
        }
        return false;
    }

    private static List<int> LeadingZeroes<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var leadingZeroes = new List<int>();
        for (var row = 0; row < matrix.Rows; ++row)
        {
            var col = 0;
            while (col < matrix.Columns)
            {
                if (matrix[col, row].IsNotZero()) break;
                ++col;
            }
            leadingZeroes.Add(col);
        }
        return leadingZeroes;
    }

    private static bool IsNotZero<T>(this T value)
    {
        if (value is decimal decimalValue) return decimalValue != 0M;
        if (value is double doubleValue) return doubleValue != 0D;
        throw new NotImplementedException();
    }

    private static T DividedBy<T>(this T v1, T v2)
    {
        if (v1 is decimal decimalValue1 && v2 is decimal decimalValue2) return (T)Convert.ChangeType(decimalValue1 / decimalValue2, typeof(T));
        if (v1 is double doubleValue1 && v2 is double doubleValue2) return (T)Convert.ChangeType(doubleValue1 / doubleValue2, typeof(T));
        throw new NotImplementedException();
    }

    private static T MultipliedBy<T>(this T v1, T v2)
    {
        if (v1 is decimal decimalValue1 && v2 is decimal decimalValue2) return (T)Convert.ChangeType(decimalValue1 * decimalValue2, typeof(T));
        if (v1 is double doubleValue1 && v2 is double doubleValue2) return (T)Convert.ChangeType(doubleValue1 * doubleValue2, typeof(T));
        throw new NotImplementedException();
    }

    private static T Subtract<T>(this T v1, T v2)
    {
        if (v1 is decimal decimalValue1 && v2 is decimal decimalValue2) return (T)Convert.ChangeType(decimalValue1 - decimalValue2, typeof(T));
        if (v1 is double doubleValue1 && v2 is double doubleValue2) return (T)Convert.ChangeType(doubleValue1 - doubleValue2, typeof(T));
        throw new NotImplementedException();
    }
}
