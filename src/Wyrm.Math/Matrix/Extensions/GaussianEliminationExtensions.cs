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

    internal static GeneralMatrix<T> InverseForm<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        if (typeof(T) != typeof(double) && typeof(T) != typeof(decimal)) throw new ArgumentException("Matrix type must be a floating point type.");
        if (matrix.Columns != matrix.Rows) throw new ArgumentException("Matrix isn't square.");

        var rowEchelonMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Values);
        var inverseMatrix = rowEchelonMatrix.IdentityMatrix();

        while (!rowEchelonMatrix.IsInIdentityForm())
        {
            if (rowEchelonMatrix.SwapRows(inverseMatrix)) continue;
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstant(inverseMatrix)) continue;
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstantInverse(inverseMatrix)) continue;
            if (rowEchelonMatrix.MultiplyRowByConstant(inverseMatrix)) continue;
            throw new ArgumentException("Matrix can't be inverted.");
        }

        return inverseMatrix;
    }

    private static GeneralMatrix<T> IdentityMatrix<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var identityMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Rows);
        for (int index = 0; index < System.Math.Min(matrix.Columns, matrix.Rows); ++index)
        {
            identityMatrix[index, index] = (T)Convert.ChangeType(1, typeof(T));
        }
        return identityMatrix;
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

    private static bool IsInIdentityForm<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        for (var row = 0; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < matrix.Columns; ++col)
            {
                if (col == row)
                {
                    if (matrix[col, row].IsNotOne()) return false;
                }
                else if (matrix[col, row].IsNotZero()) return false;
            }
        }
        return true;
    }

    private static bool SwapRows<T>(this GeneralMatrix<T> matrix, GeneralMatrix<T>? augment = null) where T : struct
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
                    matrix.SwapRows(row, swapWith);
                    augment?.SwapRows(row, swapWith);
                    return true;
                }
            }
        }
        return false;
    }

    private static void SwapRows<T>(this GeneralMatrix<T> matrix, int row, int swapWith) where T : struct
    {
        var swapValues = new T[matrix.Columns];
        Array.Copy(matrix.Values, swapWith * matrix.Columns, swapValues, 0, matrix.Columns);
        Array.Copy(matrix.Values, row * matrix.Columns, matrix.Values, swapWith * matrix.Columns, matrix.Columns);
        Array.Copy(swapValues, 0, matrix.Values, row * matrix.Columns, matrix.Columns);
    }

    private static bool AddProductOfRowMultipliedByConstant<T>(this GeneralMatrix<T> matrix, GeneralMatrix<T>? augment = null) where T : struct
    {
        var leadingZeroes = matrix.LeadingZeroes();
        for (var row = 1; row < leadingZeroes.Count; ++row)
        {
            if (leadingZeroes[row] < row)
            {
                var valueColumn = leadingZeroes[row];
                if (!matrix[valueColumn, valueColumn].IsNotZero()) continue;
                var factor = matrix[valueColumn, row].DividedBy(matrix[valueColumn, valueColumn]);
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    var product = col == valueColumn
                        ? matrix[col, row]
                        : factor.MultipliedBy(matrix[col, valueColumn]);
                    matrix[col, row] = matrix[col, row].Subtract(product);

                    if (augment.HasValue && col < augment.Value.Columns)
                    {
                        product = factor.MultipliedBy(augment.Value[col, valueColumn]);
                        augment.Value.Set(col, row, augment.Value[col, row].Subtract(product));
                    }
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

    private static bool AddProductOfRowMultipliedByConstantInverse<T>(this GeneralMatrix<T> matrix, GeneralMatrix<T>? augment = null) where T : struct
    {
        var trailingZeroes = matrix.TrailingZeroes();
        for (var row = trailingZeroes.Count - 2; row >= 0; --row)
        {
            if (trailingZeroes[row] < trailingZeroes.Count - 1 - row)
            {
                var valueColumn = matrix.Columns - 1 - trailingZeroes[row];
                if (!matrix[valueColumn, valueColumn].IsNotZero()) continue;
                var factor = matrix[valueColumn, row].DividedBy(matrix[valueColumn, valueColumn]);
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    var product = col == valueColumn
                        ? matrix[col, row]
                        : factor.MultipliedBy(matrix[col, valueColumn]);
                    matrix[col, row] = matrix[col, row].Subtract(product);

                    if (augment.HasValue && col < augment.Value.Columns)
                    {
                        product = factor.MultipliedBy(augment.Value[col, valueColumn]);
                        augment.Value.Set(col, row, augment.Value[col, row].Subtract(product));
                    }
                }
                return true;
            }
        }
        return false;
    }

    private static List<int> TrailingZeroes<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        var trailingZeroes = new List<int>();
        for (var row = 0; row < matrix.Rows; ++row)
        {
            var col = 0;
            while (col < matrix.Columns)
            {
                if (matrix[matrix.Columns - 1 - col, row].IsNotZero()) break;
                ++col;
            }
            trailingZeroes.Add(col);
        }
        return trailingZeroes;
    }

    private static bool MultiplyRowByConstant<T>(this GeneralMatrix<T> matrix, GeneralMatrix<T>? augment = null) where T : struct
    {
        for (var row = 0; row < matrix.Rows; ++row)
        {
            if (!matrix[row, row].IsNotZero()) continue;
            if (!matrix[row, row].IsNotOne()) continue;

            var multiplier = matrix[row, row].IntoOne();

            for (var col = 0; col < matrix.Columns; ++col)
            {
                matrix[col, row] = matrix[col, row].MultipliedBy(multiplier);
                if (augment.HasValue && col < augment.Value.Columns)
                {
                    augment.Value.Set(col, row, augment.Value[col, row].MultipliedBy(multiplier));
                }
            }
            return true;
        }
        return false;
    }

    private static bool IsNotZero<T>(this T value)
    {
        if (value is decimal decimalValue) return decimalValue != 0M;
        if (value is double doubleValue) return doubleValue != 0D;
        throw new NotImplementedException();
    }

    private static bool IsNotOne<T>(this T value)
    {
        if (value is decimal decimalValue) return decimalValue != 1M;
        if (value is double doubleValue) return doubleValue != 1D;
        throw new NotImplementedException();
    }

    private static T IntoOne<T>(this T v)
    {
        if (v is decimal decimalValue) return (T)Convert.ChangeType(1M / decimalValue, typeof(T));
        if (v is double doubleValue) return (T)Convert.ChangeType(1D / doubleValue, typeof(T));
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
