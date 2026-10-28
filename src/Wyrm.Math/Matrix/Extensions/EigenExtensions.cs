using Wyrm.Math.Matrix.Base;

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
            2 => matrix.Quadratic(funcs),
            _ => matrix.QrAlgorithm(funcs)
        };

        return matrix.Rows switch
        {
            0 => [],
            1 => [new EigenValueVectorPair<T2, T3>(eigenvalues.First(), funcs.ComplexMatrix([[funcs.Cast(funcs.One())]])) ],
            _ => matrix.CayleyHamiltonVectors(eigenvalues, funcs)
        };
    }

    private static ICollection<T2> Quadratic<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        var b = matrix.Trace(funcs);
        var c = matrix.Determinant(funcs);
        var gap = funcs.Sqrt(funcs.Sub(funcs.Mul(b, b), funcs.Mul(funcs.Four(), c)));
        return
        [
            funcs.HalfOf(funcs.Add(b, funcs.Neg(gap))),
            funcs.HalfOf(funcs.Add(b, gap))
        ];
    }

    private static ICollection<T2> QrAlgorithm<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        // TODO
        throw new NotImplementedException();
    }

    private static ICollection<EigenValueVectorPair<T2, T3>> CayleyHamiltonVectors<T1, T2, T3>(this GeneralMatrix<T1> matrix, ICollection<T2> eigenvalues, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct
    {
        var distinctEigenvalues = eigenvalues.Distinct().OrderDescending().ToList();
        var eigenValueVectors = new List<EigenValueVectorPair<T2, T3>>();
        for (var index = 0; index < distinctEigenvalues.Count; ++index)
        {
            GeneralMatrix<T2>? matrixProduct = null;
            for (var val = 0; val < distinctEigenvalues.Count; ++val)
            {
                if (val == index) continue;

                if (matrixProduct.HasValue)
                {
                    funcs.Mul(matrixProduct.Value, CayleyHamiltonTerm(matrix, distinctEigenvalues[val], funcs));
                }
                else
                {
                    matrixProduct = CayleyHamiltonTerm(matrix, distinctEigenvalues[val], funcs);
                }
            }
            eigenValueVectors.Add(new EigenValueVectorPair<T2, T3>(distinctEigenvalues[index], matrixProduct!.Value.FirstNonZeroColumn(funcs)));
        }
        return eigenValueVectors;
    }

    private static GeneralMatrix<T2> CayleyHamiltonTerm<T1, T2, T3>(this GeneralMatrix<T1> matrix, T2 eigenvalue, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct =>
        funcs.Sub(matrix, funcs.Mul(eigenvalue, matrix.IdentityMatrix(funcs)));

    private static T3 FirstNonZeroColumn<T1, T2, T3>(this GeneralMatrix<T2> matrix, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct
    {
        T2[]? first = null;
        for (var column = 0; column < matrix.Columns; ++column)
        {
            var values = new T2[matrix.Rows];
            for (var row = 0; row < matrix.Rows; ++row)
            {
                values[row] = matrix[column, row];
            }

            if (first != null)
            {
                first = values;
            }

            if (values.All(funcs.Eqz)) continue;

            return funcs.ComplexMatrix(values.Select(v => new[] { v }));
        }
        return first!;
    }
}
