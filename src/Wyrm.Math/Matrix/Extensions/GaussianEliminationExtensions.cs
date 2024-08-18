using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class GaussianEliminationExtensions
{
    internal static GeneralMatrix<double> TriangularForm<T>(this GeneralMatrix<T> matrix, Func<T, double> castFunc, Action swapFunc) where T : struct
    {
        var rowEchelonMatrix = new GeneralMatrix<double>(matrix.Columns, matrix.Values.Select(v => castFunc(v)).ToArray());

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

    private static bool IsInTriangularForm(this GeneralMatrix<double> matrix)
    {
        for (var row = 1; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < row; ++col)
            {
                if (matrix[col, row] != 0.0) return false;
            }
        }
        return true;
    }

    private static bool SwapRows(this GeneralMatrix<double> matrix)
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
                    var swapValues = new double[matrix.Columns];
                    Array.Copy(matrix.Values, swapWith * matrix.Columns, swapValues, 0, matrix.Columns);
                    Array.Copy(matrix.Values, row * matrix.Columns, matrix.Values, swapWith * matrix.Columns, matrix.Columns);
                    Array.Copy(swapValues, 0, matrix.Values, row * matrix.Columns, matrix.Columns);
                    return true;
                }
            }
        }
        return false;
    }

    private static bool AddProductOfRowMultipliedByConstant(this GeneralMatrix<double> matrix)
    {
        var leadingZeroes = matrix.LeadingZeroes();
        for (var row = 1; row < leadingZeroes.Count; ++row)
        {
            if (leadingZeroes[row] < row)
            {
                var factor = matrix[leadingZeroes[row], row] / matrix[leadingZeroes[row], leadingZeroes[row]];
                for (var col = 0; col < matrix.Columns; ++col)
                {
                    matrix[col, row] -= col == leadingZeroes[row]
                        ? matrix[col, row]
                        : factor * matrix[col, leadingZeroes[row]];
                }
                return true;
            }
        }
        return false;
    }

    private static List<int> LeadingZeroes(this GeneralMatrix<double> matrix)
    {
        var leadingZeroes = new List<int>();
        for (var row = 0; row < matrix.Rows; ++row)
        {
            var col = 0;
            while (col < matrix.Columns)
            {
                if (matrix[col, row] != 0.0) break;
                ++col;
            }
            leadingZeroes.Add(col);
        }
        return leadingZeroes;
    }
}
