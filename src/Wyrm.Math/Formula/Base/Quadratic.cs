using Wyrm.Math.TypeFunctions;

namespace Wyrm.Math.Formula.Base;

internal static class Quadratic<T1, T2> where T1 : struct where T2 : struct
{
    public static ICollection<T2> Roots(T1 a, T1 b, T1 c, ITypeFunctions<T1, T2> funcs)
    {
        var gap = funcs.Sqrt(funcs.Sub(funcs.Mul(b, b), funcs.Mul(funcs.Four(), a, c)));
        var divisor = funcs.Mul(funcs.Two(), a);
        var negB = funcs.Neg(b);
        return
            [
                funcs.Div(funcs.Sub(negB, gap), divisor),
                funcs.Div(funcs.Add(negB, gap), divisor)
            ];
    }
}
