using Wyrm.Math.Formula.Base;
using Wyrm.Math.Matrix.Base;
using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Matrix.Extensions;

internal static class EigenExtensions
{
    public static ICollection<EigenValueVectorPair<T2, T3>> Eigenvectors<T1, T2, T3>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct
    {
        if (!matrix.IsSquare()) throw new ArgumentException("Matrix isn't square.");

        var eigenvalues = matrix.Rows switch
        {
            0 => [],
            1 => [funcs.Cast(matrix[0, 0])],
            2 => matrix.QuadraticAlgorithm(funcs),
            3 => matrix.CubicAlgorithm(funcs),
            _ => matrix.QrAlgorithm(funcs)
        };

        return matrix.Rows switch
        {
            0 => [],
            1 => [new EigenValueVectorPair<T2, T3>(eigenvalues.First(), funcs.ComplexMatrix(new GeneralMatrix<T2>([[funcs.Cast(funcs.One())]]))) ],
            _ => matrix.CayleyHamiltonVectors(eigenvalues, funcs)
        };
    }

    private static ICollection<T2> QuadraticAlgorithm<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct =>
        Quadratic<T1, T2>.Roots(funcs.One(), funcs.Neg(matrix.Trace(funcs)), matrix.Determinant(funcs), funcs);

    private static ICollection<T2> CubicAlgorithm<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        var trace = matrix.Trace(funcs);
        var sqrC = funcs.Mul(funcs.NegHalf(), funcs.Sub(funcs.Mul(trace, trace), funcs.Mul(matrix, matrix).Trace(funcs)));
        return Cubic<T1, T2>.Roots(funcs.NegOne(), trace, sqrC, matrix.Determinant(funcs), funcs);
    }

    private static List<T2> QrAlgorithm<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        var matrixB = funcs.CopyAs(matrix);

        for (var iteration = 0; iteration < 1000; ++iteration)
        {
            if (matrixB.IsConvergent(funcs)) break;

            var (Q, R) = matrixB.QrDecomposition(funcs);
            matrixB = funcs.Mul(R, Q);
        }

        return [.. Enumerable.Range(0, matrix.Columns).Select(x => matrixB[x, x])];
    }

    private static bool IsConvergent<T1, T2>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        for (var row = 0; row < matrix.Rows; ++row)
        {
            for (var col = 0; col < matrix.Columns; ++col)
            {
                if (col == row) continue;
                if (funcs.AboveTolerance(matrix[col, row])) return false;
            }
        }
        return true;
    }

    private static (GeneralMatrix<T2> Q, GeneralMatrix<T2> R) QrDecomposition<T1, T2>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        var q = new GeneralMatrix<T2>(matrix.Columns, [..matrix.Values]);

        for (var col = 1; col < matrix.Columns; ++col)
        {
            var u = q.Column(col);
            var v = q.Column(col);

            for (var qCol = col - 1; qCol >= 0; --qCol)
            {
                var uk = q.Column(qCol);
                var p = funcs.Mul(uk, funcs.Div(uk.DotProduct(v, funcs), uk.DotProduct(uk, funcs)));
                u = funcs.Sub(u, p);
            }

            for (var row = 0; row < matrix.Rows; ++row)
            {
                q[col, row] = u[0, row];
            }
        }

        for (var qCol = 0; qCol < matrix.Columns; ++qCol)
        {
            var u = q.Column(qCol);
            var magnitude = funcs.Sqrt(u.DotProduct(u, funcs));

            for (var row = 0; row < matrix.Rows; ++row)
            {
                q[qCol, row] = funcs.Div(u[0, row], magnitude);
            }
        }

        var r = funcs.Mul(q.Transpose(), matrix);

        return (q, r);
    }

    private static T2 DotProduct<T1, T2>(this GeneralMatrix<T2> matrix1, GeneralMatrix<T2> matrix2, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        return Enumerable.Range(0, matrix1.Values.Length)
            .Select(x => funcs.Mul(matrix1.Values[x], matrix2.Values[x]))
            .Aggregate(funcs.Add);
    }

    private static List<EigenValueVectorPair<T2, T3>> CayleyHamiltonVectors<T1, T2, T3>(this GeneralMatrix<T1> matrix, ICollection<T2> eigenvalues, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct
    {
        var eigenValueVectors = new List<EigenValueVectorPair<T2, T3>>();
        for (var index = 0; index < eigenvalues.Count; ++index)
        {
            GeneralMatrix<T2>? matrixProduct = null;
            for (var val = 0; val < eigenvalues.Count; ++val)
            {
                if (val == index) continue;

                if (matrixProduct.HasValue)
                {
                    funcs.Mul(matrixProduct.Value, CayleyHamiltonTerm(matrix, eigenvalues.ElementAt(val), funcs));
                }
                else
                {
                    matrixProduct = CayleyHamiltonTerm(matrix, eigenvalues.ElementAt(val), funcs);
                }
            }
            eigenValueVectors.Add(new EigenValueVectorPair<T2, T3>(eigenvalues.ElementAt(index), funcs.ComplexMatrix(matrixProduct!.Value.FirstNonZeroColumn(funcs).Normalise(funcs))));
        }
        return eigenValueVectors;
    }

    private static GeneralMatrix<T2> CayleyHamiltonTerm<T1, T2>(this GeneralMatrix<T1> matrix, T2 eigenvalue, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct =>
        funcs.Sub(matrix, funcs.Mul(eigenvalue, matrix.IdentityMatrix(funcs)));

    private static GeneralMatrix<T2> FirstNonZeroColumn<T1, T2>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        for (var col = 0; col < matrix.Columns; ++col)
        {
            var column = matrix.Column(col);
            if (column.Values.All(funcs.Eqz)) continue;

            return column;
        }
        return matrix.Column(0);
    }

    private static GeneralMatrix<T2> Column<T2>(this GeneralMatrix<T2> matrix, int column) where T2 : struct
    {
        var values = Enumerable.Range(0, matrix.Rows)
            .Select(r => matrix[column, r])
            .ToArray();
        return new GeneralMatrix<T2>(1, values);
    }

    private static GeneralMatrix<T2> Normalise<T1, T2>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        if (matrix.Values.All(funcs.IsInteger))
        {
            var intDivisor = matrix.LargestDivisor(funcs);
            if (funcs.Eqz(intDivisor)) return matrix;

            if (funcs.Ltz(matrix.Values[matrix.Rows - 1]))
            {
                intDivisor = funcs.Neg(intDivisor);
            }
            var normalised = funcs.Div(matrix, intDivisor);
            return funcs.Eq1(intDivisor)
                ? matrix
                : funcs.Div(matrix, intDivisor);
        }

        var divisor = matrix.Values[matrix.Rows - 1];
        return funcs.Eqz(divisor)
            ? matrix
            : funcs.Div(matrix, divisor);
    }

    private static T1 LargestDivisor<T1, T2>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        var divisors = matrix.Values.Select(funcs.Abs).Distinct().ToList();
        if (divisors.Count == 1) return divisors[0];
        while (divisors.All(funcs.Nez))
        {
            var index = divisors.IndexOf(divisors.Min());
            var divisor = divisors[index];
            for (var val = 0; val < divisors.Count; ++val)
            {
                if (val == index) continue;
                divisors[val] = funcs.Mod(divisors[val], divisor);
            }
        }
        return divisors.First(funcs.Nez);
    }
}
