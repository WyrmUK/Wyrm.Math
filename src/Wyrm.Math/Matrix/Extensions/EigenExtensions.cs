using Wyrm.Math.ComplexNumbers.Base;
using Wyrm.Math.Matrix.Base;

namespace Wyrm.Math.Matrix.Extensions;

internal static class EigenExtensions
{
    public static ICollection<GeneralComplexNumber<T>> Eigenvalues<T>(this GeneralMatrix<T> matrix, Func<T, T, T> additionFunc, Func<T, T, T> multiplyFunc, Func<T, T> negateFunc, Func<T, T> minusFourTimesFunc, Func<T, GeneralComplexNumber<T>> sqrtiFunc, Func<T, T> halfFunc, Func<T> zeroFunc) where T : struct
    {
        if (!matrix.IsSquare()) throw new ArgumentException("Matrix isn't square.");

        return matrix.Rows switch
        {
            0 => [],
            1 => [new GeneralComplexNumber<T>(matrix[0, 0], zeroFunc())],
            2 => matrix.Quadratic(additionFunc, multiplyFunc, negateFunc, minusFourTimesFunc, sqrtiFunc, halfFunc),
            _ => matrix.QrAlgorithm()
        };
    }

    private static ICollection<GeneralComplexNumber<T>> Quadratic<T>(this GeneralMatrix<T> matrix, Func<T, T, T> additionFunc, Func<T, T, T> multiplyFunc, Func<T, T> negateFunc, Func<T, T> minusFourTimesFunc, Func<T, GeneralComplexNumber<T>> sqrtiFunc, Func<T, T> halfFunc) where T : struct
    {
        var b = matrix.Trace(additionFunc);
        var c = matrix.Determinant(multiplyFunc, negateFunc);
        var gap = sqrtFunc(additionFunc(multiplyFunc(b, b), minusFourTimesFunc(c)));
        return
        [
            halfFunc(additionFunc(b, negateFunc(gap))),
            halfFunc(additionFunc(b, gap))
        ];
    }

    private static ICollection<GeneralComplexNumber<T>> QrAlgorithm<T>(this GeneralMatrix<T> matrix) where T : struct
    {
        // TODO
        throw new NotImplementedException();
    }

    public static ICollection<GeneralMatrix<T>> Eigenvectors<T>(this GeneralMatrix<T> matrix, ICollection<T> eigenvalues) where T : struct
    {
        if (!matrix.IsSquare()) throw new ArgumentException("Matrix isn't square.");

        return matrix.Rows switch
        {
            0 => [new GeneralMatrix<T>([])],
            1 => [new GeneralMatrix<T>([[matrix[1, 1]]])],
            2 => matrix.QuadraticVector(eigenvalues),
            _ => matrix.QrAlgorithmVector(eigenvalues)
        };
    }

    private static ICollection<GeneralMatrix<T>> QuadraticVector<T>(this GeneralMatrix<T> matrix, ICollection<T> eigenvalues) where T : struct
    {
        // TODO
        throw new NotImplementedException();
    }

    private static ICollection<GeneralMatrix<T>> QrAlgorithmVector<T>(this GeneralMatrix<T> matrix, ICollection<T> eigenvalues) where T : struct
    {
        // TODO
        throw new NotImplementedException();
    }
}
