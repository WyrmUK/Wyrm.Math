using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class EigenExtensions
{
    public static ICollection<EigenValueVectorPair<T2, T3>> Eigenvectors<T1, T2, T3>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2, T3> funcs) where T1 : struct where T2 : struct where T3 : struct
    {
        if (!matrix.IsSquare()) throw new ArgumentException("Matrix isn't square.");

        var eigenvalues = matrix.Eigenvalues(funcs);

        return matrix.Rows switch
        {
            0 => [],
            1 => [new EigenValueVectorPair<T2, T3>(eigenvalues.First(), funcs.ComplexMatrix([[funcs.Cast(funcs.One())]])) ],
            2 => matrix.QuadraticVector<T1, T2, T3>(eigenvalues),
            _ => matrix.QrAlgorithmVector<T1, T2, T3>(eigenvalues)
        };
    }

    private static ICollection<T2> Eigenvalues<T1, T2>(this GeneralMatrix<T1> matrix, ITypeFunctions<T1, T2> funcs) where T1 : struct where T2 : struct
    {
        if (!matrix.IsSquare()) throw new ArgumentException("Matrix isn't square.");

        return matrix.Rows switch
        {
            0 => [],
            1 => [funcs.Cast(matrix[0, 0])],
            2 => matrix.Quadratic(funcs),
            _ => matrix.QrAlgorithm(funcs)
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

    private static ICollection<EigenValueVectorPair<T2, T3>> QuadraticVector<T1, T2, T3>(this GeneralMatrix<T1> matrix, ICollection<T2> eigenvalues) where T1 : struct where T2 : struct where T3 : struct
    {
        // TODO: .OrderDescending()
        // TODO
        throw new NotImplementedException();
    }

    private static ICollection<EigenValueVectorPair<T2, T3>> QrAlgorithmVector<T1, T2, T3>(this GeneralMatrix<T1> matrix, ICollection<T2> eigenvalues) where T1 : struct where T2 : struct where T3 : struct
    {
        // TODO: .OrderDescending()
        // TODO
        throw new NotImplementedException();
    }
}
