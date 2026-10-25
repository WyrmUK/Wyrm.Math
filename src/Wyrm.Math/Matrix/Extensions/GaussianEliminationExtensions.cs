using Wyrm.Math.ComplexNumbers;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class GaussianEliminationExtensions
{
    internal static GeneralMatrix<T> TriangularForm<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs, Action swapFunc) where T : struct
    {
        if (typeof(T) != typeof(double) && typeof(T) != typeof(decimal) && typeof(T) != typeof(GeneralComplexNumberDouble) && typeof(T) != typeof(GeneralComplexNumberDecimal)) throw new ArgumentException("Matrix type must be a floating point or complex number type.");

        var rowEchelonMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Values);

        while (!rowEchelonMatrix.IsInTriangularForm(funcs))
        {
            if (rowEchelonMatrix.SwapRows(funcs))
            {
                swapFunc();
                continue;
            }
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstant(funcs)) continue;
            throw new ArgumentException("Matrix can't be made triangular.");
        }

        return rowEchelonMatrix;
    }

    internal static GeneralMatrix<T> InverseForm<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        if (typeof(T) != typeof(double) && typeof(T) != typeof(decimal) && typeof(T) != typeof(GeneralComplexNumberDouble) && typeof(T) != typeof(GeneralComplexNumberDecimal)) throw new ArgumentException("Matrix type must be a floating point or complex number type.");
        if (matrix.Columns != matrix.Rows) throw new ArgumentException("Matrix isn't square.");

        var rowEchelonMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Values);
        var inverseMatrix = rowEchelonMatrix.IdentityMatrix(funcs);

        while (!rowEchelonMatrix.IsInIdentityForm(funcs))
        {
            if (rowEchelonMatrix.SwapRows(funcs, inverseMatrix)) continue;
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstant(funcs, inverseMatrix)) continue;
            if (rowEchelonMatrix.AddProductOfRowMultipliedByConstantInverse(funcs, inverseMatrix)) continue;
            if (rowEchelonMatrix.MultiplyRowByConstant(funcs, inverseMatrix)) continue;
            throw new ArgumentException("Matrix can't be inverted.");
        }

        return inverseMatrix;
    }

    private static GeneralMatrix<T> IdentityMatrix<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        var identityMatrix = new GeneralMatrix<T>(matrix.Columns, matrix.Rows);
        for (int index = 0; index < System.Math.Min(matrix.Columns, matrix.Rows); ++index)
        {
            identityMatrix[index, index] = funcs.One();
        }
        return identityMatrix;
    }

    private static bool IsInTriangularForm<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        for (var row = 1; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < row; ++col)
            {
                if (funcs.Nez(matrix[col, row])) return false;
            }
        }
        return true;
    }

    private static bool IsInIdentityForm<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        for (var row = 0; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < matrix.Columns; ++col)
            {
                if (col == row)
                {
                    if (funcs.Ne1(matrix[col, row])) return false;
                }
                else if (funcs.Nez(matrix[col, row])) return false;
            }
        }
        return true;
    }

    private static bool SwapRows<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs, GeneralMatrix<T>? augment = null) where T : struct
    {
        var leadingZeroes = matrix.LeadingZeroes(funcs);
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

    private static bool AddProductOfRowMultipliedByConstant<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs, GeneralMatrix<T>? augment = null) where T : struct
    {
        var leadingZeroes = matrix.LeadingZeroes(funcs);
        for (var row = 1; row < leadingZeroes.Count; ++row)
        {
            if (leadingZeroes[row] < row)
            {
                var valueColumn = leadingZeroes[row];
                if (funcs.Eqz(matrix[valueColumn, valueColumn])) continue;
                var factor = funcs.Div(matrix[valueColumn, row], matrix[valueColumn, valueColumn]);
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    var product = col == valueColumn
                        ? matrix[col, row]
                        : funcs.Mul(factor, matrix[col, valueColumn]);
                    matrix[col, row] = funcs.Sub(matrix[col, row], product);

                    if (augment.HasValue && col < augment.Value.Columns)
                    {
                        product = funcs.Mul(factor, augment.Value[col, valueColumn]);
                        augment.Value.Set(col, row, funcs.Sub(augment.Value[col, row], product));
                    }
                }
                return true;
            }
        }
        return false;
    }

    private static List<int> LeadingZeroes<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        var leadingZeroes = new List<int>();
        for (var row = 0; row < matrix.Rows; ++row)
        {
            var col = 0;
            while (col < matrix.Columns)
            {
                if (funcs.Nez(matrix[col, row])) break;
                ++col;
            }
            leadingZeroes.Add(col);
        }
        return leadingZeroes;
    }

    private static bool AddProductOfRowMultipliedByConstantInverse<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs, GeneralMatrix<T>? augment = null) where T : struct
    {
        var trailingZeroes = matrix.TrailingZeroes(funcs);
        for (var row = trailingZeroes.Count - 2; row >= 0; --row)
        {
            if (trailingZeroes[row] < trailingZeroes.Count - 1 - row)
            {
                var valueColumn = matrix.Columns - 1 - trailingZeroes[row];
                if (funcs.Eqz(matrix[valueColumn, valueColumn])) continue;
                var factor = funcs.Div(matrix[valueColumn, row], matrix[valueColumn, valueColumn]);
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    var product = col == valueColumn
                        ? matrix[col, row]
                        : funcs.Mul(factor, matrix[col, valueColumn]);
                    matrix[col, row] = funcs.Sub(matrix[col, row], product);

                    if (augment.HasValue && col < augment.Value.Columns)
                    {
                        product = funcs.Mul(factor, augment.Value[col, valueColumn]);
                        augment.Value.Set(col, row, funcs.Sub(augment.Value[col, row], product));
                    }
                }
                return true;
            }
        }
        return false;
    }

    private static List<int> TrailingZeroes<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs) where T : struct
    {
        var trailingZeroes = new List<int>();
        for (var row = 0; row < matrix.Rows; ++row)
        {
            var col = 0;
            while (col < matrix.Columns)
            {
                if (funcs.Nez(matrix[matrix.Columns - 1 - col, row])) break;
                ++col;
            }
            trailingZeroes.Add(col);
        }
        return trailingZeroes;
    }

    private static bool MultiplyRowByConstant<T>(this GeneralMatrix<T> matrix, ITypeFunctions<T> funcs, GeneralMatrix<T>? augment = null) where T : struct
    {
        for (var row = 0; row < matrix.Rows; ++row)
        {
            if (funcs.Eqz(matrix[row, row])) continue;
            if (funcs.Eq1(matrix[row, row])) continue;

            var multiplier = funcs.Div(funcs.One(), matrix[row, row]);

            for (var col = 0; col < matrix.Columns; ++col)
            {
                matrix[col, row] = funcs.Mul(matrix[col, row], multiplier);
                if (augment.HasValue && col < augment.Value.Columns)
                {
                    augment.Value.Set(col, row, funcs.Mul(augment.Value[col, row], multiplier));
                }
            }
            return true;
        }
        return false;
    }
    /*
    private static T OneValue<T>()
    {
        if (typeof(T) == typeof(decimal)) return (T)Convert.ChangeType(1M, typeof(decimal));
        if (typeof(T) == typeof(double)) return (T)Convert.ChangeType(1.0, typeof(double));
        if (typeof(T) == typeof(GeneralComplexNumberDecimal)) return (T)Convert.ChangeType((GeneralComplexNumberDouble)1M, typeof(GeneralComplexNumberDecimal));
        if (typeof(T) == typeof(GeneralComplexNumberDouble)) return (T)Convert.ChangeType((GeneralComplexNumberDouble)1.0, typeof(GeneralComplexNumberDouble));
        throw new NotImplementedException();
    }

    private static bool IsNotZero<T>(this T value)
    {
        if (value is decimal decimalValue) return decimalValue != 0M;
        if (value is double doubleValue) return doubleValue != 0D;
        if (value is GeneralComplexNumberDecimal decimalComplex) return decimalComplex.Real != 0M || decimalComplex.Imaginary != 0M;
        if (value is GeneralComplexNumberDouble doubleComplex) return doubleComplex.Real != 0.0 || doubleComplex.Imaginary != 0.0;
        throw new NotImplementedException();
    }

    private static bool IsNotOne<T>(this T value)
    {
        if (value is decimal decimalValue) return decimalValue != 1M;
        if (value is double doubleValue) return doubleValue != 1D;
        if (value is GeneralComplexNumberDecimal decimalComplex) return decimalComplex.Real != 1M || decimalComplex.Imaginary != 0M;
        if (value is GeneralComplexNumberDouble doubleComplex) return doubleComplex.Real != 1.0 || doubleComplex.Imaginary != 0.0;
        throw new NotImplementedException();
    }

    private static T IntoOne<T>(this T v)
    {
        if (v is decimal decimalValue) return (T)Convert.ChangeType(OneValue<decimal>() / decimalValue, typeof(T));
        if (v is double doubleValue) return (T)Convert.ChangeType(OneValue<double>() / doubleValue, typeof(T));
        if (v is GeneralComplexNumberDecimal decimalComplex) return (T)Convert.ChangeType(OneValue<GeneralComplexNumberDecimal>() / decimalComplex, typeof(T));
        if (v is GeneralComplexNumberDouble doubleComplex) return (T)Convert.ChangeType(OneValue<GeneralComplexNumberDouble>() / doubleComplex, typeof(T));
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
    */
}
